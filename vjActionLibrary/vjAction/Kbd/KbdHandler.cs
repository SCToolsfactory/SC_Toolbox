using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dxKbdInterfaceWrap;
using static dxKbdInterfaceWrap.SCdxKeyboard;
using vjMapper.VjOutput;

namespace vjAction.Kbd
{
  /// <summary>
  /// Singleton that interacts with the DxKeyboard DLL
  /// Accepts messages to be processed
  /// </summary>
  public sealed class KbdHandler
  {
    private KbdHandler() { }


    /// <summary>
    /// Returns the sole instance of the Keyboard handler
    /// </summary>
    public static KbdHandler Instance { get; } = new KbdHandler( );


    private int m_cmdNo = 0; // commands issued counter

    /// <summary>
    /// Occurs when a Joystick Button is pressed
    /// </summary>
    public event EventHandler<vjActionEventArgs> Ping;

    private void OnPing( int arg )
    {
      Ping?.Invoke( this, new vjActionEventArgs( arg ) );
    }

    /// <summary>
    /// Check if Kbd and vJoy are available
    /// </summary>
    /// <returns>Returns true if Kbd and vJoy are available</returns>
    public bool AreLibrariesLoaded()
    {
      bool ret = true;
      ret &= SCdxKeyboard.isDllLoaded;
      return ret;
    }

    /// <summary>
    /// Return the connection joystick state
    /// </summary>
    public bool Connected { get; private set; } = false;

    private void Modifier( VJ_Modifier modifier, bool press )
    {
      if ( modifier == VJ_Modifier.VJ_None ) return;
      int mod = 0;
      if ( modifier == VJ_Modifier.VJ_LCtrl )
        mod = (int)SCdxKeycode.VK_LCONTROL;
      else if ( modifier == VJ_Modifier.VJ_RCtrl )
        mod = (int)SCdxKeycode.VK_RCONTROL;
      else if ( modifier == VJ_Modifier.VJ_LAlt )
        mod = (int)SCdxKeycode.VK_LALT;
      else if ( modifier == VJ_Modifier.VJ_RAlt )
        mod = (int)SCdxKeycode.VK_RALT;
      else if ( modifier == VJ_Modifier.VJ_LShift )
        mod = (int)SCdxKeycode.VK_LSHIFT;
      else if ( modifier == VJ_Modifier.VJ_RShift )
        mod = (int)SCdxKeycode.VK_RSHIFT;

      if ( press ) {
        KeyDown( mod );
      }
      else {
        // release
        KeyUp( mod );
      }
    }

    /// <summary>
    /// Dispatch the command message 
    /// </summary>
    /// <param name="message">A VJoy Message</param>
    public bool HandleMessage( VJCommand message )
    {
      if ( !AreLibrariesLoaded( ) ) return false; // ERROR - bail out for missing libraries
      if ( !message.IsValid ) return false; // ERROR - bail out for undef messages

      bool retVal = false;

      // dxKey Message
      if ( message.CtrlType == VJ_ControllerType.DX_Key ) {
        switch ( message.CtrlDirection ) {
          case VJ_ControllerDirection.VJ_Down:
            foreach ( var m in message.CtrlModifier ) Modifier( m, true );
            KeyDown( message.CtrlIndex );
            break;

          case VJ_ControllerDirection.VJ_Up:
            KeyUp( message.CtrlIndex );
            foreach ( var m in message.CtrlModifier ) Modifier( m, false );
            break;

          case VJ_ControllerDirection.VJ_Tap:
            foreach ( var m in message.CtrlModifier ) Modifier( m, true );
            KeyStroke( message.CtrlIndex, (uint)message.CtrlValue );
            foreach ( var m in message.CtrlModifier ) Modifier( m, false );
            break;

          case VJ_ControllerDirection.VJ_DoubleTap:
            foreach ( var m in message.CtrlModifier ) Modifier( m, true );
            KeyStroke( message.CtrlIndex, (uint)message.CtrlValue );
            foreach ( var m in message.CtrlModifier ) Modifier( m, false );
            Sleep_ms( 25 ); // double tap delay is fixed
            foreach ( var m in message.CtrlModifier ) Modifier( m, true );
            KeyStroke( message.CtrlIndex, (uint)message.CtrlValue );
            foreach ( var m in message.CtrlModifier ) Modifier( m, false );
            break;

          default:
            break;
        }
        retVal = true; // finally we made it
      }

      if ( retVal )
        OnPing( ++m_cmdNo ); // just issued a joystick command

      return retVal;
    }



  }
}
