using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vjAction
{
  /// <summary>
  /// A ping event that indicates a successfull issue of a command
  /// Arg is the command no issued so far (1..)
  /// </summary>
  public class vjActionEventArgs : EventArgs
  {
    /// <summary>
    /// Command handled Ping
    /// </summary>
    /// <param name="arg">command number issued</param>
    public vjActionEventArgs( int arg )
    {
      Arg = arg;
    }

    public int Arg { get; private set; }

  }
}
