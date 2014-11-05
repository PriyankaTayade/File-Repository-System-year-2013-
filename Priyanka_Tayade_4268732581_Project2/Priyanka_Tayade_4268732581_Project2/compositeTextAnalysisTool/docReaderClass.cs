/////////////////////////////////////////////////////////////////////
///  demoXmlTextWriter.cs  -  Demonstrate docReaderClass           //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Sources:      http://www.dotnetperls.com/word                 //
///                                                                //
///  Purpose:    read a doc file for a given path and retrn        //
///              all the content                                   // 
/////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace compositeTextAnalysisTool
{
    class docReaderClass
    {
        public string getDocContent(string file)
        {
            try
            {
                FileInfo f = new FileInfo(Directory.GetParent(file)+"\\"+Path.GetFileName(file)); //get file info
                long size = f.Length;
                if(File.Exists(Directory.GetParent(file)+"\\" + Path.GetFileName(file)))     
                {
                    if (size/1000< 35)       //size should be less than 35KB                
                    {
                        //-----------< Open a doc file.>------------------------------------//
                        _Application application = new Application();
                         object noparam = Type.Missing;
                        Document document = application.Documents.Open(Directory.GetParent(file) +"\\" +Path.GetFileName(file),ref noparam ,true); //providing full path
                        string content = null;

                        //-----------< Loop through all words in the document.>-------------//
                        int count = document.Words.Count;
                        for (int i = 1; i <= count; i++)
                        {
                            // Write the word.
                            content += document.Words[i].Text.Trim()+ " ";
                            content = content.Replace(System.Environment.NewLine, " ");// Removes all the new line element.
                        }
                        // Close word.
                        ((_Document)document).Close();
                        ((_Application)application).Quit();
                        return content;
                    }
                    else
                    {
                        Console.WriteLine("size of " + Path.GetFileName(file) + "exceeds the limit 35KB");
                        return "NA";
                    }
                }
                return "NA";
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("The file appears to be corrupted.")==false)
                {
                    Console.WriteLine("Error in reading Doc."); return "NA";
                }
            } return "NA";
        } //----------------------------------<test tub>-----------------------------
#if(TEST_DOCR)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- docReaderClass.cs");
            Console.Write("\n ============================\n");
            string file = Path.GetFullPath("..\\...\\..\\FILEREPOSITORY\\BUSINESS\\formatted.doc");
            try
            {
                docReaderClass r = new docReaderClass();
                Console.WriteLine(r.getDocContent(file));
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Problem occurred read doc file");
            }
        }
#endif
    }
}
