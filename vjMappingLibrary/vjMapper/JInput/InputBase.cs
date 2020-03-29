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
  /// Base for command lists for a keyword (Input)
  /// </summary>
  [DataContract]
 abstract public class InputBase
  {
    /*
      { "Input": "keyName",       "Cmd" : [ COMMANDlist ] }     
    */
    [DataMember( IsRequired = true, Name = "Input" )]
    public string Input { get; set; }
    [DataMember( IsRequired = true, Name ="Cmd" )]
    public CommandList Cmd { get; set; } = new CommandList( );

    // non Json

    /// <summary>
    /// Returns the command dictionary of the Cmd part
    /// Note to implementors - need to do this in the derived class
    /// </summary>
    /// <param name="macros">The macro List</param>
    /// <returns>An empty VJCommandDict</returns>
    virtual public VJCommandDict VJCommands( MacroDefList macros )
    {
      return new VJCommandDict( ); // base class returns an empty one..
    }

  }
}
