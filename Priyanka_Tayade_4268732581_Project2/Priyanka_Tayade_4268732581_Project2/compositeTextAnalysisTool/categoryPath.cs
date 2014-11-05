/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate category path class                //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///                                                                //
///  Purpose:    returns xml file path for to edit or              //
///              generate xml data module                          // 
/////////////////////////////////////////////////////////////////////
///------------------used to generate xml file name and path or creation or modification purpose---------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compositeTextAnalysisTool
{
    class categoryPath:inputProcessor
    {
        public string getMetaFilePath(ArrayList processQuerryForExecution)
        {
            string path = null;
            try
            {   //----------<processing for category path>------------------------------------------------------------//
                var queryForCategory = from processedQuerry p in processQuerryForExecution
                                       where (p.command.Equals("C".ToString()))
                                       select p;
               if (queryForCategory.Count() == 0)
                {
                    Console.WriteLine("Category path not found" + queryForCategory.Count());   //command not found throw erro
                    Environment.Exit(0);
                    return null;
                }
                else
                {
                    foreach (processedQuerry s in queryForCategory)
                    {
                        path = s.rawString.Substring(s.rawString.IndexOf(" ")).Trim();   // taking category data from raw string
                    }
                }
            }
            catch (Exception) { Console.WriteLine("There was some problem read the path"); }
            path = "..\\..\\..\\FILEREPOSITORY\\" + path;                         //relative path of the file
            try
            {
                if (Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                {

                    if (File.Exists(path))
                    {
                        string patt = "xml";
                        return path.Substring(0, path.LastIndexOf(".") + 1) + patt;   //append xml extension and return file path then create or generate this xml file
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                        Environment.Exit(0);
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Path does not exist");
                    Environment.Exit(0);
                    return null;
                }
            }
            catch (Exception) { Console.WriteLine("Unable to get text file path");  } return null;
        } //-------------------<test stub>-------------------------------------
#if(TEST_MFILE)
        [STAThread]
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- editMetaFile.cs");
            Console.Write("\n ============================\n");
            ArrayList _processedQuerry = new ArrayList();
            _processedQuerry.Add(
                           new processedQuerry
                           {
                               rawString = "E", command = "E", searchText = "NA"
                           });
            _processedQuerry.Add(
                            new processedQuerry
                            {
                                rawString = "C CLIENT/contact.txt", command = "C", searchText = "CLIENT/contact.txt"
                            });
            _processedQuerry.Add(
                            new processedQuerry
                            {
                                rawString = "T some description", command = "T", searchText = "some description"
                            });
            _processedQuerry.Add(
                            new processedQuerry
                            {
                                rawString = "D CLIENT,Dependency", command = "D", searchText = "CLIENT,Dependency"
                            });
            _processedQuerry.Add(
                            new processedQuerry
                            {
                                rawString = "K client,contact,email,phone", command = "K", searchText = "client,contact,email,phone"
                            });

            categoryPath ic = new categoryPath();
            try
            {
                Console.WriteLine(ic.getMetaFilePath(_processedQuerry));
            }
            catch (Exception)
            {
                Console.WriteLine("Problem occurred during medata file processing");
            }
            Console.ReadKey();
        }
#endif
    }
    }