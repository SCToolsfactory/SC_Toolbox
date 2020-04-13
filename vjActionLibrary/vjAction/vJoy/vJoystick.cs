using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vJoyInterfaceWrap;

namespace vjAction.vJoy
{
  /// <summary>
  /// The POV Direction Enum
  /// </summary>
  public enum POVType
  {
    Up = 0,     // N
    Right = 1,  // E
    Down = 2,   // S
    Left = 3,   // W
    Nil = -1,     // Center
  };

  /// <summary>
  /// Implements one vJoy device
  /// </summary>
  internal class vJoystick
  {

    // convert to and from -32768 .. 32767 base on external range 0...1000



    // Class


    // State and BackStore for all control items
    private class xState
    {
      public const int MAXOUT = 1000;

      public HID_USAGES usage = HID_USAGES.HID_USAGE_POV;
      public bool has = false;
      public int pindex = 0; // POV index 1..4, button index 1..128
      public int min = 0;
      public int max = 0;
      public int current = 0;
      public double range = 0.0;

      // set current
      public void ToJS( int value )
      {
        // make sure we reach the borders..
        if ( value == 0 ) current = min;
        if ( value == MAXOUT ) current = max;

        int ret = (int)( value * range + min );
        // make sure we are in range
        if ( ret < min ) current = min;
        if ( ret > max ) current = max;
        // just return the result
        current = ret;
      }

      // get from current
      public int FromJS()
      {
        // make sure we reach the borders..
        if ( current == min ) return 0;
        if ( current == max ) return MAXOUT;

        int ret = (int)( ( current - min ) / range );
        // make sure we are in range
        if ( ret < 0 ) return 0;
        if ( ret > MAXOUT ) return MAXOUT;
        // just return the result
        return ret;
      }

    }

    // Class internals 

    private vJoyInterfaceWrap.vJoy m_joy = null;
    private string m_prodS = "";
    private int m_nButtons = 0;
    private int m_nPov = 0; // discrete POV only
    private xState m_hasX = new xState( );
    private xState m_hasY = new xState( );
    private xState m_hasZ = new xState( );
    private xState m_hasRX = new xState( );
    private xState m_hasRY = new xState( );
    private xState m_hasRZ = new xState( );
    private xState m_hasSL1 = new xState( );
    private xState m_hasSL2 = new xState( );
    private xState[] m_hasPov = new xState[1 + 4]; // [0] is not used
    private xState[] m_hasBtn = new xState[1 + 128];// [0] is not used


    // get the state from the vJ instance
    private xState GetState( HID_USAGES usage, int pindex = 0 )
    {
      var ret = new xState { usage = usage, current = 0 };

      if ( usage == HID_USAGES.HID_USAGE_POV ) {
        ret.has = ( pindex > 0 && pindex <= m_nPov );
        ret.pindex = pindex;
        ret.current = (int)POVType.Nil;
      }
      else if ( usage == HID_USAGES.HID_USAGE_WHL ) { // button
        ret.has = ( pindex > 0 && pindex <= m_nPov );
        ret.pindex = pindex;
        ret.current = 0; // == false...
      }
      else {
        ret.has = m_joy.GetVJDAxisExist( Index, usage );
        if ( ret.has ) {
          long val = 0;
          if ( m_joy.GetVJDAxisMin( Index, usage, ref val ) ) ret.min = (int)val;
          if ( m_joy.GetVJDAxisMax( Index, usage, ref val ) ) ret.max = (int)val;
          ret.range = (double)( ret.max - ret.min ) / xState.MAXOUT;
        }
      }

      return ret;
    }

    /// <summary>
    /// cTor: Create an instance of a vJoy Device
    /// </summary>
    /// <param name="joy"></param>
    /// <param name="jID"></param>
    public vJoystick( vJoyInterfaceWrap.vJoy joy, uint jID )
    {
      m_joy = joy;
      Index = jID;

      m_nButtons = m_joy.GetVJDButtonNumber( Index );
      m_nPov = m_joy.GetVJDDiscPovNumber( Index );

      m_prodS = m_joy.GetvJoyProductString( );
      m_hasX = GetState( HID_USAGES.HID_USAGE_X );
      m_hasY = GetState( HID_USAGES.HID_USAGE_Y );
      m_hasZ = GetState( HID_USAGES.HID_USAGE_Z );

      m_hasRX = GetState( HID_USAGES.HID_USAGE_RX );
      m_hasRY = GetState( HID_USAGES.HID_USAGE_RY );
      m_hasRZ = GetState( HID_USAGES.HID_USAGE_RZ );

      m_hasSL1 = GetState( HID_USAGES.HID_USAGE_SL0 );
      m_hasSL2 = GetState( HID_USAGES.HID_USAGE_SL1 );

      for ( int i = 1; i <= m_nPov; i++ ) {
        m_hasPov[i] = GetState( HID_USAGES.HID_USAGE_POV, i );
      }
      for ( int i = 1; i <= m_nButtons; i++ ) {
        m_hasBtn[i] = GetState( HID_USAGES.HID_USAGE_WHL, i ); // Use WHL as button qualifier...
      }
    }

