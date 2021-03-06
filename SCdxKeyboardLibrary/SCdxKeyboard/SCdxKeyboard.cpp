/**
* @file           SCdxKeyboard.cpp
*****************************************************************************
* Consts
*
*  Implements sending keycodes to the activa application
*
* Copyright (C) 2018 Martin Burri  (bm98@burri-web.org)
*
*
*<hr>
*
* @b Project      SXdxInput<br>
*
* @author         M. Burri
* @date           27-May-2018
*
*****************************************************************************
*<hr>
* @b Updates
* - dd-mmm-yyyy V. Name: Description
*
*****************************************************************************/

// Defines the entry point for the DLL application.
//



#include "stdafx.h"
#include "SCdxKeyboard.h"

#include <WinUser.h>  // using user32.dll


/// The exported DLL hook 
BOOL APIENTRY DllMain(HANDLE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}
// plain C calls
SCDXKEYBOARD_API_(VOID) KeyDown_C(int vKey)
{
	SCdxKeyboard::KeyDown(vKey);
}

SCDXKEYBOARD_API_(VOID) KeyUp_C(int vKey)
{
	SCdxKeyboard::KeyUp(vKey);
}

SCDXKEYBOARD_API_(VOID) KeyTap_C(int vKey)
{
	SCdxKeyboard::KeyTap(vKey);
}

SCDXKEYBOARD_API_(VOID) KeyStroke_C(int vKey, unsigned int msec)
{
	SCdxKeyboard::KeyStroke(vKey, msec);
}

SCDXKEYBOARD_API_(VOID) Sleep_ms_C(unsigned int  mseconds)
{
	SCdxKeyboard::Sleep_ms(mseconds);
}



///////////////////////////////////////////////////////////////////////////////
/// Static Class Code

///////////////////////////////////////////////////////////////////////////////
/// @brief Sends a Key down - press
/// @return void
void SCdxKeyboard::KeyDown(int vKey) {
	// sends a scancode
	INPUT input;
	input.type = INPUT_KEYBOARD; ::memset(&(input.ki), 0, sizeof(KEYBDINPUT));
	// keydown = 0 (else it is KEYEVENTF_KEYUP  ; | KEYEVENTF_EXTENDEDKEY)
	bool extended = false;
	ScanCode(vKey, input.ki.wScan, extended);
	input.ki.dwFlags = KEYEVENTF_SCANCODE | ((extended == true) ? KEYEVENTF_EXTENDEDKEY : 0);
	UINT sentCmd = ::SendInput(1, &input, sizeof(INPUT)); // nInputs, InputArray, SizeOf
}

///////////////////////////////////////////////////////////////////////////////
/// @brief Sends a Key up - release
/// @return void
void SCdxKeyboard::KeyUp(int vKey) {
	// sends a scancode
	INPUT input;
	input.type = INPUT_KEYBOARD; ::memset(&(input.ki), 0, sizeof(KEYBDINPUT));
	// keydown = 0 (else it is KEYEVENTF_KEYUP  ; | KEYEVENTF_EXTENDEDKEY)
	bool extended = false;
	ScanCode(vKey, input.ki.wScan, extended);
	input.ki.dwFlags = KEYEVENTF_KEYUP | KEYEVENTF_SCANCODE | ((extended == true) ? KEYEVENTF_EXTENDEDKEY : 0);
	UINT sentCmd = ::SendInput(1, &input, sizeof(INPUT));
}

