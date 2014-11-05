/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate searchMetaFiles class              //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///                                                                //
///  Purpose:    To match user input text and  display             //
///              search result                                     //
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
    class searchMetaFiles : inputProcessor
    {
        public void search(string stringTags, ArrayList processedQuerryResultofProcessor)
        {
            ArrayList matchFile = new ArrayList();
            string extensionReq = "xml"; 
            string[] separators = { "," }; //tag seperator
            string[] stringTag = stringTags.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //-----------------<get files with .xml extension>----------------------------//
            directoryClass dc = new directoryClass();
            string[] files = dc.directoryPathEntered(extensionReq, processedQuerryResultofProcessor);

            //-----------------<match metadata>-----------------------------------------//
            getFileNameMatchMetaSearch mm = new getFileNameMatchMetaSearch();
            foreach (string file in files)
            {
                if(!mm.metaDataMatch(stringTag, file).Equals("NA"))                     // check if files are returned
                matchFile.Add(Path.GetFullPath(mm.metaDataMatch(stringTag, file)));   //call metadataMatch function and add the match result 
            }
            Console.WriteLine("\n\n=============================DISPLAY META SEARCH RESULT=========================\n\n");
            int counter = 0;
            foreach (string s in matchFile)
            {
                xmlTextReader nr = new xmlTextReader();
                nr.xmlRead(s);                           // call to display metadata 
                counter++;
            }
            Console.WriteLine("\n Total Match Found (is)Are: " + counter);
            Console.WriteLine("\n \n===========================END META SEARCH RESULT============================\n\n");
        } //-------------------------<test stub>---------------------------------------//
#if(TEST_SMFILE)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- metasearch.cs");
            Console.Write("\n ============================\n");
            string stringTags = "CLIENT";
            ArrayList _processedQuerry= new ArrayList();
        _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "G",
                                command = "G",  
                                searchText = "NA"
                            });
         _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "C CLIENT\\",
                                command = "C",  
                                searchText = "CLIENT\\"
                            });
             _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "MCLIENT",
                                command = "M",  
                                searchText = "CLIENT"
                            });           
            try
            {
                searchMetaFiles smf = new searchMetaFiles();
                smf.search(stringTags, _processedQuerry);
                Console.ReadKey();
            }
            catch (Exception)
            { 
                Console.WriteLine("Problem occurred meta search process"); 
            }
            Console.ReadLine();
        }
#endif
    }
}
             
            


