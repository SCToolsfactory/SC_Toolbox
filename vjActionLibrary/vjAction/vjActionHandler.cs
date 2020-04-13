using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vjMapper.VjOutput;

namespace vjAction
{

  /// <summary>
  /// Wrapping of vJoy and Keyboard commands
  /// </summary>
  public class vjActionHandler
  {
    /// <summary>
    /// Returns true if the Keyboard DLL is loaded
    /// </summary>
    public static bool IsKbdDllLoaded { get => dxKbdInterfaceWrap.SCdxKeyboard.isDllLoaded; }

    /// <summary>
    /// Returns true if the vJoy DLL is loaded
    /// </summary>
    public static bool IsvJoyDllLoaded { get => vJoyInterfaceWrap.vJoy.isDllLoaded; }

    /// <summary>
    /// Returns true if the vJoy device with number exists
    /// </summary>
    /// <param name="deviceNo">A vJoy device number (1..16)</param>
    /// <returns>True if it exists</returns>
    public static bool IsvJoyDeviceExists(int deviceNo )
    {
      var tvJoy = new vJoyInterfaceWrap.vJoy( );
      return tvJoy.isVJDExists( (uint)deviceNo );
    }


    /// <summary>
    /// Connects to the nth vJoy device
    /// </summary>
    /// <param name="deviceNo">The vJoy number 1..16(if configured)</param>
    /// <returns></returns>
    public static bool ConnectJoystick(int deviceNo )
    {
      return vJoy.vJoyHandler.Instance.Connect( deviceNo );
    }

    /// <summary>
    /// Disconnecs the vJoy interface
    /// </summary>
    public static void DisconnectJoysticks()
    {
      vJoy.vJoyHandler.Instance.Disconnect( );
    }


    /// <summary>
    /// Transparent handling of vJoy and Kbd Commands
    /// Submit a VJCommand and it will either issue the contained vJoy or Keyboard command
    /// vJoy device is assumed to be in message.CtrlJNo 1..16 (if connected)
    /// </summary>
    /// <param name="vJCommand">A VJCommand</param>
    public static bool HandleMessage( VJCommand vJCommand )
    {
      if ( !vJCommand.IsValid ) return false;
      if ( vJCommand.IsVJoyCommand ) {
        return vJoy.vJoyHandler.Instance.HandleMessage( vJCommand );
      }
      else if ( vJCommand.IsKeyCommand ) {
        return Kbd.KbdHandler.Instance.HandleMessage( vJCommand );
      }
      // any other unsupported
      return false;
    }

  }
}
