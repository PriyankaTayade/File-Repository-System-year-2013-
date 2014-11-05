/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate noMataData class                   //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///                                                                //
///  Purpose:    checks if the text file has no asscociated        //
///              meta file, name of metafile is same as text       // 
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace compositeTextAnalysisTool
{
    class noMetaData
    {
        public ArrayList noMetaDataofTxt(string[] files)
        {
                ArrayList noMetaDataFiles = new ArrayList();
                getFileNameMatchMetaSearch g = new getFileNameMatchMetaSearch();
                XmlDocument doc = new XmlDocument(); //
                Console.WriteLine("\n\n======================FILES WITH NO META DATA==========================\n\n");
                int count = 0;  //counter for match files
                foreach (string file in files)
                {
                    if (file != "NA")
                    {
                        switch (Path.GetExtension(file))
                        {
                            case ".cs":
                            case ".csproj":
                            case ".txt":
                            case ".config":
                            case ".dat":
                            case ".doc":
                            case ".docx":
                                if (!File.Exists(file.Substring(0,file.LastIndexOf(".") +1)+ "xml"))  //getting xml path, checking if it is present
                                {                                                                     
                                    noMetaDataFiles.Add(Path.GetFullPath(file));    //add the data in ArrayList for further processing if req.
                                    Console.Write(Path.GetFileName(file) + "\n\n");  //display if match not found
                                    count++;                                                                                           
                                }
                                break;
                        }
                    }
                }
                Console.WriteLine(">>>>>>>>>>>>>>>Total Match Found Are: "+count);
                Console.WriteLine("\n\n======================END FILES WITH NO META DATA======================\n\n");
                return noMetaDataFiles;
        }//-------------------------<test stub>---------------------------------------//
#if(TEST_NOM)
            [STAThread]
            static void Main(string[] args)
            {
                Console.Write("\n  Testing Composite text analysis tool- noMetaData.cs ");
                Console.Write("\n ============================\n");
               try
                {
                    string path = "..\\..\\..\\FILEREPOSITORY\\CLIENT\\";
                    Console.WriteLine(Path.GetFullPath(path));
                    string[] files = Directory.GetFiles(Path.GetFullPath(path), ".txt");
                    foreach (string file in files)
                    {
                        Console.WriteLine(file);
                    }
                    noMetaData mc = new noMetaData();
                    mc.noMetaDataofTxt(files);
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem occurred while processing files");
                }
               Console.ReadLine();
            }
#endif
    }
}
