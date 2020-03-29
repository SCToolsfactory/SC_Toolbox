using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using vjMapper.VjOutput;

namespace vjMapper.JInput
{
  public class InputRotary : InputBase
  {
    /*
          { "Input": "key", "Cmd" : [COMMAND_pos0, COMMAND_pos1, COMMAND_pos2, COMMAND_pos3, COMMAND_pos4, ..] }     
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
      var ret = new VJCommandDict( );

      int rotaryPos = 0;
      foreach ( var rc in Cmd ) {
        string key = Rotary_Pos(Input, rotaryPos );
        ret.Add( key, rc.VJCommand( macros ) );
        rotaryPos++;
      }
      return ret;
    }

    /// <summary>
    /// Create the lookup name for a Rotary Position
    /// </summary>
    /// <param name="input">The Input name</param>
    /// <param name="pos">The rotary position</param>
    /// <returns>The lookup name for this Input</returns>
    public static string Rotary_Pos(string input, int pos )
    {
      return $"{input}_{pos}";
    }

  }
}
