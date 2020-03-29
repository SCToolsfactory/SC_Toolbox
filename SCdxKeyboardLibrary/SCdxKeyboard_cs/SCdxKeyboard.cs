/**
* @file           SCdxKeyboard.cs
*****************************************************************************
* Consts
*
*  Interfaces the named DLL sending keycodes to the activa application
*
* Copyright (C) 2020 Martin Burri  (bm98@burri-web.org)
*
*
*<hr>
*
* @b Project      SXdxInput<br>
*
* @author         M. Burri
* @date           29-Mar-2020
*
*****************************************************************************
*<hr>
* @b Updates
* - dd-mmm-yyyy V. Name: Description
*
*****************************************************************************/

using System;
using static dxKbdInterfaceWrap.SCdxKeyboard_DLL;

namespace dxKbdInterfaceWrap
{
  public class SCdxKeyboard
  {

    public static bool Enabled { get; set; } = false; // default false - not true...

    public static bool isDllLoaded { get => UnsafeNativeMethods.NativeModuleHandle != IntPtr.Zero; }

    public static void KeyDown( int vKey )
    {
      if ( Enabled )
        UnsafeNativeMethods.KeyDown( vKey );
    }

    public static void KeyUp( int vKey )
    {
      if ( Enabled )
        UnsafeNativeMethods.KeyUp( vKey );
    }

    public static void KeyTap( int vKey )
    {
      if ( Enabled )
        UnsafeNativeMethods.KeyTap( vKey );
    }

    public static void KeyStroke( int vKey, uint msec )
    {
      if ( Enabled )
        UnsafeNativeMethods.KeyStroke( vKey, msec );
    }

    public static void Sleep_ms( uint msec )
    {
      UnsafeNativeMethods.Sleep_ms( msec );
    }


  }
}
