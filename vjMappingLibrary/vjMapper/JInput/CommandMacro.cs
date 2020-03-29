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
  /// Macro Command
  /// </summary>
  [DataContract]
  internal class CommandMacro : CommandBase
  {
    /*
	      Macro:    { "M": {"Macro": "mName", "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }  
	           - mName - a macro name defined in the Macros section
    */
    [DataMember( IsRequired = true, Name ="Macro" )]
    internal string Macro { get; set; }

    // non Json

    /// <summary>
    /// Returns the complete Macro Command incl. the list of Macro Commands to exec
    /// </summary>
    /// <param name="macros">THe Macro collection</param>
    /// <returns>A Macro type VJCommand</returns>
    internal VJCommand MacroCmd( MacroDefList macros )
    {
      var retVal = base.Cmd; // evaluate Ext1..3

      retVal.CtrlType = VJ_ControllerType.VX_Macro;
      retVal.CtrlMacroList = macros.GetMacro( Macro );

      return retVal;
    }

 
  }
}
