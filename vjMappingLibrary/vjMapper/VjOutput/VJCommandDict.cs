using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vjMapper.VjOutput
{
  /// <summary>
  /// A Dictionary of VJCommands 
  ///  Key is a string (name) to look it up
  /// </summary>
  public class VJCommandDict : Dictionary<string, VJCommand>
  {

    /// <summary>
    /// Merge one dictionary (source) into the target dictionary
    /// </summary>
    /// <param name="target">ref target dictionary</param>
    /// <param name="source">The source dictionary</param>
    public static void Merge( ref VJCommandDict target, VJCommandDict source )
    {
      // may be there is a more clever way but Concat etc does not work for this
      foreach ( var kv in source ) {
        try {
          target.Add( kv.Key, kv.Value );
        }
        catch {
          ; // duplicate IDs .. likely
        }
      }

    }

    /// <summary>
    /// Merge one dictionary (source) into this dictionary
    /// </summary>
    /// <param name="source">The source dictionary</param>
    public void Append(VJCommandDict source )
    {
      foreach ( var kv in source ) {
        try {
          this.Add( kv.Key, kv.Value );
        }
        catch {
          ; // duplicate IDs .. likely
        }
      }

    }


  }
}
