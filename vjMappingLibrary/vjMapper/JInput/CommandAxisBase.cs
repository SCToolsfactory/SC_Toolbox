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
  /// CommandAxis template, defines the Axis Attributes
  /// </summary>
  [DataContract]
  internal abstract class CommandAxisBase : CommandBase
  {
    [DataMember( IsRequired = true, Name = "Direction" )]
    internal string Direction { get; set; }
    [DataMember( IsRequired = true,Name = "Value" )]
    internal short Value { get; set; }
    [DataMember( Name = "JNo" )]
    internal short JNo { get; set; }

    // non Json

    /// <summary>
    /// Handles the hit mode parameter of a command
    /// </summary>
    /// <param name="cmd">The VJCommand to modify</param>
    /// <param name="axisString">The Axis</param>
    protected void HandleAxis( ref VJCommand cmd, string axisString )
    {
      switch ( axisString.ToUpperInvariant( ) ) {
        case "X":
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_X;
          break;
        case "Y":
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_Y;
          break;
        case "Z":
          cmd.CtrlDirection = VJ_ControllerDirection.VJ_Z;
          break;
        default: // just return the default message (unknown ctrl)
          cmd.CtrlType = VJ_ControllerType.VJ_Unknown; // ERROR - unknown Axis, invalidate
          break;
      }
    }

  }
}
