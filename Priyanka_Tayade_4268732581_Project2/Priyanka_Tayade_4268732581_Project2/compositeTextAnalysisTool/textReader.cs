/////////////////////////////////////////////////////////////////////
///  demoXmlTextWriter.cs  -  Demonstrate readText class           //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Sources:      Jim Fawcett, CST 2-187, Syracuse University      //
///               (315) 443-3948, jfawcett@twcny.rr.com            //
///                                                                //
///  Purpose:    searches text search keywords in text files       //
///              and return the match result i.e file path.        // 
///              Scannes one file for search keywords and return   //
///              if atleast one is true                            //
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compositeTextAnalysisTool
{
    class textReader
    {
        public string matchText(string file, string stringText, string command)
        {
            try
            {
                string[] stringTextElements = null;
                string content = null; //txt file contant

                //--------------------<read txt file>-------------------------//
                TextReader tr = File.OpenText(file);    
                content = tr.ReadToEnd();
                tr.Close();
                //---------------------<close txt>----------------------------//

                content = content.Replace(System.Environment.NewLine, " ");    // Removes all the new line element. 
                if (content.Length < 5000)
                {

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
                else
                {
                    Console.WriteLine("size of " + Path.GetFileName(file) + "exceeds the limit 5KB");
                    return "NA";
                }
            }
            catch
            {
                Console.Write("\n\n Some problem in text matching ");
            }
            return "NA";
        }   //---------------------<test stub>--------------//
#if(TEST_RT)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- readText.cs");
            Console.Write("\n ============================\n");
            string text = "BLUE STREET";
            string file = Path.GetFullPath("..\\...\\..\\FILEREPOSITORY\\CLIENT\\contact2.txt");
            string command = "O";
            try
            {
                textReader r = new textReader();
                Console.WriteLine(r.matchText(file,text, command));
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
