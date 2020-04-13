using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using vjMapper.VjOutput;
using vjMapper;
using vjMapper.JInput;

namespace vjAction.Commands
{
  /// <summary>
  ///  Defines the JSON syntax of the commands received from the client
  ///  provides methods to get valid VJ_Commands from
  /// </summary>
  public sealed class VJoyCmdParser
  {
    /// <summary>
    /// Extracts a top level '{ anything }' element from a Json string
    /// </summary>
    /// <param name="jsInput">The input string</param>
    /// <param name="fragment">out the extracted fragment</param>
    /// <param name="jsRemaining">out the input - the extracted part</param>
    /// <returns></returns>
    private static bool ExtractFragment( string jsInput, out string fragment, out string jsRemaining )
    {
      fragment = ""; jsRemaining = jsInput;
      // do some houskeeping first
      if ( string.IsNullOrWhiteSpace( jsInput ) ) return false; // no usable content

      jsInput = jsInput.Replace( "\n", "" ).Replace( "\r", "" ).TrimStart( ); // cleanup any CR, LFs and whitespaces

      int endPos = 0;
      int bOpen = 0; bool triggered = false;
      if ( jsInput.IndexOf( "{" ) != 0 ) return false; // seems not having a starting { item..

      for ( int i = 0; i < jsInput.Length; i++ ) {
        if ( jsInput[i] == '{' ) {
          bOpen++; triggered = true;
        }
        else if ( triggered && jsInput[i] == '}' ) {
          bOpen--;
        }
        if ( triggered && bOpen == 0 ) {
          endPos = i;
          triggered = false;
          break; // no further reading needed
        }
      }
      if ( endPos > 0 ) {
        // extract
        fragment = jsInput.Substring( 0, endPos + 1 );
        jsRemaining = jsInput.Substring( endPos + 1 );
        return true;
      }
      return false;
    }


    /// <summary>
    /// Detects a message and returns it, removes the returned one from the inBuffer
    /// </summary>
    /// <param name="inBuffer">The stream buffer</param>
    /// <returns>A message or an Empty string</returns>
    private static string ExtractMessage( ref string inBuffer )
    {
      // garbage cleaner..
      if ( inBuffer.IndexOf( '\0' ) >= 0 ) inBuffer = inBuffer.Substring( 0, inBuffer.IndexOf( '\0' ) ); // kill Nul chars in the string

      string retVal = "";
      if ( ExtractFragment( inBuffer, out string msg, out string remain ) ) {
        // there is at least one complete message
        retVal = msg; // the current message
        inBuffer = remain; // return any remaining ones
      }
      return retVal;
    }

    /// <summary>
    /// Decompose the input Command 
    /// </summary>
    /// <param name="cmd">A Command obj</param>
    /// <returns>A VJCommand obj</returns>
    private static VJCommand ParseMessage( Command cmd )
    {
      if ( cmd == null ) return new VJCommand( ); // No Command - bail out Valid=> false;
      return cmd.VJCommand( new MacroDefList( ) ); // decomposes the JSON object into our VJ_Command
    }


    /// <summary>
    /// Reads from the open stream and returns a VJoyCmdParser entry
    /// </summary>
    /// <param name="jStream">An open stream at position</param>
    /// <returns>A VJCommand obj</returns>
    public static VJCommand FromJson( Stream jStream )
    {
      return ParseMessage( vjMapping.FromJsonStream<Command>( jStream ) );
    }

    /// <summary>
    /// Reads from the string and returns a VJoyCmdParser entry
    /// </summary>
    /// <param name="jString">An Json string</param>
    /// <returns>A VJCommand obj</returns>
    public static VJCommand FromJson( string jString )
    {
      return ParseMessage( vjMapping.FromJsonString<Command>( jString ) );
    }


    /// <summary>
    /// Returns the next message decoded from the buffer as command
    /// removes the translated message from the buffer
    /// </summary>
    /// <param name="inBuffer">A string buffer</param>
    /// <returns>A VJCommand</returns>
    static public VJCommand FromJsonBuffer( ref string inBuffer )
    {
      string msg = ExtractMessage( ref inBuffer );
      return FromJson( msg );
    }


  }
}
