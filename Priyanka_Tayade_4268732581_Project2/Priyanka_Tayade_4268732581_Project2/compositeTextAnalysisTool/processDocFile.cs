/////////////////////////////////////////////////////////////////////
///  demoXmlTextWriter.cs  -  Demonstrate processDocFile class     //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Sources:      http://www.dotnetperls.com/word                 //
///                                                                //
///  Purpose:    match doc content for a file a retun file name    //
///              if match found!                                   // 
/////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compositeTextAnalysisTool
{
    class processDocFile
    {
        public string matchDoc(string file,string stringText, string command)
        {
            try
            {
                string[] stringTextElements = null;
                docReaderClass dr = new docReaderClass();
                string content = dr.getDocContent(file);

                //---------------------<switch O - or operation>--------------//
                if (command.Equals("O".ToString()))
                {
                    string[] separator = { " " };
                    stringTextElements = stringText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string m in stringTextElements)
                    {
                        if (content.ToUpper().Trim().IndexOf(m.ToUpper().Trim()) > -1)
                        {
                            return Path.GetFullPath(file);   // Altleast one match found return
                        }
                    }
                }
                else
                {   //---------------------<switch A - and operation>--------------//
                    if (content.ToUpper().Trim().IndexOf(stringText.ToUpper().Trim()) > -1)
                    {
                        return Path.GetFullPath(file);    // All match found return
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("problem in doc processing");
            }
            return "NA";
        }  
        //---------------------<test stub>--------------//
#if(TEST_PDOC)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- processDocFile.cs");
            Console.Write("\n ============================\n");
            string text = "problems faced";
            string file = Path.GetFullPath("..\\...\\..\\FILEREPOSITORY\\BUSINESS\\formatted.doc");
            string command = "O";
            try
            {
                processDocFile r = new processDocFile();
                Console.WriteLine(r.matchDoc(file,text, command));
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Problem occurred read text process");
            }
        }
#endif
    }
}
