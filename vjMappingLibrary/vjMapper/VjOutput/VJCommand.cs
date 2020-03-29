using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vjMapper.VjOutput
{
  /// <summary>
  /// The Controller type of a command
  /// </summary>
  public enum VJ_ControllerType
  {
    VJ_Unknown = -1,
    VJ_Button = 0,  // a button to actuate
    VJ_Axis,        // an Axis to actuate
    VJ_RotAxis,     // a RotAxis to actuate
    VJ_Slider,      // a Slider to actuate
    VJ_Hat,         // a Hat(POV) to actuate

    DX_Key,         // a key to type

    VX_Macro,       // a macro to execute
  }

  /// <summary>
  /// Modifier for Key commands
  /// </summary>
  public enum VJ_Modifier
  {
    VJ_None = 0,
    VJ_LCtrl,
    VJ_RCtrl,
    VJ_LAlt,
    VJ_RAlt,
    VJ_LShift,
    VJ_RShift,
  }

  /// <summary>
  /// The command behavior for the different type of Controllers
  /// </summary>
  public enum VJ_ControllerDirection
  {
    VJ_NotUsed = 0,

    VJ_X,         // Axis
    VJ_Y,         // Axis
    VJ_Z,         // Axis

    VJ_Center,    // POV
    VJ_Left,      // POV
    VJ_Right,     // POV
    VJ_Up,        // POV or button or key
    VJ_Down,      // POV or button or key
    VJ_Tap,       // button or key
    VJ_DoubleTap, // button or key
  }

  #region VJCommand

  /// <summary>
  /// The Command class which contains the in memory definiton of commands
  /// supports an outgoing Json command string for the SCJoyServer
  /// </summary>
  public class VJCommand
  {
    private string m_jString = "";  // store for the Json command string to send
    private List<VJ_Modifier> m_modifiers = new List<VJ_Modifier>( ); // store for the macrolist

    // Defaults
    internal const int VJ_MAXBUTTON = 60;  // the last allowed button number
    internal const int DEFAULT_DELAY = 150; // msec
    internal const int DEFAULT_SHORTDELAY = 5; // msec - short tap const

    /// <summary>
    /// The controller e.g. Button, Axis, etc.
    /// </summary>
    public VJ_ControllerType CtrlType { get; set; } = VJ_ControllerType.VJ_Unknown;

    /// <summary>
    /// The controller direction e.g. Axis X,Y,Z or POV direction or button up or down
    /// </summary>
    public VJ_ControllerDirection CtrlDirection { get; set; } = VJ_ControllerDirection.VJ_NotUsed;

    
    /// <summary>
    /// The controller modifiers 
    /// </summary>
    public IList<VJ_Modifier> CtrlModifier { get => m_modifiers; }

    /// <summary>
    /// The index of the controller 1..n
    /// for K it is the Key Integer VK_Key
    /// </summary>
    internal int CtrlIndex_keycode { get; set; } = 0;
    /// <summary>
    /// The index of the controller 1..n
    /// </summary>
    public int CtrlIndex { get => CtrlIndex_keycode; }
    /// <summary>
    /// The Key Integer VK_Key
    /// </summary>
    public int CtrlKeycode { get => CtrlIndex_keycode; }

    /// <summary>
    /// The value of the controller 1..1000
    /// for B and K  Taps it is the delay in mseconds 
    /// </summary>
    internal int CtrlValue_Delay { get; set; } = 0;
    /// <summary>
    /// The value of the controller 1..1000
    /// </summary>
    public int CtrlValue { get => CtrlValue_Delay; }
    /// <summary>
    /// The delay in mseconds 
    /// </summary>
    public int CtrlDelay { get => CtrlValue_Delay; }

    /// <summary>
    /// The JNo (Joystick number)
    /// </summary>
    public int CtrlJNo { get; set; } = 1;

    /// <summary>
    /// A list of commands to exec if it is a macro command
    /// </summary>
    public VJCommandList CtrlMacroList { get; set; } = new VJCommandList( );

    // EXTRAS for User Extensions

    /// <summary>
    /// User Extension parameter 1 (a string)
    /// </summary>
    public string CtrlExt1 { get; set; } = "";
    /// <summary>
    /// User Extension parameter 2 (a string)
    /// </summary>
    public string CtrlExt2 { get; set; } = "";
    /// <summary>
    /// User Extension parameter 3 (a string)
    /// </summary>
    public string CtrlExt3 { get; set; } = "";

    
    // State Items

    /// <summary>
    /// Returns true for a valid command
    /// </summary>
    public bool IsValid { get => this.CtrlType != VJ_ControllerType.VJ_Unknown; }
    
    /// <summary>
    /// true if it is a Joystick Command
    /// </summary>
    public bool IsVJoyCommand { get => this.CtrlType != VJ_ControllerType.DX_Key; }

    /// <summary>
    /// true if it is a Key Command
    /// </summary>
    public bool IsKeyCommand { get => this.CtrlType == VJ_ControllerType.DX_Key; }

    /// <summary>
    /// true if it is a Macro Command
    /// </summary>
    public bool IsMacroCommand { get => this.CtrlType == VJ_ControllerType.VX_Macro; }


    // SCJoyServer support (Json command string)

    /// <summary>
    /// Returns the command string for any command ready to send to SCJoyServer
    /// </summary>
    public string JString
    {
      get {
        // create the string if it was empty before (exec JCommand only once)
        // The command is not supposed to change once created, may be make it immutable ??
        if ( string.IsNullOrEmpty( m_jString ) ) {
          m_jString = SCJoyServerCommand.JCommand(this);
        }
        return m_jString;
      }
    }


  }

  #endregion

}

