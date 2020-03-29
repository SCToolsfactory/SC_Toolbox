using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vjMapper.VjOutput;

namespace vjMapper.JInput
{
  /// <summary>
  /// A list of InputSwitche(s)
  /// </summary>
  public class InputSwitchList : List<InputSwitch>
  {

    /// <summary>
    /// Return the dictionary of commands for the list
    /// </summary>
    /// <returns>A Dictionary with Input NAME as Key and the corresponding VJCommand</returns>
    public VJCommandDict VJCommandDict( MacroDefList macros )
    {
      var ret = new VJCommandDict( );

      // get switch commands
      foreach ( var sm in this ) {
        var cmds = sm.VJCommands( macros ); // gets the On/Off commands for this Input
        // ignore existing Inputs (don't complain...)
        foreach ( var c in cmds ) {
          if ( !ret.ContainsKey( c.Key ) )
            ret.Add( c.Key, c.Value );
        }
      }
      return ret;
    }

  }
}
