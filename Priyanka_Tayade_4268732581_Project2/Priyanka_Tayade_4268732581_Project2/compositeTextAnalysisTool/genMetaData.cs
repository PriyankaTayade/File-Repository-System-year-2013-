/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate genMetaData class                  //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///                                                                //
///  Purpose:     Performs meta data search operation and display  //
///                result                                          // 
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
    class genMetaData:inputProcessor
    {
        public void metaDataProcess(ArrayList processedQuerryResultofProcessor)
        {
            try
            {   //---------------------<get xml path to be written>--------------------//
                categoryPath em = new categoryPath();
                string metaFilePath = em.getMetaFilePath(processedQuerryResultofProcessor);
                string description = null, dependency = null, keyword = null;

                //---------------------<check for description>--------------------//
                var queryForText = from processedQuerry p in processedQuerryResultofProcessor where (p.command.Equals("T".ToString())) select p;
                if (queryForText.Count() < 1)
                {
                    Console.WriteLine("Invalid command, No command for metadata /T  was entered"); Console.ReadKey(); Environment.Exit(0); //end;
                }
                else if (queryForText.Count() == 1)
                {
                    foreach (processedQuerry s in queryForText)
                    {
                        description = s.searchText;
                    }   //---------------------<check for dependency and get value>--------------------//
                    var queryForDependency = from processedQuerry p in processedQuerryResultofProcessor where (p.command.Equals("D".ToString())) select p;
                    foreach (processedQuerry s in queryForDependency)
                    {
                        dependency = s.searchText;
                    }
                    if (queryForDependency.Count() == 1)
                    {  //---------------------<check for keywords and get value>--------------------//
                        var queryForKeyword = from processedQuerry p in processedQuerryResultofProcessor where (p.command.Equals("K".ToString())) select p;
                        if (queryForKeyword.Count() == 1)
                        {
                            foreach (processedQuerry s in queryForKeyword)
                            {
                                keyword = s.searchText;
                            }
                        }
                        else { Console.WriteLine("Command /D for dependency was not found"); Console.ReadKey(); Environment.Exit(0); }
                    }
                    else { Console.WriteLine("Command /D for dependency was not found"); Console.ReadKey(); Environment.Exit(0); }
                    xmlTextWriter de = new xmlTextWriter();
                    de.xmlWrite(metaFilePath, description, dependency, keyword);  //pass data to xml writer
                    xmlTextReader xn = new xmlTextReader();
                    xn.xmlRead(metaFilePath);                      // pass data to read
                }
                else
                {
                    Console.WriteLine("Can perform only metadata operation, Please enter T only once -command for decription"); Console.ReadKey(); Environment.Exit(0);
                }
            }
            catch (Exception) { Console.WriteLine("error in metaGeneration"); }
        } //-------------------------<test stub>---------------------------------------//
#if(TEST_MP)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- metaDataProcessor.cs");
            Console.Write("\n ============================\n");
            ArrayList _processedQuerry= new ArrayList();
        _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "E",
                                command = "E",  
                                searchText = "NA"
                            });
         _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "C CLIENT/contact2.txt",
                                command = "C",  
                                searchText = "CLIENT/contact2.txt"
                            });
             _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "D CLIENT",
                                command = "D",  
                                searchText = "CLIENT"
                            });
        _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "K CLIENT",
                                command = "K",  
                                searchText = "CLIENT,CONTACT"
                            });
        _processedQuerry.Add(
                            new processedQuerry
                            {   rawString = "T some description",
                                command = "T",  
                                searchText = "some description"
                            });
            try
            {
                 genMetaData m = new genMetaData();
                 m.metaDataProcess(_processedQuerry);
                Console.ReadKey();
            }
            catch (Exception)
            { 
                Console.WriteLine("Problem occurred meta generation process"); 
            }
        }
#endif
    }
}
