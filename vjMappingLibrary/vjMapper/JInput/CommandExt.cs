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
  /// Ext Command
  /// </summary>
  [DataContract]
  internal class CommandExt : CommandBase
  {
    /*
          Ext:    { "E": { "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }    // trigger only extended commands
    */
    // non Json

    /// <summary>
    /// Returns the VJCommand for this item
    /// </summary>
    internal override VJCommand Cmd
    {
      get {
        var retVal = base.Cmd; // evaluate Ext1..3

        retVal.CtrlType = VJ_ControllerType.OX_Ext;

        string j = retVal.JString; // create the Json command string (no lazy init)
        return retVal;
      }
    }

  }
}
