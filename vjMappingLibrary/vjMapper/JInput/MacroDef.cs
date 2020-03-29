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
  /// Definition of a Macro
  /// </summary>
  [DataContract]
  public class MacroDef
  {
    /*
        { "MName": "mName", "CmdList": [COMMAND] }  COMMAND or a list of COMMAND,COMMAND,...

     */
    [DataMember( IsRequired = true, Name = "MName" )]
    internal string MName { get; set; }
    [DataMember( IsRequired = true,Name = "CmdList" )]
    internal List<Command> CmdList { get; set; } = new List<Command>( );

    // non Json

    /// <summary>
    /// returns a list of commands to exec for this macro
    /// </summary>
    /// <returns>A VJCommandList</returns>
    public VJCommandList VJCommandList()
    {
      var ret = new VJCommandList( );
      foreach ( var cmd in CmdList ) {
        var vj = cmd.VJCommand( new MacroDefList( ) ); // nested macros are not supported..
        string j = vj.JString;    // create the Json command string (no lazy init)
        ret.Add( vj ); 
      }
      return ret;
    }

  }
}
