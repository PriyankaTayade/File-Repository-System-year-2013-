/////////////////////////////////////////////////////////////////////
///  demoXmlTextWriter.cs  -  Demonstrate directory class          //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Author:      Jim Fawcett, CST 2-187, Syracuse University      //
///               (315) 443-3948, jfawcett@twcny.rr.com            //
///  Purpose:     Creat directory and identifies path for category //
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
    class directoryClass:inputProcessor
    {
        public void creatFileRepository(string path)
        {
            try
            {
                if (path == " ")
                {
                    path = "..\\..\\..\\FILEREPOSITORY\\";   //default path
                }
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Console.WriteLine("The file repository was created successfully at ", Directory.GetLastWriteTime(path));
                    Console.WriteLine("-------------------------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("The file repository exists, last modified " + Directory.GetLastWriteTime(path));
                    Console.WriteLine("-------------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Directory Was not created please enter valid path");
            }
        }
            public string[] directoryPathEntered(string extensionReq, ArrayList processedQuerryResultofProcessor)
            {
                string patt = "*" + extensionReq;
                string path = "..\\..\\..\\fileRepository\\";
                var queryForPaths = from processedQuerry p in processedQuerryResultofProcessor     // check for category path
                                    where (p.command.Equals("C".ToString()))
                                    select p;
                string[] files = null;
                if (queryForPaths.Count() > 0 & queryForPaths.Count() < 2)
                { //---------------------<for recursive search>-----------------------------------------
                    var queryForAllRs = from processedQuerry p in processedQuerryResultofProcessor where (p.command.Equals("R".ToString())) select p;
                     foreach (processedQuerry s in queryForPaths)
                        {
                            path = path + s.searchText;  // create realtive path
                            if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))   
                            {
                                Console.WriteLine("Path does not exist");
                            }
                        }
                     if (queryForAllRs.Count() > 0)   // if /R found 
                     {                  
                        files = Directory.GetFiles(path, patt, SearchOption.AllDirectories); // for recursive search
                    }
                    else
                    {
                        files = Directory.GetFiles(path, patt); // for non recursive search
                    }
                }
                else
                {
                    Console.WriteLine("No Category Path Found");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                return files;          
          }       
//---------------------------<test stub>----------------------------------------------------// 
#if(TEST_DC)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- directoryClass.cs");
            Console.Write("\n ============================\n");
            try
            { ArrayList _processedQuerry= new ArrayList();
               string extensionReq="txt";
            _processedQuerry.Add(
                        new processedQuerry
                        {
                            rawString = "C CONTENT\\POEMS\\",
                            command = "C",
                            searchText = "CONTENT\\POEMS\\"
                        });
               directoryClass d = new directoryClass();
               string[] files=d.directoryPathEntered(extensionReq, _processedQuerry);
               foreach (string file in files)
               {
                   Console.WriteLine(Path.GetFullPath(file)+"\n");
               }
               Console.ReadKey();
            }
            catch (Exception)
            { 
                Console.WriteLine("Problem occurred xml write process"); 
            }
        }
#endif
    }
}
