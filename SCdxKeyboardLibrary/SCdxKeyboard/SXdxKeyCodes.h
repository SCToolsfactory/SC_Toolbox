/**
* @file           SXdxKeyCodes.h
*****************************************************************************
* Consts
*
*  Provides keyboard scan codes for Input simulation
*   Note: this is just a copy of the WinUser.h section for convenience only
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

#pragma once

#include "stdafx.h"

// Only if WinUser.h is not already included
#ifndef _WINUSER_

/*
* Virtual Keys Standard Set
*/
#define VK_LBUTTON        0x01
#define VK_RBUTTON        0x02
#define VK_CANCEL         0x03
#define VK_MBUTTON        0x04    /* NOT contiguous with L & RBUTTON */

#if(_WIN32_WINNT >          0x0500)
#define VK_XBUTTON1       0x05    /* NOT contiguous with L & RBUTTON */
#define VK_XBUTTON2       0x06    /* NOT contiguous with L & RBUTTON */
#endif /* _WIN32_WINNT >          0x0500 */

/*
* 0x07 : reserved
*/

#define VK_BACK           0x08
#define VK_TAB            0x09

/*
* 0x0A - 0x0B : reserved
*/

#define VK_CLEAR          0x0C
#define VK_RETURN         0x0D

/*
* 0x0E - 0x0F : unassigned
*/

#define VK_SHIFT          0x10
#define VK_CONTROL        0x11
#define VK_MENU           0x12
#define VK_PAUSE          0x13
#define VK_CAPITAL        0x14

/*
* 0x16 : unassigned
*/

/*
* 0x1A : unassigned
*/

#define VK_ESCAPE         0x1B

#define VK_CONVERT        0x1C
#define VK_NONCONVERT     0x1D
#define VK_ACCEPT         0x1E
#define VK_MODECHANGE     0x1F

#define VK_SPACE          0x20
#define VK_PRIOR          0x21
#define VK_NEXT           0x22
#define VK_END            0x23
#define VK_HOME           0x24
#define VK_LEFT           0x25  // Arrows
#define VK_UP             0x26  // Arrows
#define VK_RIGHT          0x27  // Arrows
#define VK_DOWN           0x28  // Arrows
#define VK_SELECT         0x29
#define VK_PRINT          0x2A
#define VK_EXECUTE        0x2B
#define VK_SNAPSHOT       0x2C 
#define VK_INSERT         0x2D
#define VK_DELETE         0x2E
#define VK_HELP           0x2F

/*
* VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
* 0x3A - 0x40 : unassigned
* VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
*/
#define VK_0		   0x30
#define VK_1           0x31
#define VK_2           0x32
#define VK_3           0x33
#define VK_4           0x34
#define VK_5           0x35
#define VK_6           0x36
#define VK_7           0x37
#define VK_8           0x38
#define VK_9           0x39

#define VK_A           0x41
#define VK_B           0x42
#define VK_C           0x43
#define VK_D           0x44
#define VK_E           0x45
#define VK_F           0x46
#define VK_G           0x47
#define VK_H           0x48
#define VK_I           0x49
#define VK_J           0x4A
#define VK_K           0x4B
#define VK_L           0x4C
#define VK_M           0x4D
#define VK_N           0x4E
#define VK_O           0x4F
#define VK_P           0x50
#define VK_Q           0x51
#define VK_R           0x52
#define VK_S           0x53
#define VK_T           0x54
#define VK_U           0x55
#define VK_V           0x56
#define VK_W           0x57
#define VK_X           0x58
#define VK_Y           0x59
#define VK_Z           0x5A


#define VK_LWIN           0x5B  // Left Win Key
#define VK_RWIN           0x5C  // RIght Win Key
#define VK_APPS           0x5D

/*
* 0x5E : reserved
*/

#define VK_SLEEP          0x5F

#define VK_NUMPAD0        0x60
#define VK_NP_0				VK_NUMPAD0
#define VK_NUMPAD1        0x61
#define VK_NP_1				VK_NUMPAD1
#define VK_NUMPAD2        0x62
#define VK_NP_2				VK_NUMPAD2
#define VK_NUMPAD3        0x63
#define VK_NP_3				VK_NUMPAD3
#define VK_NUMPAD4        0x64
#define VK_NP_4				VK_NUMPAD4
#define VK_NUMPAD5        0x65
#define VK_NP_5				VK_NUMPAD5
#define VK_NUMPAD6        0x66
#define VK_NP_6				VK_NUMPAD6
#define VK_NUMPAD7        0x67
#define VK_NP_7				VK_NUMPAD7
#define VK_NUMPAD8        0x68
#define VK_NP_8				VK_NUMPAD8
#define VK_NUMPAD9        0x69
#define VK_NP_9				VK_NUMPAD9


