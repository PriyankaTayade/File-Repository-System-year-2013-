/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate xmlTextWriter class                //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Source:      Jim Fawcett, CST 2-187, Syracuse University      //
///               (315) 443-3948, jfawcett@twcny.rr.com            //
///  Purpose:      To write and Display xml data in formated way   //
/////////////////////////////////////////////////////////////////////
//
using System;
using System.Xml;
using System.IO;

namespace compositeTextAnalysisTool
{
    class xmlTextWriter
    {
        [STAThread]
        public void xmlWrite(string metaFilePath,string description, string dependency, string keyword)
        {
            XmlTextWriter tw = null;
            FileInfo i = new FileInfo(metaFilePath.Substring(0,metaFilePath.LastIndexOf(".") +1)+ "txt");
            long size = i.Length; //text file size
            try
            {
                // attempt to create reader attached to an xml file in debug directory
                tw = new XmlTextWriter(Path.GetFullPath(metaFilePath), null);
                tw.Formatting = Formatting.Indented;
                tw.WriteStartDocument();
                tw.WriteStartElement("METAFILE");     
                tw.WriteStartElement("name");
                tw.WriteString(Path.GetFileNameWithoutExtension(metaFilePath)+".txt");
                tw.WriteEndElement();
                tw.WriteStartElement("created");
                tw.WriteString("" + File.GetCreationTime(metaFilePath));
                tw.WriteEndElement();
                tw.WriteStartElement("modified");
                tw.WriteString(DateTime.Now.ToString("HH:mm:ss tt"));   
                tw.WriteEndElement();
                tw.WriteStartElement("version");
                tw.WriteString("1.0");
                tw.WriteEndElement();
                tw.WriteStartElement("Size");
                tw.WriteString(""+size/1000);
                tw.WriteEndElement();
                tw.WriteStartElement("description");
                tw.WriteString(description);
                tw.WriteEndElement();
                tw.WriteStartElement("dependency");
                tw.WriteString(dependency);
                tw.WriteEndElement();
                tw.WriteStartElement("keywords"); 
                tw.WriteString(keyword);
                tw.WriteEndElement();
                tw.WriteEndElement();
                tw.WriteEndDocument();
                tw.Flush();
                tw.Close();               
      }
      catch(XmlException xmlexp)
      {
        Console.WriteLine(xmlexp.Message);
      }

        } //-------------------------<test stub>---------------------------------------//
#if(TEST_WXML)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- xmlTextWriter.cs");
            Console.Write("\n ============================\n");
            try
            {
               string metaFilePath="../../../FILEREPOSITORY\\CONTENT\\MUSIC\\lyrics1.xml";
               string description="classic music";
               string dependency="music";
               string keyword="music,fun";
               xmlTextWriter xw = new xmlTextWriter();
               xw.xmlWrite(metaFilePath, description, dependency, keyword);
                xmlTextReader r = new xmlTextReader();
                r.xmlRead(metaFilePath);
            }
            catch (Exception)
            { 
                Console.WriteLine("Problem occurred xml write process"); 
            }
            Console.ReadKey();
        }
#endif
    }
}