///////////////////////////////////////////////////////////////////////////////
/// @brief Sends a Key tap - press -/- release
/// @return void
void SCdxKeyboard::KeyTap(int vKey) {
	// sends a scancode
	INPUT input[2];
	input[0].type = INPUT_KEYBOARD; ::memset(&(input[0].ki), 0, sizeof(KEYBDINPUT));
	input[1].type = INPUT_KEYBOARD; ::memset(&(input[1].ki), 0, sizeof(KEYBDINPUT));
	// keydown = 0 (else it is KEYEVENTF_KEYUP  ; | KEYEVENTF_EXTENDEDKEY)
	bool extended = false;
	ScanCode(vKey, input[0].ki.wScan, extended);
	input[0].ki.dwFlags = KEYEVENTF_SCANCODE | ((extended) ? KEYEVENTF_EXTENDEDKEY : 0);
	input[1].ki.wScan = input[0].ki.wScan;
	input[1].ki.dwFlags = KEYEVENTF_KEYUP | KEYEVENTF_SCANCODE | ((extended == true) ? KEYEVENTF_EXTENDEDKEY : 0);
	UINT sentCmd = ::SendInput(1, &input[0], sizeof(INPUT));
	Sleep_ms(1); // needs a minimum, else DirecInput may not really see it...
	sentCmd = ::SendInput(1, &input[1], sizeof(INPUT));
}

///////////////////////////////////////////////////////////////////////////////
/// @brief Sends a Key tap - press - wait - release
/// @return void
void SCdxKeyboard::KeyStroke(int vKey, unsigned int msec) {
	// sends a scancode
	INPUT input[2];
	input[0].type = INPUT_KEYBOARD; ::memset(&(input[0].ki), 0, sizeof(KEYBDINPUT));
	input[1].type = INPUT_KEYBOARD; ::memset(&(input[0].ki), 0, sizeof(KEYBDINPUT));
	// keydown = 0 (else it is KEYEVENTF_KEYUP  ; | KEYEVENTF_EXTENDEDKEY)
	bool extended = false;
	ScanCode(vKey, input[0].ki.wScan, extended);
	input[1].ki.wScan = input[0].ki.wScan;
	input[0].ki.dwFlags = KEYEVENTF_SCANCODE | ((extended == true) ? KEYEVENTF_EXTENDEDKEY : 0);
	input[1].ki.dwFlags = KEYEVENTF_KEYUP | KEYEVENTF_SCANCODE | ((extended == true) ? KEYEVENTF_EXTENDEDKEY : 0);
	UINT sentCmd = ::SendInput(1, &input[0], sizeof(INPUT));
	Sleep_ms(msec);  // delay - TODO - have to have this in a queue and do it asych if it gets longer than a few msecs
	sentCmd += ::SendInput(1, &input[1], sizeof(INPUT));
}


///////////////////////////////////////////////////////////////////////////////
/// @brief Translates virtual keys to scancodes
void SCdxKeyboard::ScanCode(int vKey, WORD &scanCode, bool &extended) {
	scanCode = 0; extended = false;
	int vk = vKey;
	// we defined NP_ENTER as VK_RETURN+1 (which is unassigned in Win)
	if (vk == (VK_RETURN + 1)) vk--; // NP_Enter is regular Enter with Extended bit set (MSDN)
	UINT32 sCode = ::MapVirtualKey(vk, MAPVK_VK_TO_VSC); // convert VirtualKeys to ScanCodes
	scanCode = sCode;
	// extended definition taken from the MSDN doc of Keyboard handling
	extended = (vKey == VK_RMENU) || (vKey == VK_RCONTROL)
		|| (vKey == VK_LEFT) || (vKey == VK_RIGHT) || (vKey == VK_UP) || (vKey == VK_DOWN)
		|| (vKey == VK_PRIOR) || (vKey == VK_NEXT) || (vKey == VK_HOME) || (vKey == VK_END) || (vKey == VK_INSERT) || (vKey == VK_DELETE)
		|| (vKey == VK_NUMLOCK) || (vKey == VK_PRINT) || (vKey == VK_CANCEL) || (vKey == VK_DIVIDE) || (vKey == (VK_RETURN + 1));
}


///////////////////////////////////////////////////////////////////////////////
/// @brief Sleep for an amount of milliseconds
void SCdxKeyboard::Sleep_ms(unsigned int mseconds)
{
	// Windows Sleep uses miliseconds
	// the argument is arriving in milliseconds
	::Sleep(mseconds);
}



