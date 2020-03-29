/**
* @file           SCdxKeyboard.h
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


#ifndef _SCdxKeyboard_INCLUDE
#define _SCdxKeyboard_INCLUDE


#pragma once

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the SCDXKEYBOARD_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// SCDXKEYBOARD_API functions as being imported from a DLL, wheras this DLL sees symbols
// defined with this macro as being exported.
#ifdef SCDXKEYBOARD_EXPORTS
	#define SCDXKEYBOARD_API __declspec(dllexport) 
	#define SCDXKEYBOARD_API_(type) __declspec(dllexport) type STDAPICALLTYPE
#else
	#define SCDXKEYBOARD_API __declspec(dllimport)
	#define SCDXKEYBOARD_API_(type) __declspec(dllimport) type STDAPICALLTYPE
#endif

#include <windows.h>


// This class is exported from the SCdxKeyboard.dll
class SCDXKEYBOARD_API SCdxKeyboard
{
public:  // Structs, Enums

private:
	static void ScanCode(int vKey, WORD &scanCode, bool &extended);

public:
	static void KeyDown(int vKey);
	static void KeyUp(int vKey);

	static void KeyTap(int vKey);

	static void KeyStroke(int vKey, unsigned int msec);

	static void Sleep_ms(unsigned int  mseconds);

private: // VARS

};



// plain C calls
SCDXKEYBOARD_API_(VOID) KeyDown_C(int vKey);
SCDXKEYBOARD_API_(VOID) KeyUp_C(int vKey);
SCDXKEYBOARD_API_(VOID) KeyTap_C(int vKey);
SCDXKEYBOARD_API_(VOID) KeyStroke_C(int vKey, unsigned int msec);
SCDXKEYBOARD_API_(VOID) Sleep_ms_C(unsigned int  mseconds);

#endif // _SCdxKeyboard_INCLUDE
