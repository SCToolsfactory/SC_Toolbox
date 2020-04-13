using System;

using vJoyInterfaceWrap;
using dxKbdInterfaceWrap;
using static dxKbdInterfaceWrap.SCdxKeyboard;
using vjMapper.VjOutput;
using System.Collections.Generic;

namespace vjAction.vJoy
{
  /// <summary>
  /// Instance that interacts with the VJoy DLL
  /// Accepts messages to be processed
  /// </summary>
  public sealed class vJoyHandler
  {
    private vJoyHandler() { }

    /// <summary>
    /// The vJoy Handling instance
    /// </summary>
    public static vJoyHandler Instance { get; } = new vJoyHandler( );

    /// <summary>
    /// Occurs when a Joystick command is issued
    /// </summary>
    public event EventHandler<vjActionEventArgs> Ping;


    // private stuff

    private int m_cmdNo = 0; // commands issued counter
    private vJoyInterfaceWrap.vJoy m_joystick = null;  // the vJoy device
    private SortedList<int, vJoystick> m_vJoystickList = new SortedList<int, vJoystick>( ); // my virtual JS

    private void OnPing( int arg )
    {
      Ping?.Invoke( this, new vjActionEventArgs( arg ) );
    }

    /// <summary>
    /// Check if vJoy is available
    /// </summary>
    /// <returns>Returns true if vJoy is available</returns>
    public bool AreLibrariesLoaded()
    {
      return vJoyInterfaceWrap.vJoy.isDllLoaded;
    }

    /// <summary>
    /// Connect to a Joystick instance
    /// </summary>
    /// <param name="n">The joystick ID 1..16</param>
    /// <returns>True if successfull</returns>
    public bool Connect( int n )
    {
      if ( m_vJoystickList.ContainsKey( n ) ) return true; // already connected

      bool connected = false;
      try {
        if ( !vJoyInterfaceWrap.vJoy.isDllLoaded ) return false; // ERROR exit, dll not loaded

        if ( n <= 0 || n > 16 ) return false;  // ERROR exit, invalid Js ID
        if ( m_joystick == null ) {
          m_joystick = new vJoyInterfaceWrap.vJoy( );
          if ( !m_joystick.vJoyEnabled( ) ) {
            Disconnect( ); // cleanup
            return false; // ERROR exit
          }
        }

        // try to control..
        connected = m_joystick.isVJDExists( (uint)n ); // exists?
        if ( connected ) {
          connected = m_joystick.AcquireVJD( (uint)n ); // to use?
        }
        if ( connected ) {
          bool r = m_joystick.ResetVJD( (uint)n );
          m_vJoystickList.Add( n, new vJoystick( m_joystick, (uint)n ) ); // the one to use..
        }
        else {
          return false; // ERROR exit
        }
      }
      catch {
        // wrong ... cannot for any reason
        m_joystick = null;
      }

      return connected;
    }

    /// <summary>
    /// Last one close the door...
    /// 
    /// Disconnect the Joystick system
    /// </summary>
    public void Disconnect()
    {
      // for any connected before
      foreach (var kv in m_vJoystickList ) {
        try {
          m_joystick.ResetVJD( (uint)kv.Key );
          m_joystick.RelinquishVJD( (uint)kv.Key );
        }
        catch { }
      }
      m_vJoystickList = new SortedList<int, vJoystick>( );
      m_joystick = null;
    }


