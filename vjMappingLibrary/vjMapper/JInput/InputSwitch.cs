using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using vjMapper.VjOutput;

namespace vjMapper.JInput
{
  [DataContract]
  public class InputSwitch : InputBase
  {
    /*
      { "Input": "keyword",       "Cmd" : [ COMMAND_on, COMMAND_off ] }
    */

    // Attributes in InputBase

    // non Json

    /// <summary>
    /// Returns the command dictionary of the Cmd part
    /// </summary>
    /// <param name="macros">The macro List</param>
    /// <returns>A VJCommandDict with the positional commands</returns>
    public override VJCommandDict VJCommands( MacroDefList macros )
    {
      // derives the switch ON and OFF state from the parsed content
      var ret = new VJCommandDict( );
      if ( Cmd.Count > 1 ) {
        ret.Add( Input_Off( this.Input ), Cmd[1].VJCommand( macros ) );
      }
      if ( Cmd.Count > 0 ) {
        ret.Add( Input_On( this.Input ), Cmd[0].VJCommand( macros ) );
      }
      return ret;
    }

    /// <summary>
    /// Create the lookup name for a Switch Input ON
    /// </summary>
    /// <param name="input">The Input name</param>
    /// <returns>The lookup name for this Input</returns>
    public static string Input_On( string input )
    {
      return input + "_on";
    }

    /// <summary>
    /// Create the lookup name for a Switch Input OFF
    /// </summary>
    /// <param name="input">The Input name</param>
    /// <returns>The lookup name for this Input</returns>
    public static string Input_Off( string input )
    {
      return input + "_off";
    }


  }

}
