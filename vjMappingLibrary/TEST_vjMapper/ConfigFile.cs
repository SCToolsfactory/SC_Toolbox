using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using vjMapper.JInput;
using vjMapper.VjOutput;

namespace TEST_vjMapper
{

  /// <summary>
  /// The Device Mapping File
  /// </summary>
  [DataContract]
  internal class ConfigFile
  {
    /*
        {
          "_Comment" : "SwitchPanel Config File",
          "MapName" : "AnyNameWillDo",
      	  "Macros"   : [MACRO,..],
          "SwitchMap": [
              { "Input": "MASTER_BATT",       "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "MASTER_ALT",        "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "AVIONICS_MASTER",   "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "FUEL_PUMP",         "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "DE_ICE",            "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "PITOT_HEAT",        "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "GEAR",              "Cmd" : [ COMMAND_on_up, COMMAND_off_down ] },
              { "Input": "COWL_CLOSE",        "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "PANEL",             "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "BEACON",            "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "NAV",               "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "STROBE",            "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "TAXI",              "Cmd" : [ COMMAND_on, COMMAND_off ] },
              { "Input": "LANDING",           "Cmd" : [ COMMAND_on, COMMAND_off ] }
            ],
          "Rotary": 
              { "Input": "key",               "Cmd" : [COMMAND_posOff, COMMAND_posR, COMMAND_posL, COMMAND_posAll, COMMAND_posStart] }
        }
        see below for COMMAND
    */

    // public members
    [DataMember]
    public string _Comment { get; set; }
    [DataMember( IsRequired = true )]
    public string MapName { get; set; }

    // private members
    [DataMember] // optional
    private MacroDefList Macros { get; set; } = new MacroDefList( );
    [DataMember] // optional
    private InputSwitchList SwitchMap { get; set; } = new InputSwitchList ( );
    [DataMember] // optional
    private InputRotary Rotary { get; set; } = new InputRotary( );


    // non Json

    /// <summary>
    /// Return the dictionary of commands 
    /// </summary>
    /// <returns>A Dictionary with Input NAME as Key and the corresponding VJCommand</returns>
    public VJCommandDict VJCommandDict
    {
      get {
        // combine all commands
        var ret = Rotary.VJCommands( Macros );              // get rotary
        ret.Append( SwitchMap.VJCommandDict( Macros ) );    // get switches

        return ret; // all collected commands
      }

    }

  }





}