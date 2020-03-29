using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using vjMapper.DxKbd;
using vjMapper.VjOutput;

namespace vjMapper.JInput
{
  /// <summary>
  /// Key Command
  /// </summary>
  [DataContract]
  internal class CommandKey : CommandBase
  {
    /*
          Key:      { "K": {"VKcodeEx": "keyName", "VKcode": n, "Mode": "p|r|t|s|d", "Modifier": "mod", "Delay": 100, "Ext1": "extstr", "Ext2": "extstr", "Ext3": "extstr" } }  
                      - VKcodeEx "s" either a number n=> 1..255 or a WinUser VK_.. literal (see separate Reference file)
                      - VKcode n=> 1..255 WinUser VK_.. (see separate Reference file)
                         if both are found the VKcodeEx item gets priority and the VKcode element is ignored
                         if none is found the command is ignored
                      - Mode optional - either of the chars (see below)
                      - Modifier optional - a set of codes (see below)
    */
    [DataMember( Name = "VKcodeEx" )]
    internal string VKcodeEx { get; set; }
    [DataMember( Name = "VKcode" )]
    internal int VKcode { get; set; } = 0;
    [DataMember( Name = "Mode" )]
    internal string Mode { get; set; }
    [DataMember( Name = "Modifier" )]
    internal string Modifier { get; set; }
    [DataMember( Name = "Delay" )]
    internal short Delay { get; set; }

    // non Json

    /// <summary>
    /// Returns the VJCommand for this item
    /// </summary>
    internal override VJCommand Cmd
    {
      get {
        var retVal = base.Cmd; // evaluate Ext1..3

        // either a number or a keyname
        if ( !string.IsNullOrEmpty( VKcodeEx ) ) {
          // VKcodeEx has priority if given
          if ( int.TryParse( VKcodeEx, out int code ) ) {
            VKcode = code; 
          }
          else {
            VKcode = SCdxKeycodes.KeyCodeFromKeyName( VKcodeEx );
          }
        }
        // merged VKCodeEx into VKcode if it was supplied, else we take VKCode
        retVal.CtrlIndex_keycode = VKcode;

        if ( ( retVal.CtrlIndex_keycode < 1 ) || ( retVal.CtrlIndex_keycode > 0xff ) ) {
          return retVal; // ERROR - bail out on invalid number
        }

        retVal.CtrlType = VJ_ControllerType.DX_Key;
        if ( !string.IsNullOrEmpty( Modifier ) ) {
          // treat multiple modifiers
          string[] e = Modifier.ToLowerInvariant( ).Split( new char[] { '&' } );
          for ( int i = 0; i < e.Length; i++ ) {
            switch ( e[i] ) {
              case "lc": // leftCtrl
                retVal.CtrlModifier.Add( VJ_Modifier.VJ_LCtrl );
                break;
              case "rc": // rightCtrl
                retVal.CtrlModifier.Add( VJ_Modifier.VJ_RCtrl );
                break;
              case "la": // leftAlt
                retVal.CtrlModifier.Add( VJ_Modifier.VJ_LAlt );
                break;
              case "ra": // rightAlt
                retVal.CtrlModifier.Add( VJ_Modifier.VJ_RAlt );
                break;
              case "ls": // leftShift
                retVal.CtrlModifier.Add( VJ_Modifier.VJ_LShift );
                break;
              case "rs": // rightShift
                retVal.CtrlModifier.Add( VJ_Modifier.VJ_RShift );
                break;
              default: // none                
                break;
            }
          }
        }

        HandleDelay( ref retVal, Delay );
        HandleMode( ref retVal, Mode );

        string j = retVal.JString; // create the Json command string (no lazy init)
        return retVal;
      }
    }
  }//CommandKey
}
