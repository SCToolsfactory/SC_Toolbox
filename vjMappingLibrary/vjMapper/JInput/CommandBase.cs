using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using vjMapper.VjOutput;

namespace vjMapper.JInput
{
  /// <summary>
  /// Command Template, implements Ext1..3
  /// </summary>
  [DataContract]
  internal abstract class CommandBase
  {
    // supporting extensions
    [DataMember( Name = "Ext1" )]
    internal string Ext1 { get; set; }
    [DataMember( Name = "Ext2" )]
    internal string Ext2 { get; set; }
    [DataMember( Name = "Ext3" )]
    internal string Ext3 { get; set; }

    // non Json

    /// <summary>
    /// Returns the VJCommand for this item
    /// </summary>
    virtual internal VJCommand Cmd
    {
      get {
        // stores only Ext1..3
        var ret = new VJCommand {
          CtrlExt1 = Ext1,
          CtrlExt2 = Ext2,
          CtrlExt3 = Ext3
        };
        return ret;
      }
    }

    // Handler for some multiple used Attributes

    /// <summary>
    /// Handles the Delay parameter of a command
    /// </summary>
    /// <param name="cmd">The VJCommand to modify</param>
    /// <param name="delay">The delay time in ms</param>
    protected void HandleDelay( ref VJCommand cmd, int delay )
    {
      if ( ( delay > 0 ) && ( delay <= 20_000 ) ) // sanity check for max delay..
        cmd.CtrlValue_Delay = delay;
      else
        cmd.CtrlValue_Delay = VJCommand.DEFAULT_DELAY; // if Delay is not provided or out of range
    }

    /// <summary>
    /// Handles the hit mode parameter of a command
    /// </summary>
    /// <param name="cmd">The VJCommand to modify</param>
    /// <param name="modeString">The hit mode</param>
    protected void HandleMode( ref VJCommand cmd, string modeString )
    {
      if ( string.IsNullOrEmpty( modeString ) ) modeString = "t"; // Tap is default if nothing is given
      switch ( modeString.ToLowerInvariant( ) ) {
        case "p": // pressed
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_Down;
          break;
        case "r": // released
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_Up;
          break;
        case "t": // tap
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_Tap;
          break;
        case "s": // short tap
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_Tap;
          cmd.CtrlValue_Delay = VJCommand.DEFAULT_SHORTDELAY; // const for short tap
          break;
        case "d": // double tap
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_DoubleTap;
          break;
        default: // just return the default message (unknown ctrl)
          cmd.CtrlType = VJ_ControllerType.VJ_Unknown;
          break;
      }

    }

  }

}
