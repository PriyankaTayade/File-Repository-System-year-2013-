/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate inputController class              //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///                                                                //
///  Purpose:    makes pragram flow decesions reading commands     //
///                  such as /T /M /E                              // 
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compositeTextAnalysisTool
{
    class inputController:inputProcessor
    {
        public void inputControllers(ArrayList processedQuerryResultofProcessor)
        {
            string searchTextAppend = null;
            try
            {   //----------<processing for generate metadata>------------------------------------------------------------//
                var queryForAllGs = from processedQuerry p in processedQuerryResultofProcessor where (p.command.Equals("G".ToString())) select p;
                if (queryForAllGs.Count() > 0)
                {
                    genMetaData eM = new genMetaData();             // call metadata processor to extract metadata commands from processed input
                    eM.metaDataProcess(processedQuerryResultofProcessor);
                }
                else
                {    //----------<processing for text search>------------------------------------------------------------//
                    var queryForAllTs = from processedQuerry p in processedQuerryResultofProcessor where (p.command.Equals("T".ToString())) select p;
                    if (queryForAllTs.Count() > 0)
                    {
                        foreach (processedQuerry st in queryForAllTs)
                        {
                            searchTextAppend += st.searchText + " ";   // comma seperate for two /T's arguments
                        }
                        Console.WriteLine("TEXT SEARCH STRING.....> " + searchTextAppend);
                        textSwitchProcessor ob = new textSwitchProcessor();                        
                        ob.searchIncludingAllString(searchTextAppend, processedQuerryResultofProcessor);
                    }
                    //----------<processing for metadata search>------------------------------------------------------------//
                    var queryForAllMs = from processedQuerry p in processedQuerryResultofProcessor where (p.command.Equals("M".ToString())) select p;  
                    if (queryForAllMs.Count() > 0)
                    {
                        foreach (processedQuerry st in queryForAllMs)   
                        {
                            Console.WriteLine("META SEARCH TAGS.....> " + st.searchText);     
                            searchMetaFiles sm = new searchMetaFiles();
                            sm.search(st.searchText, processedQuerryResultofProcessor);     
                        }
                    }
                    if (queryForAllTs.Count() < 1 && queryForAllMs.Count() < 1)
                    {
                        Console.WriteLine("Incomplete arguments");               
                    }
                }
            }
            catch (Exception) { Console.WriteLine("Error in operation, Invalid Arguments"); }
        } //-------------------------<test stub>---------------------------------------//
#if(TEST_IC)
        [STAThread]
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- inputProcessor.cs");
            Console.Write("\n ============================\n");
            ArrayList _processedQuerry= new ArrayList();
             _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "C CLIENT/",
                                command = "C",  
                                searchText = "CLIENT"
                            });
            _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "T Nivya",
                                command = "T",  
                                searchText = "Nivya"
                            });
            
            inputController ic = new inputController();
            try
            {
                ic.inputControllers(_processedQuerry);
            }
            catch (Exception)
            {
                Console.WriteLine("Problem occurred during processing");
            }
            Console.ReadKey();
        }
#endif
    }
}