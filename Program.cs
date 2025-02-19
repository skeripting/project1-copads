using System.Diagnostics;

class Program {
    public static string HELP_MESSAGE = @"Usage: du [-s] [-d] [-b] <path>
Summarize disk usage of the set of FILES, recursively for directories.
You MUST specify one of the parameters, -s, -d, or -b
-s Run in single threaded mode
-d Run in parallel mode (uses all available processors)
-b Run in both parallel and single threaded mode.
Runs parallel followed by sequential mode";

    public static string COMMAND_LINE_PARAMETER_SINGLE_THREADED_MODE = "-s";
    public static string COMMAND_LINE_PARAMETER_MULTI_THREADED_MODE = "-d";
    public static string COMMAND_LINE_PARAMETER_BOTH_MODE = "-b"; 

    static void DisplayHelp() {
        Console.WriteLine(HELP_MESSAGE);
    }

    static void Main(string[] args) {
        string modeParameter;
        string pathParameter;
        
        try {
            modeParameter = args[0];
            pathParameter = args[1];
        }
        catch (IndexOutOfRangeException) {
            Console.WriteLine("ERROR: Invalid number of arguments.");
            DisplayHelp();
            return; 
        }
        
        if (modeParameter == null || pathParameter == null) {
            Console.WriteLine("ERROR: Detected a null argument.");
            DisplayHelp();
            return;
        }

        Console.WriteLine("Directory '" + pathParameter + "':");
        Console.WriteLine();

        if (modeParameter == COMMAND_LINE_PARAMETER_SINGLE_THREADED_MODE) {
            DateTime start = DateTime.Now;

            SingleThreadedSearch sts = new SingleThreadedSearch(pathParameter);
            sts.Search();

            DateTime end = DateTime.Now; 

            TimeSpan elapsed = end - start;

            Console.WriteLine("Sequential Calculated in: " + elapsed.TotalSeconds + "s");

            Console.WriteLine(sts.NumFolders + " folders, " + sts.NumFiles + " files, " + sts.TotalBytes +" bytes");
            Console.WriteLine(sts.NumImages + " image files, " + sts.TotalImageBytes + " bytes");

            if (sts.ImagesFoundInDirectory == false) {
                Console.WriteLine("No image files found in the directory.");
            }
        }
        else if (modeParameter == COMMAND_LINE_PARAMETER_MULTI_THREADED_MODE) {

        }
        else if (modeParameter == COMMAND_LINE_PARAMETER_BOTH_MODE) {
            
        }
        else {
            DisplayHelp();
        }
    }
}