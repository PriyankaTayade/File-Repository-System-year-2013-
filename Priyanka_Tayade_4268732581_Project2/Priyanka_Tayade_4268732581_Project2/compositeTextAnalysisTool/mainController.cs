/////////////////////////////////////////////////////////////////////
///  Program.cs  -  Demonstrate mainController class               //
///                                                                //
///  Language:    C#                                               //
///  Platform:    TOSHIBA C640, Windows 7 ultimate.                //
///  Application: CIS 681 -Software Modeling & Analysis,Fall 2013  //
///  Author:      Priyanka Tayade, Syracuse University             //
///               (315) 416-0205, pbtayade@syr.edu                 //
///  Purpose:     Controls main program flow                       //
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compositeTextAnalysisTool
{   
    class mainController
    {
        public void Run(string argumnt)
        {
            try
            {
                //----------<create repository>--------------//
                directoryClass dc = new directoryClass();  // Call directory class to creat repositroy if not present
                dc.creatFileRepository(" ");                //passing default path

                //----------<process user input>--------------//
                inputProcessor ip = new inputProcessor(); //process input querry into  meaningful command 
                ip.querryAnalyser(argumnt);

                //----------<make decesion based on user query>--------------//
                inputController ic = new inputController();  // pass processed querry further to input controller to make further flow control
                ic.inputControllers(ip.processedQuerryResult);
            }
            catch(Exception)
            {
                Console.WriteLine("There was some error in processing, Please try again!");  //For some unkown exception
            }
        }
            //----< test stub >-------------------------------------------------
#if(TEST_CON)
            [STAThread]
            static void Main(string[] args)
            {
                Console.Write("\n  Testing Composite text analysis tool- mainController.cs ");
                Console.Write("\n ============================\n");
                try
                {
                    string argument1 = "C CLIENT/ /T Nivya";
                    string argument2 = "C CLIENT/ /mclient";
                    string argument3 = "E /C CLIENT/contact.txt /T Microsof is the vender of visial studio /D CLIENT /K client,contact,phone,email,address";
                    mainController mc = new mainController();
                    mc.Run(argument1);
                    mc.Run(argument2);
                    mc.Run(argument3);
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem occurred while processing queries");
                }
            }
#endif
    }
}
