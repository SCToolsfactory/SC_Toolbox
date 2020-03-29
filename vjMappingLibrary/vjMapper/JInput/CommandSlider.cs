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
  /// Slider Command
  /// </summary>
  [DataContract]
  internal class CommandSlider : CommandBase
  {
    /*
        Slider:   { "S": {"Index": 1|2, "Value": number, "JNo": j,  "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr"} }
                    - number => 0..1000 (normalized)
    */
    [DataMember( IsRequired = true, Name = "Index" )]
    internal short Index { get; set; }
    [DataMember( IsRequired = true, Name = "Value" )]
    internal short Value { get; set; }
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

        if ( Value < 0 || Value > 1000 ) {
          return retVal; // ERROR - bail out on invalid number
        }
        if ( Index < 1 || Index > 2 ) {
          return retVal; // ERROR - bail out on invalid number
        }

        retVal.CtrlValue_Delay = Value;
        retVal.CtrlType = VJ_ControllerType.VJ_Slider;
        retVal.CtrlDirection = VJ_ControllerDirection.VJ_NotUsed;
        retVal.CtrlIndex_keycode = Index;
        retVal.CtrlJNo = ( JNo > 0 ) ? JNo : 1;

        string j = retVal.JString; // create the Json command string (no lazy init)
        return retVal;
      }
    }
  }

}
