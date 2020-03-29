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
  /// POV Command
  /// </summary>
  [DataContract]
  internal class CommandPov : CommandBase
  {
    /*
        POV:      { "P": {"Index": 1|2|3|4, "Direction": "c | u | r | d | l", "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }   
                    - Index n=> 1..MaxPOV (setup of vJoy)
                    - JoyNo j=> 1..Max (caller dependent) (defaults to 1)
                    - Direction either of the chars (center (released), up, right, donw, left)
    */
    [DataMember( IsRequired = true, Name = "Index" )]
    internal short Index { get; set; }
    [DataMember( IsRequired = true, Name = "Direction" )]
    internal string Direction { get; set; }
    [DataMember( Name = "JNo" )]
    internal int JNo { get; set; } = 1;

    // non Json

    /// <summary>
    /// Returns the VJCommand for this item
    /// </summary>
    internal override VJCommand Cmd
    {
      get {
        var retVal = base.Cmd; // evaluate Ext1..3

        if ( Index < 1 || Index > 4 ) {
          return retVal; // ERROR - bail out on invalid number
        }

        retVal.CtrlType = VJ_ControllerType.VJ_Hat;
        retVal.CtrlJNo = ( JNo > 0 ) ? JNo : 1;
        retVal.CtrlIndex_keycode = Index;
        switch ( Direction.ToLowerInvariant( ) ) {
          case "c":
            retVal.CtrlDirection = VJ_ControllerDirection.VJ_Center;
            break;
          case "r":
            retVal.CtrlDirection = VJ_ControllerDirection.VJ_Right;
            break;
          case "l":
            retVal.CtrlDirection = VJ_ControllerDirection.VJ_Left;
            break;
          case "u":
            retVal.CtrlDirection = VJ_ControllerDirection.VJ_Up;
            break;
          case "d":
            retVal.CtrlDirection = VJ_ControllerDirection.VJ_Down;
            break;
          default: // just return the default message (unknown ctrl)
            retVal.CtrlType = VJ_ControllerType.VJ_Unknown;
            break;
        }

        string j = retVal.JString; // create the Json command string (no lazy init)
        return retVal;
      }
    }
  }
}