    /// <summary>
    /// vJoy Instance index
    /// </summary>
    public uint Index { get; } = 0;

    /// <summary>
    /// XAxis property
    /// </summary>
    public int XAxis
    {
      get => m_hasX.FromJS( );
      set {
        m_hasX.ToJS( value );
        m_joy.SetAxis( m_hasX.current, Index, m_hasX.usage );
      }
    }

    /// <summary>
    /// YAxis property
    /// </summary>
    public int YAxis
    {
      get => m_hasY.FromJS( );
      set {
        m_hasY.ToJS( value );
        m_joy.SetAxis( m_hasY.current, Index, m_hasY.usage );
      }
    }

    /// <summary>
    /// ZAxis property
    /// </summary>
    public int ZAxis
    {
      get => m_hasZ.FromJS( );
      set {
        m_hasZ.ToJS( value );
        m_joy.SetAxis( m_hasZ.current, Index, m_hasZ.usage );
      }
    }

    /// <summary>
    /// RotXAxis property
    /// </summary>
    public int XRotAxis
    {
      get => m_hasRX.FromJS( );
      set {
        m_hasRX.ToJS( value );
        m_joy.SetAxis( m_hasRX.current, Index, m_hasRX.usage );
      }
    }

    /// <summary>
    /// RotYAxis property
    /// </summary>
    public int YRotAxis
    {
      get => m_hasRY.FromJS( );
      set {
        m_hasRY.ToJS( value );
        m_joy.SetAxis( m_hasRY.current, Index, m_hasRY.usage );
      }
    }

    /// <summary>
    /// RotZAxis property
    /// </summary>
    public int ZRotAxis
    {
      get => m_hasRZ.FromJS( );
      set {
        m_hasRZ.ToJS( value );
        m_joy.SetAxis( m_hasRZ.current, Index, m_hasRZ.usage );
      }
    }

    /// <summary>
    /// Slider1 property
    /// </summary>
    public int Slider1
    {
      get => m_hasSL1.FromJS( );
      set {
        m_hasSL1.ToJS( value );
        m_joy.SetAxis( m_hasSL1.current, Index, m_hasSL1.usage );
      }
    }

    /// <summary>
    /// Slider2 property
    /// </summary>
    public int Slider2
    {
      get => m_hasSL2.FromJS( );
      set {
        m_hasSL2.ToJS( value );
        m_joy.SetAxis( m_hasSL2.current, Index, m_hasSL2.usage );
      }
    }


    /// <summary>
    /// Set the POV (vJoy DiscretePOV 1..4 - if set..)
    /// </summary>
    /// <param name="povIndex">The POV 1..4</param>
    /// <param name="direction">The POV direction</param>
    public void SetPOVDirection( int povIndex, POVType direction )
    {
      if ( povIndex < 1 || povIndex > m_nPov ) return;
      if ( m_hasPov[povIndex].has ) {
        m_hasPov[povIndex].current = (int)direction; // read back storage
        m_joy.SetDiscPov( m_hasPov[povIndex].current, Index, (uint)povIndex );
      }
    }

    /// <summary>
    /// Get the POV (vJoy DiscretePOV 1..4 - if set..)
    /// </summary>
    /// <param name="povIndex">The POV 1..4</param>
    /// <returns>The POV value</returns>
    public POVType GetPOVDirection( int povIndex )
    {
      if ( povIndex < 1 || povIndex > m_nPov ) return POVType.Nil;
      return (POVType)( m_hasPov[povIndex].current );
    }

    /// <summary>
    /// Set a button (vJoy 1..n)
    /// </summary>
    /// <param name="buttonIndex">The button 1..n</param>
    /// <param name="press">true for Button Down, false for Button Up</param>
    public void SetButtonState( int buttonIndex, bool press )
    {
      if ( buttonIndex < 1 || buttonIndex > m_nButtons ) return;
      if ( press ) {
        m_hasBtn[buttonIndex].current = 1; // read back storage
        m_joy.SetBtn( true, Index, (uint)buttonIndex );
      }
      else {
        m_hasBtn[buttonIndex].current = 0; // read back storage
        m_joy.SetBtn( false, Index, (uint)buttonIndex );
      }
    }

    /// <summary>
    /// Get a button (vJoy 1..n)
    /// </summary>
    /// <param name="buttonIndex">The button 1..n</param>
    /// <returns>true for Button Down, false for Button Up</returns>
    public bool GetButtonState( int buttonIndex )
    {
      if ( buttonIndex < 1 || buttonIndex > m_nButtons ) return false;
      return ( m_hasPov[buttonIndex].current == 1 ) ? true : false;
    }

  }
}
