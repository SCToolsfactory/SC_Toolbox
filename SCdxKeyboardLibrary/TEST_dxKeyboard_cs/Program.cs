using System;

using dxKbdInterfaceWrap;
using static dxKbdInterfaceWrap.SCdxKeyboard;

namespace TEST_dxKeyboard_cs
{
  class Program
  {


    static void Test1()
    {
      // LAlt+PageUp  sequence
      KeyDown( (int)SCdxKeycode.VK_LALT );
      KeyDown( (int)SCdxKeycode.VK_PGUP );
      Sleep_ms( 100 );
      KeyUp( (int)SCdxKeycode.VK_PGUP );
      KeyUp( (int)SCdxKeycode.VK_LALT );
    }

    static void Test2()
    {
      // Test if scancodes follow the layout and not the letter written on the keys...
      KeyDown( 'Y' ); // should give Z on a QWERTZ keyboard i.e. the layout char of the US kbd (second left above L-Alt)
      Sleep_ms( 100 );
      KeyUp( 'Y' );
      Console.WriteLine( "should give Z on a QWERTZ keyboard i.e. the layout char of the US kbd (second left above L-Alt) \n" );
    }

    static void Test3()
    {
      // Test if scancodes follow the layout and not the letter written on the keys...
      KeyDown( 'Z' ); // should give Y on a QWERTZ keyboard i.e. the layout char of the QWERT_  sequence
      Sleep_ms( 100 );
      KeyUp( 'Z' );
      Console.WriteLine( "should give Y on a QWERTZ keyboard i.e. the layout char of the QWERT_  sequence\n" );
    }

    static void Test4()
    {
      // Test how fast chars are recognised as Up/Down
      KeyTap( 'G' );
      Console.WriteLine( "should give G \n" );
    }

    static void Test4a( ushort msec )
    {
      // Test how fast chars are recognised as Up/Down
      KeyDown( 'G' );
      Sleep_ms( msec );
      KeyUp( 'G' );
      Console.WriteLine( "should give G \n" );
    }

    static void Test5()
    {
      // NumPad Enter - (Note in Win there is no regular VirtKey - it is defined for this DLL only)
      KeyTap( (int)SCdxKeycode.VK_NP_ENTER );
      Console.WriteLine( "should give NumPad Enter \n" );
    }

    static void Test6()
    {
      // Tilde
      KeyTap( (int)SCdxKeycode.VK_MINUS );
      Console.WriteLine( "should give ~ \n" );
    }

    static void Test7()
    {
      // =
      KeyTap( (int)SCdxKeycode.VK_EQUALS );
      Console.WriteLine( "should give 'equals' \n" );
    }



    static void Main( string[] args )
    {
      Console.WriteLine( "Waiting 5 secs now - change active window\n" );

      Sleep_ms( 5000 );
      Console.WriteLine( "About to send key\n" );

      // a long space Tap to start with - 
      KeyDown( ' ' );
      Sleep_ms( 100 );
      KeyUp( ' ' );

      //Test4(); // immediate Down Up
      // Test4a(1); // delay between DOWN and UP
      Test7( );

      Console.WriteLine( "Program ends now\n" );

    }
  }
}