    /// <summary>
    /// Dispatch the command message to the vJoy device
    /// message.CtrlJNo defines which vJoy device to use (1...12; only installed ones are used)
    /// </summary>
    /// <param name="message">A VJoy Message</param>
    public bool HandleMessage( VJCommand message )
    {
      if ( !AreLibrariesLoaded( ) ) return false; // ERROR - bail out for missing libraries
      if ( !message.IsValid ) return false; // ERROR - bail out for undef messages
      if ( !m_vJoystickList.ContainsKey( message.CtrlJNo ) ) return false;// ERROR - bail out for unconnected joysticks

      bool retVal = true;

      if ( message.IsVJoyCommand ) {
        // mutual exclusive access to the device
        lock ( m_joystick ) {
          try {
            switch ( message.CtrlType ) {
              case VJ_ControllerType.VJ_Axis:
                switch ( message.CtrlDirection ) {
                  case VJ_ControllerDirection.VJ_X:
                    m_vJoystickList[message.CtrlJNo].XAxis = message.CtrlValue;
                    break;
                  case VJ_ControllerDirection.VJ_Y:
                    m_vJoystickList[message.CtrlJNo].YAxis = message.CtrlValue;
                    break;
                  case VJ_ControllerDirection.VJ_Z:
                    m_vJoystickList[message.CtrlJNo].ZAxis = message.CtrlValue;
                    break;
                  default:
                    retVal = false;
                    break;
                }
                break;

              case VJ_ControllerType.VJ_RotAxis:
                switch ( message.CtrlDirection ) {
                  case VJ_ControllerDirection.VJ_X:
                    m_vJoystickList[message.CtrlJNo].XRotAxis = message.CtrlValue;
                    break;
                  case VJ_ControllerDirection.VJ_Y:
                    m_vJoystickList[message.CtrlJNo].YRotAxis = message.CtrlValue;
                    break;
                  case VJ_ControllerDirection.VJ_Z:
                    m_vJoystickList[message.CtrlJNo].ZRotAxis = message.CtrlValue;
                    break;
                  default:
                    retVal = false;
                    break;
                }
                break;

              case VJ_ControllerType.VJ_Slider:
                switch ( message.CtrlIndex ) {
                  case 1:
                    m_vJoystickList[message.CtrlJNo].Slider1 = message.CtrlValue;
                    break;
                  case 2:
                    m_vJoystickList[message.CtrlJNo].Slider2 = message.CtrlValue;
                    break;
                  default:
                    retVal = false;
                    break;
                }
                break;

              case VJ_ControllerType.VJ_Hat:
                switch ( message.CtrlDirection ) {
                  case VJ_ControllerDirection.VJ_Center:
                    m_vJoystickList[message.CtrlJNo].SetPOVDirection( message.CtrlIndex, POVType.Nil );
                    break;
                  case VJ_ControllerDirection.VJ_Left:
                    m_vJoystickList[message.CtrlJNo].SetPOVDirection( message.CtrlIndex, POVType.Left );
                    break;
                  case VJ_ControllerDirection.VJ_Right:
                    m_vJoystickList[message.CtrlJNo].SetPOVDirection( message.CtrlIndex, POVType.Right );
                    break;
                  case VJ_ControllerDirection.VJ_Up:
                    m_vJoystickList[message.CtrlJNo].SetPOVDirection( message.CtrlIndex, POVType.Up );
                    break;
                  case VJ_ControllerDirection.VJ_Down:
                    m_vJoystickList[message.CtrlJNo].SetPOVDirection( message.CtrlIndex, POVType.Down );
                    break;
                  default:
                    retVal = false;
                    break;
                }
                break;

              case VJ_ControllerType.VJ_Button:
                switch ( message.CtrlDirection ) {
                  case VJ_ControllerDirection.VJ_Down:
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, true );
                    break;
                  case VJ_ControllerDirection.VJ_Up:
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, false );
                    break;
                  case VJ_ControllerDirection.VJ_Tap:
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, true );
                    Sleep_ms( (uint)message.CtrlValue );
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, false );
                    break;
                  case VJ_ControllerDirection.VJ_DoubleTap:
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, true );
                    Sleep_ms( (uint)message.CtrlValue ); // tap delay
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, false );
                    Sleep_ms( 25 ); // double tap delay is fixed
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, true );
                    Sleep_ms( (uint)message.CtrlValue ); // tap delay
                    m_vJoystickList[message.CtrlJNo].SetButtonState( message.CtrlIndex, false );
                    break;
                  default:
                    retVal = false;
                    break;
                }
                break;


              default:
                retVal = false;
                break;
            }//switch message type

          }
          catch { // anything
            retVal = false;
          }

        }//endlock
      }

      if ( retVal )
        OnPing( ++m_cmdNo ); // just issued a joystick command

      return retVal;
    }



  }
}
