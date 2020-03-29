using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vjMapper.VjOutput

{
  /// <summary>
  /// Translates a VJCommand into a SCJoyServer command string (Json format)
  /// </summary>
  internal class SCJoyServerCommand
  {

    private static string JAxisDirection( VJCommand vJ )
    {
      // "Direction": "X|Y|Z"
      switch ( vJ.CtrlDirection ) {
        case VJ_ControllerDirection.VJ_X: return $"\"Direction\": \"X\"";
        case VJ_ControllerDirection.VJ_Y: return $"\"Direction\": \"Y\"";
        case VJ_ControllerDirection.VJ_Z: return $"\"Direction\": \"Z\"";
        default: return "";
      }
    }

    private static string JPOVDirection( VJCommand vJ )
    {
      // "Direction": "c | u | r | d | l"
      switch ( vJ.CtrlDirection ) {
        case VJ_ControllerDirection.VJ_Center: return $"\"Direction\": \"c\"";
        case VJ_ControllerDirection.VJ_Up: return $"\"Direction\": \"u\"";
        case VJ_ControllerDirection.VJ_Right: return $"\"Direction\": \"u\"";
        case VJ_ControllerDirection.VJ_Down: return $"\"Direction\": \"d\"";
        case VJ_ControllerDirection.VJ_Left: return $"\"Direction\": \"l\"";
        default: return "";
      }
    }

    private static string JHitModeAndDelay( VJCommand vJ )
    {
      // "Mode": "p|r|t|s|d", "Delay":100
      switch ( vJ.CtrlDirection ) {
        case VJ_ControllerDirection.VJ_Down: return $"\"Mode\": \"p\"";
        case VJ_ControllerDirection.VJ_Up: return $"\"Mode\": \"r\"";
        case VJ_ControllerDirection.VJ_Tap:
          if ( vJ.CtrlValue_Delay == VJCommand.DEFAULT_SHORTDELAY ) {
            return $"\"Mode\": \"s\"";
          }
          else {
            return $"\"Mode\": \"t\", \"Delay\": {vJ.CtrlValue_Delay}";
          }
        case VJ_ControllerDirection.VJ_DoubleTap: return $"\"Mode\": \"d\", \"Delay\": {vJ.CtrlValue_Delay}";
        default: return "";
      }
    }

    private static string JKeyModifier( VJCommand vJ )
    {
      // "Modifier": "mod"  - [mod[&mod]] (n)one, (lc)trl, (rc)trl, (la)lt, (ra)lt, (ls)hift, (rs)hift
      string mod = "";
      if ( vJ.CtrlModifier.Count == 0 ) {
        mod = "n";
      }
      else {
        foreach ( var m in vJ.CtrlModifier ) {
          switch ( m ) {
            case VJ_Modifier.VJ_LAlt:
              mod += "la"; break;
            case VJ_Modifier.VJ_RAlt:
              mod += "ra"; break;
            case VJ_Modifier.VJ_LCtrl:
              mod += "lc"; break;
            case VJ_Modifier.VJ_RCtrl:
              mod += "rc"; break;
            case VJ_Modifier.VJ_LShift:
              mod += "ls"; break;
            case VJ_Modifier.VJ_RShift:
              mod += "rs"; break;
            default: break;
          }
          if ( m != vJ.CtrlModifier.Last( ) ) {
            mod += "&";
          }
        }

      }
      return $"\"Modifier\": \"{mod}\"";
    }

    private static string JSIndexNumber( VJCommand vJ )
    {
      // "Index": N
      return $"\"Index\": {vJ.CtrlIndex}";
    }

    private static string JSKeyCodeNumber( VJCommand vJ )
    {
      // "VKcode": n  (use the int version - less overhead)
      return $"\"VKcode\": {vJ.CtrlKeycode}";
    }

    private static string JAnalogValue( VJCommand vJ )
    {
      // "Value": number
      switch ( vJ.CtrlDirection ) {
        case VJ_ControllerDirection.VJ_X: return $"\"Value\": {vJ.CtrlValue}";
        case VJ_ControllerDirection.VJ_Y: return $"\"Value\": {vJ.CtrlValue}";
        case VJ_ControllerDirection.VJ_Z: return $"\"Value\": {vJ.CtrlValue}";
        default: return "";
      }
    }

    /// <summary>
    /// Returns a formatted Json string for a command
    /// This is following the SCJoyServer Input command syntax
    /// Expands macros into multiple commands e.g. "{cmd1} {cmd2} {cmd3}"
    /// </summary>
    /// <param name="vJ">A VJCommand to translate</param>
    /// <returns>A formatted string</returns>
    internal static string JCommand( VJCommand vJ )
    {
      string ret = "";
      switch ( vJ.CtrlType ) {
        case VJ_ControllerType.VX_Macro:
          //  a string commands
          ret = "";
          foreach ( var c in vJ.CtrlMacroList ) {
            ret += c.JString + " ";
          }
          break;
        case VJ_ControllerType.VJ_Axis:
          // { "A": {"Direction": "X|Y|Z", "Value": number } }
          ret = $"{{ \"A\": {{ {JAxisDirection(vJ)}, {JAnalogValue( vJ )} }} }}";
          break;
        case VJ_ControllerType.VJ_RotAxis:
          // { "R": {"Direction": "X|Y|Z", "Value": number } }
          ret = $"{{ \"R\": {{ {JAxisDirection( vJ )}, {JAnalogValue( vJ )} }} }}";
          break;
        case VJ_ControllerType.VJ_Slider:
          // { "S": {"Index": 1|2, "Value": number} }
          ret = $"{{ \"S\": {{ {JSIndexNumber( vJ )}, {JAnalogValue( vJ )} }} }}";
          break;
        case VJ_ControllerType.VJ_Hat:
          // { "P": {"Index": 1|2|3|4, "Direction": "c | u | r | d | l" } }   
          ret = $"{{ \"P\": {{ {JSIndexNumber( vJ )}, {JPOVDirection( vJ )} }} }}";
          break;
        case VJ_ControllerType.VJ_Button:
          // { "B": {"Index": n, "Mode": "p|r|t|s|d", "Delay":100 } } 
          ret = $"{{ \"B\": {{ {JSIndexNumber( vJ )}, {JHitModeAndDelay( vJ )} }} }}";
          break;
        case VJ_ControllerType.DX_Key:
          // { "K": {"VKcode": keyCode, "Mode": "p|r|t|s|d", "Modifier": "mod", "Delay": 100 } }  
          ret = $"{{ \"K\": {{ {JSKeyCodeNumber( vJ )}, {JHitModeAndDelay( vJ )}, {JKeyModifier( vJ )} }} }}";
          break;
        default:
          ret = "{}";
          break;
      }
      return ret;
    }

  }
}
