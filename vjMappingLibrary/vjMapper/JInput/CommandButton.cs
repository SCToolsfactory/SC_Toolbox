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
  /// Button Command
  /// </summary>
  [DataContract]
  internal class CommandButton : CommandBase
  {
    /*
        Button:   { "B": {"Index": n, "Mode": "p|r|t|s|d", "Delay": msec, "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } } 
                    - Button Index n => 1..VJ_MAXBUTTON (setup of vJoy)
                    - Mode optional - either of the chars (see below)
    */
    [DataMember( IsRequired = true, Name = "Index" )]
    internal short Index { get; set; }
    [DataMember( Name = "Mode" )]
    internal string Mode { get; set; }
    [DataMember( Name = "Delay" )]
    internal short Delay { get; set; }
    [DataMember( Name = "JNo" )]
    internal short JNo { get; set; }

    // non Json

    /// <summary>
    /// Returns the VJCommand for this item
    /// </summary>
    internal override VJCommand Cmd
    {
      get {
        var retVal = base.Cmd; // evaluate Ext1..3

        retVal.CtrlIndex_keycode = Index;
        if ( ( retVal.CtrlIndex_keycode < 1 ) || ( retVal.CtrlIndex_keycode > VJCommand.VJ_MAXBUTTON ) ) {
          return retVal; // ERROR - bail out on invalid number
        }

        retVal.CtrlType = VJ_ControllerType.VJ_Button;
        retVal.CtrlJNo = ( JNo > 0 ) ? JNo : 1;
        HandleDelay( ref retVal, Delay );
        HandleMode( ref retVal, Mode );

        string j = retVal.JString; // create the Json command string (no lazy init)
        return retVal;
      }
    }
  }
}
