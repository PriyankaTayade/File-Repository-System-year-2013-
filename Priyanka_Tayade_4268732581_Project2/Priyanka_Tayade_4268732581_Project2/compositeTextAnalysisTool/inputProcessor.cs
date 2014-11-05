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
///                  such as /T /M /G                              // 
/////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compositeTextAnalysisTool
{
    class inputProcessor
    {
        private ArrayList _processedQuerry= new ArrayList();   //To hold processed data
        public ArrayList processedQuerryResult
        {
            get { return _processedQuerry;  }
        }
        public class processedQuerry   // input query property 
        {
            public string rawString { get; set; }
            public string command { get; set; }
            public string searchText { get; set; }
        }
        public void querryAnalyser(string _userInput)
        {
            string[] _separators = { " /" };   
            string[]  _querry = _userInput.Split(_separators, StringSplitOptions.RemoveEmptyEntries);                           //Split each command with the seperator ' /' and hold it in temp array.
            for (int i = 0; i < _querry.Length; i++)
            {
                try
                {    //----------</T /C /M /D /K command identifier>------------------------------------------------------------//
                    if (_querry[i].IndexOf(" ") > -1)                       //white space is used as seperator for command and search text
                    {
                        string temp = _querry[i].Substring(0, _querry[i].IndexOf(" ") + 1).ToUpper().Trim(); //exracting command in temporary variable
                        if (temp.Equals("T".ToString()) || temp.Equals("C".ToString()) || temp.Equals("U".ToString()) || temp.Equals("D".ToString()) || temp.Equals("K".ToString())) // All allowed commands
                        {  
                            _processedQuerry.Add(             
                                new processedQuerry
                                {
                                    rawString = _querry[i].Trim(),          //user raw input data
                                    command = temp.Trim(),                  //command
                                    searchText = _querry[i].Substring(_querry[i].IndexOf(" ")).ToUpper().Trim()               // command arguments
                                });
                        }
                        else { Console.WriteLine("invalid command " + _querry[i]); Console.ReadKey(); Environment.Exit(0); }  // Handle invalid all command other than that specified on if condition.
                    }  
                    //-----------------------</A /O /R /X /E command identifier>------------------------------------------------//
                    else if (_querry[i].ToString().ToUpper().Equals("A".ToString()) || _querry[i].ToString().ToUpper().Equals("O".ToString()) || _querry[i].ToString().ToUpper().Equals("G".ToString()) || _querry[i].ToString().ToUpper().Equals("X".ToString()) || _querry[i].ToString().ToUpper().Equals("M".ToString()) || _querry[i].ToString().ToUpper().Equals("R".ToString())) // for special command like /A /O  with no white space seperator
                        {
                            _processedQuerry.Add(
                                new processedQuerry
                                {   rawString = _querry[i].ToUpper(),
                                    command = _querry[i].ToUpper(),  
                                    searchText = "NA"
                                });
                        }  
                        //----------<meta search identifier>-------------------------------------------------------------------//
                        else if (_querry[i].ToUpper().IndexOf("M") == 0)   // Identify meta search command
                            {
                                _processedQuerry.Add(
                                        new processedQuerry
                                        {
                                            rawString = _querry[i],        //user input data
                                            command = "M",
                                            searchText = _querry[i].Substring(1).ToUpper().Trim()
                                        });
                            }
                            else { Console.WriteLine("Invalid Command " + _querry[i]); Console.ReadKey(); Environment.Exit(0); } // Handle invalid all command other than that specified on if condition.
                }
                catch (Exception) { Console.WriteLine("Invalid Command"); Console.ReadKey(); Environment.Exit(0); }             // Handle invalid all command .
            }
        } //-------------------------<test stub>---------------------------------------//
#if(TEST_IP)
        static void Main(string[] args)
        {
            Console.Write("\n  Testing Composite text analysis tool- inputProcessor.cs");
            Console.Write("\n ============================\n");
            string argument1 = "C CLIENT\\ /T Nivya";
            string argument2 = "C CLIENT\\ /mclient";
            string argument3 = "G /C CLIENT\\contact.txt  /T Microsof  /D CLIENT  /K client";
            inputProcessor ip = new inputProcessor();
            try
            {
                ip.querryAnalyser(argument1);
            }
            catch (Exception)
            { 
                Console.WriteLine("Problem occurred text process"); 
            }
            try
            {
                ip.querryAnalyser(argument2);
            }
            catch (Exception)
            {
                Console.WriteLine("Problem occurred metadata search process"); 
            }
            try
            {
                ip.querryAnalyser(argument3);
            }
            catch (Exception)
            {
                Console.WriteLine("Problem occured with meta genration");
            }
            Console.ReadKey();
        }
#endif
    }
}
