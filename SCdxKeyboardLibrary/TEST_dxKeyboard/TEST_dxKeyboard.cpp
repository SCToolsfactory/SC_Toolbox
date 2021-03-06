// TEST_dxKeyboard.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "..\SCdxKeyboard\SCdxKeyboard.h"  // the lower level driver
#include "..\SCdxKeyboard\SXdxKeyCodes.h"  // the lower level driver
#pragma comment (lib,"SCdxKeyboard.lib")

void Test1() {
	// LAlt+PageUp  sequence
	SCdxKeyboard::KeyDown(VK_LALT);
	SCdxKeyboard::KeyDown(VK_PGUP);
	SCdxKeyboard::Sleep_ms(100);
	SCdxKeyboard::KeyUp(VK_PGUP);
	SCdxKeyboard::KeyUp(VK_LALT); 
}

void Test2() {
	// Test if scancodes follow the layout and not the letter written on the keys...
	SCdxKeyboard::KeyDown('Y'); // should give Z on a QWERTZ keyboard i.e. the layout char of the US kbd (second left above L-Alt)
	SCdxKeyboard::Sleep_ms(100);
	SCdxKeyboard::KeyUp('Y');
	printf("should give Z on a QWERTZ keyboard i.e. the layout char of the US kbd (second left above L-Alt) \n");
}

void Test3() {
	// Test if scancodes follow the layout and not the letter written on the keys...
	SCdxKeyboard::KeyDown('Z'); // should give Y on a QWERTZ keyboard i.e. the layout char of the QWERT_  sequence
	SCdxKeyboard::Sleep_ms(100);
	SCdxKeyboard::KeyUp('Z');
	printf("should give Y on a QWERTZ keyboard i.e. the layout char of the QWERT_  sequence\n");
}

void Test4() {
	// Test how fast chars are recognised as Up/Down
	SCdxKeyboard::KeyTap('G');
	printf("should give G \n");
}

void Test4a(unsigned short msec) {
	// Test how fast chars are recognised as Up/Down
	SCdxKeyboard::KeyDown('G');
	SCdxKeyboard::Sleep_ms(msec);
	SCdxKeyboard::KeyUp('G');
	printf("should give G \n");
}

void Test5() {
	// NumPad Enter - (Note in Win there is no regular VirtKey - it is defined for this DLL only)
	SCdxKeyboard::KeyTap(VK_NP_ENTER);
	printf("should give NumPad Enter \n");
}

void Test6() {
	// Tilde
	SCdxKeyboard::KeyTap(VK_MINUS);
	printf("should give ~ \n");
}

void Test7() {
	// =
	SCdxKeyboard::KeyTap(VK_EQUALS);
	printf("should give 'equals' \n");
}


int main()
{
	printf("Waiting 5 secs now - change active window\n");

	SCdxKeyboard::Sleep_ms(5000);
	printf("About to send key\n");
	
	// a long space Tap to start with - 
	SCdxKeyboard::KeyDown(' ');
	SCdxKeyboard::Sleep_ms(100);
	SCdxKeyboard::KeyUp(' ');

	//Test4(); // immediate Down Up
	// Test4a(1); // delay between DOWN and UP
	Test7();

	printf("Program ends now\n");
	return 0;
}

