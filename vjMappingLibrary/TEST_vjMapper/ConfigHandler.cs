using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_vjMapper
{
  class ConfigHandler
  {

    private string m_configFile = "";


    private static string ConfigFileString()
    {
      return
@"{
  ""_Comment"" : ""SwitchPanel Config File"",
  ""MapName"": ""SwitchPanel Sample Config File"",
  ""Macros"": [
      {
          ""MName"": ""_MacroOn"",
          ""CmdList"": [ { ""A"": {""Direction"": ""X"", ""Value"": 1000}}, { ""A"": {""Direction"": ""Y"", ""Value"": 1000}} ]
      },
      {
          ""MName"": ""_MacroOff"",
          ""CmdList"": [ { ""A"": {""Direction"": ""X"", ""Value"": 0}}, { ""A"": {""Direction"": ""Y"", ""Value"": 0}} ]
      }
    ],
  ""SwitchMap"": [
      { ""Input"": ""MASTER_BATT"",       ""Cmd"" : [ { ""B"": {""Index"": 10 }}, { ""B"": {""Index"":1 }} ] },
      { ""Input"": ""MASTER_ALT"",        ""Cmd"" : [ { ""B"": {""Index"": 2, ""Mode"": ""t""}}, { ""B"": {""Index"": 2, ""Mode"":""t""}} ] },
      { ""Input"": ""AVIONICS_MASTER"",   ""Cmd"" : [ { ""B"": {""Index"": 3, ""Mode"": ""t"", ""Delay"":300}}, { ""B"": {""Index"":3, ""Mode"":""t"", ""Delay"":100}} ] },
      { ""Input"": ""FUEL_PUMP"",         ""Cmd"" : [ { ""B"": {""Index"": 1, ""Mode"": ""p""}}, { ""B"": {""Index"":1, ""Mode"":""r""}} ] },
      { ""Input"": ""DE_ICE"",            ""Cmd"" : [ { ""B"": {""Index"": 1, ""Mode"": ""t""}} ] },
      { ""Input"": ""PITOT_HEAT"",        ""Cmd"" : [ { ""B"": {""Index"": 6, ""Mode"": ""p""}}, { ""B"": {""Index"":6, ""Mode"":""r""}} ] },
      { ""Input"": ""GEAR"",              ""Cmd"" : [ { ""B"": {""Index"": 1, ""Mode"": ""t""}}, { ""B"": {""Index"":1, ""Mode"":""t""}} ] },
      { ""Input"": ""COWL_CLOSE"",        ""Cmd"" : [ { ""P"": {""Index"": 1, ""Direction"": ""u"", ""Ext1"": ""NA"" }},{ ""P"": {""Index"": 1, ""Direction"": ""c"", ""Ext1"": ""NO"" }} ] },
      { ""Input"": ""PANEL"",             ""Cmd"" : [ { ""B"": {""Index"": 1, ""Mode"": ""t""}}, { ""B"": {""Index"":1, ""Mode"":""t""}} ] },
      { ""Input"": ""BEACON"",            ""Cmd"" : [ { ""M"": {""Macro"": ""_MacroOn"", ""Ext1"": ""RG""}}, { ""M"": {""Macro"": ""_MacroOff"", ""Ext1"": ""RO""}} ] },
      { ""Input"": ""NAV"",               ""Cmd"" : [ { ""K"": {""VKcodeEx"": ""VK_NP_DIVIDE"" }}, { ""K"": {""VKcodeEx"": ""VK_NP_ENTER"" }} ] },
      { ""Input"": ""STROBE"",            ""Cmd"" : [ { ""B"": {""Index"": 1, ""Mode"": ""t"", ""Ext1"": ""LG"" }}, { ""B"": {""Index"":1, ""Mode"":""t"", ""Ext1"": ""LO""}} ] },
      { ""Input"": ""TAXI"",              ""Cmd"" : [ { ""K"": {""VKcodeEx"": ""VK_Y"" }}, { ""K"": {""VKcodeEx"": ""VK_Z"" }} ] },
      { ""Input"": ""LANDING"",           ""Cmd"" : [ { ""K"": {""VKcodeEx"": ""VK_A"", ""Modifier"": ""ls"" }}, { ""K"": {""VKcodeEx"": ""VK_B"", ""Modifier"": ""ls&rs"" }} ] }
    ],
  ""Rotary"": 
      {""Input"": ""ROTARY"",            ""Cmd"" : [
         { ""R"": {""Direction"": ""Z"", ""Value"": 0}},
         { ""R"": {""Direction"": ""Z"", ""Value"": 250}},
         { ""R"": {""Direction"": ""Z"", ""Value"": 500}},
         { ""R"": {""Direction"": ""Z"", ""Value"": 750}},
         { ""R"": {""Direction"": ""Z"", ""Value"": 1000}}
       ]
     }
}";
    }


    public static ConfigFile CFG = null;


    public static void Process()
    {
      CFG = vjMapper.vjMapping.FromJsonString<ConfigFile>( ConfigFileString( ) );
      ;
    }


  }
}
