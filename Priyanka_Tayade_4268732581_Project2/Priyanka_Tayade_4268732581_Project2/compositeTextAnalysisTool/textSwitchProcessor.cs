/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate textSearchProcess class            //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Purpose:    To Identify switches /A match all search text     //         
///                 or /O atleast match one search text            //
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections;  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compositeTextAnalysisTool
{
    class textSwitchProcessor: inputProcessor
    {
        public void searchIncludingAllString(string stringText, ArrayList processedQuerryResultofProcessor)
        {
            var query = from processedQuerry p in processedQuerryResultofProcessor                       // search for /A /O switches
                        where (p.command.Equals("A".ToString()) || p.command.Equals("O".ToString()))   
                        select p;

            if (query.Count() > 1)
            {
                Console.WriteLine("Invalid command, /a or /o is allowed once only");               // more than one switch not allowed
            }
            else
            {
                searchTextFile stf = new searchTextFile();   
                if (query.Count() != 0)
                {
                    foreach (processedQuerry s in query)
                    {
                        if (s.command.Equals("A".ToString()))
                        {
                            stf.searchData(stringText, processedQuerryResultofProcessor, "A");
                        }
                        else
                        {
                            stf.searchData(stringText, processedQuerryResultofProcessor, "O");
                        }
                    }
                }
                else
                {
                    stf.searchData(stringText, processedQuerryResultofProcessor, "O");           // without switch command default value is /O
                }
            }
        } ///-----------------------------<test stub>--------------------------------
#if(TEST_TSWITCH)
            [STAThread]
            static void Main(string[] args)
            {
                Console.Write("\n  Testing Composite text analysis tool- textSearchProcessor.cs ");
                Console.Write("\n ============================\n");
                 ArrayList _processedQuerry= new ArrayList();
                string stringT="land where";
        _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "T",
                                command = "T",  
                                searchText = "land"
                            });
        _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "T",
                                command = "T",  
                                searchText = "where"
                            });
        _processedQuerry.Add(
                    new processedQuerry
                    {
                        rawString = "C CONTENT\\POEMS\\",
                        command = "C",
                        searchText = "CONTENT\\POEMS\\"
                    });
         _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "A",
                                command = "A",  
                                searchText = "NA"
                            });
            try
            {
                textSwitchProcessor s = new textSwitchProcessor();
                s.searchIncludingAllString(stringT, _processedQuerry);
                Console.ReadKey();
            }
            catch (Exception)
            { 
                Console.WriteLine("Problem occurred meta search process"); 
            }
            Console.ReadKey();
         }
#endif
    }
}
