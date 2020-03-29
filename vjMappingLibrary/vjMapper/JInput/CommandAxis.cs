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
  /// Axis Command
  /// </summary>
  [DataContract]
  internal class CommandAxis : CommandAxisBase
  {
    /*
        Axis:     { "A": {"Direction": "X|Y|Z", "Value": number, "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr"} }
                    - number => 0..1000 (normalized)
    */

    // Attributes in CommandAxisBase

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

        retVal.CtrlType = VJ_ControllerType.VJ_Axis;
        retVal.CtrlValue_Delay = Value;
        retVal.CtrlJNo = ( JNo > 0 ) ? JNo : 1;

        HandleAxis( ref retVal, Direction );

        string j = retVal.JString; // create the Json command string (no lazy init)
        return retVal;
      }
    }

  }


}
