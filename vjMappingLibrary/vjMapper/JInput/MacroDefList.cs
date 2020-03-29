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
  /// The list of all defined Macros
  /// </summary>
  public class MacroDefList : List<MacroDef>
  {

    /// <summary>
    /// Returns the Macro as VJCommandList
    /// </summary>
    /// <param name="mName">The Macro name</param>
    /// <returns>A VJCommandList (can be empty)</returns>
    public VJCommandList GetMacro( string mName )
    {
      var mac = this.Where( x => x.MName == mName );
      if ( mac != null ) {
        return mac.FirstOrDefault( ).VJCommandList( );
      }

      return new VJCommandList( ); // an empty one
    }

  }
}
