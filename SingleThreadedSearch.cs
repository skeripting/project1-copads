class SingleThreadedSearch : SearchAlgorithm {
    private string startDirectory; 
    private int numFiles;
    private int numFolders;
    private int numImages;

    public SingleThreadedSearch(string startDirectory) {
        this.startDirectory = startDirectory;
    }
    public override void Search() {
        string[] fileNames = Directory.GetFiles(startDirectory);

        foreach (string fileName in fileNames) {
            Console.WriteLine(fileName);
        }
    }
}