#define VK_MULTIPLY       0x6A
#define VK_ADD            0x6B
#define VK_SEPARATOR      0x6C
#define VK_SUBTRACT       0x6D
#define VK_DECIMAL        0x6E
#define VK_DIVIDE         0x6F
#define VK_F1             0x70
#define VK_F2             0x71
#define VK_F3             0x72
#define VK_F4             0x73
#define VK_F5             0x74
#define VK_F6             0x75
#define VK_F7             0x76
#define VK_F8             0x77
#define VK_F9             0x78
#define VK_F10            0x79
#define VK_F11            0x7A
#define VK_F12            0x7B
#define VK_F13            0x7C
#define VK_F14            0x7D
#define VK_F15            0x7E
/*
not defined in DirectInput - hence left out here
#define VK_F16            0x7F
#define VK_F17            0x80
#define VK_F18            0x81
#define VK_F19            0x82
#define VK_F20            0x83
#define VK_F21            0x84
#define VK_F22            0x85
#define VK_F23            0x86
#define VK_F24            0x87
*/

#define VK_NUMLOCK        0x90
#define VK_SCROLL         0x91  // SCROLL LOCK

/*
* 0x97 - 0x9F : unassigned
*/

/*
* VK_L* & VK_R* - left and right Alt Ctrl and Shift virtual keys.
* Used only as parameters to GetAsyncKeyState() and GetKeyState().
* No other API or message will distinguish left and right keys in this way.
*/
#define VK_LSHIFT         0xA0
#define VK_RSHIFT         0xA1
#define VK_LCONTROL       0xA2
#define VK_RCONTROL       0xA3
#define VK_LMENU          0xA4
#define VK_RMENU          0xA5

/*
* 0xB8 - 0xB9 : reserved
*/

#define VK_OEM_1          0xBA   // ';:' for US
#define VK_OEM_PLUS       0xBB   // '+' any country
#define VK_OEM_COMMA      0xBC   // '' any country
#define VK_OEM_MINUS      0xBD   // '-' any country
#define VK_OEM_PERIOD     0xBE   // '.' any country
#define VK_OEM_2          0xBF   // '/?' for US
#define VK_OEM_3          0xC0   // '`~' for US

/*
* 0xC1 - 0xC2 : reserved
*/


#define VK_OEM_4          0xDB  //  '[{' for US
#define VK_OEM_5          0xDC  //  '\|' for US
#define VK_OEM_6          0xDD  //  ']}' for US
#define VK_OEM_7          0xDE  //  ''"' for US
#define VK_OEM_8          0xDF

/*
* 0xE0 : reserved
*/

#if(WINVER >          0x0400)
#define VK_PROCESSKEY     0xE5
#endif /* WINVER >          0x0400 */

/*
* 0xE8 : unassigned
*/

/*
* 0xFF : reserved
*/


#endif // _WINUSER_

// Added to converge more with DirectInput naming and additions

// US ISO Kbd 1st row after Key 0
#define VK_BACKSPACE      VK_BACK  // added
#define VK_EQUALS         VK_OEM_6 // added
#define VK_MINUS          VK_OEM_4 // added

// US ISO Kbd 2nd row after Key P
#define VK_LBRACKET       VK_OEM_1 // added
#define VK_RBRACKET       VK_OEM_3 // added

// US ISO Kbd 3rd row after Key L
#define VK_SEMICOLON      VK_OEM_7 // added
#define VK_APOSTROPHE     VK_OEM_5 // added
#define VK_BACKSLASH      VK_OEM_8 // added

// US ISO Kbd 4th row after Key M
#define VK_SLASH          VK_OEM_MINUS // added
#define VK_PERIOD         VK_OEM_PERIOD // added
#define VK_COMMA          VK_OEM_COMMA // added


// NumPad aside from numbers
#define VK_NP_DIVIDE    VK_DIVIDE  // added 
#define VK_NP_MULTIPLY     VK_MULTIPLY // added 
#define VK_NP_SUBTRACT    VK_SUBTRACT  // added 
#define VK_NP_ADD     VK_ADD // added 
#define VK_NP_ENTER    VK_RETURN+1 // added - needs special treatment in the DLL...
#define VK_NP_PERIOD   VK_DECIMAL  // added 


#define VK_ALT            VK_MENU   //  added generic ALT key                    MENU
#define VK_CAPSLOCK       VK_CAPITAL  // added
#define VK_PGUP           VK_PRIOR  //  added
#define VK_PGDN           VK_NEXT  //  added
#define VK_LEFTARROW      VK_LEFT  // Arrows
#define VK_UPARROW        VK_UP  // Arrows
#define VK_RIGHTARROW     VK_RIGHT  // Arrows
#define VK_DOWNARROW      VK_DOWN  // Arrows
#define VK_PRINTSCREEN    VK_SNAPSHOT // added
#define VK_LALT           VK_LMENU // added
#define VK_RALT           VK_RMENU // added

// NUMLOCK -> PAUSE

