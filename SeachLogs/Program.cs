using System;
using System.IO;
using System.Linq;

namespace SeachLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            var details = new SearchDetails();

            Console.Write("Input your search text: ");
            details.SearchStartPattern = Console.ReadLine();

            Console.Write("Input your search end term, leave blank if there is no end term: ");
            details.SearchEndPattern = Console.ReadLine();

            Console.Write("Input a file path to search: ");
            details.FilePath = Console.ReadLine();

            details.FilePath = TrimPath(details.FilePath);

            details = GetStartIndexAndEndIndex(details);

            Console.WriteLine("Reading log");
            var log = PrecisionReadFile(details);

            Console.WriteLine(log);
            Console.ReadLine();
        }
        public static string TrimPath(string path)
        {
            if (path.Contains("\""))
            {
                return path = path.Trim((char)34);
            }
            else
            {
                return path;
            }
        }
        public static SearchDetails GetStartIndexAndEndIndex(SearchDetails e)
        {
            int counter = 0;
            string line;
            int StartLineNumber = 0;
            int EndLineNumber = 0;
            bool IsStartIndexFound = false;

            var file =  new StreamReader(e.FilePath);

            while ((line = file.ReadLine()) != null)
            {
                if (IsStartIndexFound == false)
                {
                    if (line.Contains(e.SearchStartPattern))
                    {
                        StartLineNumber = counter;
                        e.StartLine = StartLineNumber;
                        IsStartIndexFound = true;
                    }
                }
                else
                {
                    if (line.Contains(e.SearchEndPattern))
                    {
                        EndLineNumber = counter;
                        e.StartLine = EndLineNumber;


                        break;
                    }
                }

                counter++;
            }

            Console.WriteLine("Start Line: {0}. End Line {1}.",StartLineNumber,EndLineNumber );
            file.Close();
            return e;
        }

        public static string PrecisionReadFile(SearchDetails e)
        {
            var readLength = (e.EndLine - e.StartLine)+1;
            var lines = File.ReadLines(e.FilePath).Skip(e.StartLine - 1).Take(readLength);

            string log = "";
            //convert to string
            foreach (string line in lines)
            {

                log = log + Environment.NewLine + line;
               
            }
            return log;
        }

        //NOTE: string line = File.ReadLines(FileName).Skip(14).Take(1).First();
        //https://stackoverflow.com/questions/1262965/how-do-i-read-a-specified-line-in-a-text-file
    }


    

    class SearchDetails
    {
        public SearchDetails()
        {
            SearchEndPattern = "";
        }
        public string SearchStartPattern { get; set; }
        public string SearchEndPattern { get; set; }
        public string FilePath { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }
    }
}
