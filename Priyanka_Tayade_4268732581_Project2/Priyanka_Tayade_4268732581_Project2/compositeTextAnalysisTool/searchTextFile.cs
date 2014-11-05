/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate searchTextFiles class              //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///                                                                //
///  Purpose:    performs search operation in text files           //
///              and display result                                // 
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
    class searchTextFile : inputProcessor
    {
        public void searchData(string stringText,ArrayList processedQuerryResultofProcessor,string command)
        {
            string extensionReq = "*.*";
            ArrayList matchFiles = new ArrayList();
            directoryClass dc = new directoryClass();
            //-----------------<get files >-----------------------------------------//
            string[] files = dc.directoryPathEntered(extensionReq, processedQuerryResultofProcessor);  
            textReader rt = new textReader();       // text reader
            processDocFile df = new processDocFile();  //doc reader
            foreach (string file in files)
            {
                switch(Path.GetExtension(file))
                {
                    case ".cs":
                    case ".csproj":
                    case ".txt":
                    case ".config":
                    case ".dat":
                        matchFiles.Add(rt.matchText(file, stringText, command));    //call match text function and add the result to ArrayList
                        break;
                    case ".doc":
                    case ".docx":
                         matchFiles.Add(df.matchDoc(file, stringText, command));    //call match doc function and add the result to ArrayList
                        break;

                }
            }
            Console.WriteLine("\n\n========================DISPLAY TEXT RESULT=============================\n\n");
            int count = 0;
            foreach (string match in matchFiles)
            {
                if (match != "NA")
                {
                    FileInfo f = new FileInfo(match);
                    long size = f.Length;
                    Console.Write(Path.GetFileName(match) + "            " + (size / 1000) + "KB     \n\n");  // file size conver to KB
                    count++;                  
                }
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>Total Match Found Are: " +count);
            Console.WriteLine("\n\n=======================END TEXT SEARCH RESULT==========================\n\n");

            noMetaData nm = new noMetaData();   // check for all the sacanned files if xml file is present 
            nm.noMetaDataofTxt(files);
            //---------------------------<end of text search>----------------------------------//
        } //-------------------------<test stub>---------------------------------------//
#if(TEST_TSEARCH)
            [STAThread]
            static void Main(string[] args)
            {
                Console.Write("\n  Testing Composite text analysis tool- searchTextFile.cs ");
                Console.Write("\n ============================\n");
                 ArrayList _processedQuerry= new ArrayList();
                string stringT="land where";

        _processedQuerry.Add(
                    new processedQuerry
                    {
                        rawString = "C CONTENT\\POEMS\\",
                        command = "C",
                        searchText = "CONTENT\\POEMS\\"
                    });
        
            try
            {
                searchTextFile s = new searchTextFile();
                s.searchData(stringT, _processedQuerry,"A");
                Console.ReadKey();
            }
            catch (Exception)
            { 
                Console.WriteLine("Problem occurred text search process"); 
            }
         }
#endif
    }     
}
   