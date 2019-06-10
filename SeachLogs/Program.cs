using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeachLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            var details = new SearchDetails();

            Console.Write("Input your search text: ");
            details.SearchTerm = Console.ReadLine();

            Console.Write("Input your search end term, leave blank if there is no end term: ");
            details.EndPattern = Console.ReadLine();

            Console.Write("Input a file path to search: ");
            details.FilePath = Console.ReadLine();

            if (details.FilePath.Contains("\""))
            {
                details.FilePath = details.FilePath.Trim((char)34);
            }
        }

        public string StartIndexAndEndIndex(SearchDetails e)
        {
            int counter = 0;
            string line;
            int StartLineNumber = 0;
            int EndLineNumber = 0;
            bool IsStartIndexFound = false;

            var file =  new System.IO.StreamReader(e.FilePath);

            while ((line = file.ReadLine()) != null)
            {
                if (IsStartIndexFound == false)
                {
                    if (line.Contains(e.SearchTerm))
                    {
                        StartLineNumber = counter;
                        IsStartIndexFound = true;
                    }
                }
                else
                {
                    if (line.Contains(e.EndPattern))
                    {
                        EndLineNumber = counter;
                        break;
                    }
                }

                counter++;
            }

            Console.WriteLine("Start Line: {0}. End Line {1}.",StartLineNumber,EndLineNumber );

            file.Close();

            return StartLineNumber + "|" + EndLineNumber;
        }

        //NOTE: string line = File.ReadLines(FileName).Skip(14).Take(1).First();
        //https://stackoverflow.com/questions/1262965/how-do-i-read-a-specified-line-in-a-text-file
    }




    class SearchDetails
    {
        public SearchDetails()
        {
            EndPattern = "";
        }
        public string SearchTerm { get; set; }
        public string EndPattern { get; set; }
        public string FilePath { get; set; }

    }
}
