/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate matchMetaData class                //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Purpose:    To match user input metatags with xml file data   //
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
namespace compositeTextAnalysisTool
{
    class getFileNameMatchMetaSearch
    {
        public string metaDataMatch(string[] tags,string file)
        {
            string[] seperator={","};  //tag seperator
            XmlDocument doc = new XmlDocument();           
            foreach(string tag in tags)
            {
                doc.Load(file);                             
                XmlNodeReader nr = new XmlNodeReader(doc);   // read xml file
                while (nr.Read())
                {
                    string[] tempValue = nr.Value.Split(seperator, StringSplitOptions.RemoveEmptyEntries);  // take value of xml nodes 
                    foreach (string temp in tempValue)
                    {
                        if (tag.ToUpper().Trim().Equals(temp.ToUpper().ToString().Trim()))   
                        {
                            return file;  //Aleast one tag match return file name
                        }
                    }
                }
            }
            return "NA";   //no match found
        }//------------------------------------<text stub>-------------------------------------------//
#if(TEST_MMETA)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- matchMetadata.cs");
            Console.Write("\n ============================\n");
            try
            {
                string[] seperator = { "," };
                string[] tags="client,contact".Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                string file = "../.../../FILEREPOSITORY/CLIENT/contact.xml";
               
                getFileNameMatchMetaSearch d = new getFileNameMatchMetaSearch();
                Console.WriteLine(d.metaDataMatch(tags, Path.GetFullPath(file)));
                Console.ReadKey();;
            }
            catch (Exception)
            {
                Console.WriteLine("Problem occurred xml write process");
            }
        }
#endif
    }
}
