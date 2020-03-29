using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace vjMapper
{
  public class vjMapping
  {
    // create a stream from a string
    private static Stream StreamFromString( string s )
    {
      var stream = new MemoryStream( );
      var writer = new StreamWriter( stream );
      writer.Write( s );
      writer.Flush( );
      stream.Position = 0;
      return stream;
    }

    /// <summary>
    /// Provides information about De-Serialization problems
    /// </summary>
    public static string ErrorMsg { get; private set; }
    
    /// <summary>
    /// De-serializes from the open stream one T type entry
    /// </summary>
    /// <param name="jStream">An open stream at position</param>
    /// <returns>A T type obj or default(T) i.e.  null for errors, see ErrorMessage for details</returns>
    public static T FromJsonStream<T>( Stream jStream )
    {
      try {
        var jsonSerializer = new DataContractJsonSerializer( typeof( T ) );
        object objResponse = jsonSerializer.ReadObject( jStream );
        return (T)objResponse;
      }
      catch ( Exception e ) {
        ErrorMsg = e.Message;
        return default(T);
      }
    }

    /// <summary>
    /// De-serializes from the file one T type entry
    /// </summary>
    /// <param name="jFilename">The Json Filename</param>
    /// <returns>A T type obj or default(T) for errors</returns>
    public static T FromJsonFile<T>( string jFilename )
    {
      T c = default( T );
      if ( File.Exists( jFilename ) ) {
        using ( var ts = File.OpenRead( jFilename ) ) {
          c = FromJsonStream<T>( ts );
        }
      }
      return c;
    }

    /// <summary>
    /// De-serializes from the string one T type entry
    /// </summary>
    /// <param name="json">The Json string obj</param>
    /// <returns>A T type obj or default(T) for errors</returns>
    public static T FromJsonString<T>( string json )
    {
      T c = default( T );
      using ( var ts = StreamFromString( json ) ) {
        c = FromJsonStream<T>( ts );
      }
      return c;
    }


  }
}
