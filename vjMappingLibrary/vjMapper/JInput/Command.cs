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
  /// Enveloppe of one command - can be any of the supported ones (base class in Json)
  /// </summary>
  [DataContract]
  public class Command
  {
    /*
      COMMAND format:

      Macros:
	      Macro:    { "M": {"Macro": "mName", "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }  
	           - mName - a macro name defined in the Macros section

      Joystick:
        Axis:     { "A": {"Direction": "X|Y|Z", "Value": number, "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr"} }
                    - number => 0..1000 (normalized)
    
        RotAxis:  { "R": {"Direction": "X|Y|Z", "Value": number, "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr"} }
                    - number => 0..1000 (normalized)
    
        Slider:   { "S": {"Index": 1|2, "Value": number, "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr"} }
                    - number => 0..1000 (normalized)
    
        POV:      { "P": {"Index": 1|2|3|4, "Direction": "c | u | r | d | l", "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }   
                    - Index n=> 1..MaxPOV (setup of vJoy, max = 4)
                    - Direction either of the chars (center (released), up, right, donw, left)

        Button:   { "B": {"Index": n, "Mode": "p|r|t|s|d", "Delay": msec, "JNo": j, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } } 
                    - Button Index n => 1..VJ_MAXBUTTON (setup of vJoy - SC supports 60)
                    - Mode optional - either of the chars (see below)

        Keyboard:
          Key:      { "K": {"VKcodeEx": "keyName", "VKcode": n, "Mode": "p|r|t|s|d", "Modifier": "mod", "Delay": msec, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }  
                      - VKcodeEx "s" either a number n=> 1..255 or a WinUser VK_.. literal (see separate Reference file)
                      - VKcode n=> 1..255 WinUser VK_.. (see separate Reference file)
                         if both are found the VKcodeEx item gets priority and the VKcode element is ignored
                         if none is found the command is ignored
                      - Mode optional - either of the chars (see below)
                      - Modifier optional - a set of codes (see below)

        Other:
          Ext:    { "E": { "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }    // trigger only extended commands

      
         - Mode:     [mode]      (p)ress, (r)elease, (t)ap, (s)hort tap, (d)ouble tap           (default=tap - short tap is a tap with almost no delay)
         - Modifier: [mod[&mod]] (n)one, (lc)trl, (rc)trl, (la)lt, (ra)lt, (ls)hift, (rs)hift   (default=none - concat modifiers with & char)
         - Delay:    [delay]      nnnn  milliseconds, optional for Tap and Double Tap           (default=150 VJCommand.DEFAULT_DELAY, max 20_000 msec)     
         - JNo:      [joyNumber] >0 Joystick number                                             (default=1)
         - Ext1.3:   [string]    Optional user extensions that must be handled by the caller
    */

    // either of the one below
    [DataMember]
    internal CommandMacro M { get; set; }
    [DataMember]
    internal CommandAxis A { get; set; }
    [DataMember]
    internal CommandRotAxis R { get; set; }
    [DataMember]
    internal CommandSlider S { get; set; }
    [DataMember]
    internal CommandPov P { get; set; }
    [DataMember]
    internal CommandButton B { get; set; }
    [DataMember]
    internal CommandKey K { get; set; }
    [DataMember]
    internal CommandExt E { get; set; }

    // non Json

    /// <summary>
    /// Returns the Command found in the Json content
    /// </summary>
    /// <param name="macros">A MacroDefList (used to complete Macro commands)</param>
    /// <returns>A valid VJCommand or </returns>
    public VJCommand VJCommand( MacroDefList macros )
    {
      if ( M != null ) return M.MacroCmd( macros );// use the Macro method here
      if ( A != null ) return A.Cmd;
      if ( R != null ) return R.Cmd;
      if ( S != null ) return S.Cmd;
      if ( P != null ) return P.Cmd;
      if ( B != null ) return B.Cmd;
      if ( K != null ) return K.Cmd;
      if ( E != null ) return E.Cmd;
      return new VJCommand( ); // return a default one (is marked Invalid though)
    }
  }
}
