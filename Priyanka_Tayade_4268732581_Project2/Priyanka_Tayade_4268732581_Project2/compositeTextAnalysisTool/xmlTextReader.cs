/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate Program class                      //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Source:      Jim Fawcett, CST 2-187, Syracuse University      //
///               (315) 443-3948, jfawcett@twcny.rr.com            //
///  Purpose:      To Read and Display xml data in formmated way   //
/////////////////////////////////////////////////////////////////////
///
//   Ver 1.1 : 21 Sept 2006
//   - fixed bug discovered by TA Manas Kelshikar.  There was an 
//     error caused by exception handling flow that resulted in missed
//     nodes.  That has been fixed by testing for XmlNodeType.Text
//     in the last loop.
//
using System;
using System.Xml;
using System.IO; //Added by priyanka
namespace compositeTextAnalysisTool
{
    class xmlTextReader
    {
        [STAThread]
        public void xmlRead(string path)
        {
            string DemoTitle = "  XML file "+Path.GetFileName(path); // extracting name of xml file.
            string Border = " =================================";
            Console.WriteLine("\n{0}", Border);
            Console.WriteLine(DemoTitle);
            Console.WriteLine(Border);
            XmlTextReader tr1 = null;
            try
            {    // attempt to create reader attached to an xml file in debug directory
                tr1 = new XmlTextReader(path);
                while (tr1.Read())
                {
                    switch (tr1.NodeType)
                    {                   
                        case XmlNodeType.Element: 
                            Console.Write(tr1.Name);
                            break;
                        case XmlNodeType.Text:
                            Console.Write(tr1.Name + "   {0}", tr1.Value + "\n ------------------------");
                            break;
                        case XmlNodeType.EndElement:
                            Console.Write("\n ");
                            break;                                   
                        case XmlNodeType.XmlDeclaration:
                            Console.Write(" "+tr1.Name+" ");
                            Console.Write(tr1.Value);
                            break;
                        case XmlNodeType.Document:
                            Console.Write("\n  Docum: {0}", tr1.Value);
                            break;
                        case XmlNodeType.DocumentType:
                            Console.Write("\n  Docum: {0}", tr1.Value);
                            break;
                        default:
                            Console.Write("\n ");
                            break;
                    }
                }
            }
            catch (Exception xmlexp) 
            {
                Console.WriteLine("\n  " + xmlexp.Message + "\n\n");
            }
        } //-------------------------<test stub>---------------------------------------//
#if(TEST_TXML)
      
        [STAThread]
            static void Main(string[] args)
            {
                try
                {
                    Console.Write("\n  Testing Composite text analysis tool- xmlTextReader.cs ");
                    Console.Write("\n ============================\n");
                    string argument1 = "C:\\Users\\Priyanka\\Documents\\Visual Studio 2012\\Projects\\compositeTextAnalysisTool\\FILEREPOSITORY\\CLIENT\\contact.xml";
                    xmlTextReader xr = new xmlTextReader();
                    xr.xmlRead(argument1);
                    Console.ReadKey();
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem occurred xml read process");
                }
            }
#endif
    }
}
