/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate Program class                      //
///                                                                //
///  Language:    C#                                               //
///  Platform:    Dell Dimension 8100, Windows 2000 Prof., SP2     //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Purpose:     Contains main and takes commandline arguments    //
///               and pass it to main controller                   //
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace compositeTextAnalysisTool
{
    class Program
    {
#if(MAIN_M)
        static void Main(string[] args)
        {
            mainController mc = new mainController();
            string argument = null;
            foreach (string s in args)   //converting args into string for processing
            {
                argument += " " + s;
            }
            mc.Run(argument);            //pass argument to mainController 
        }
#endif
    }
}
