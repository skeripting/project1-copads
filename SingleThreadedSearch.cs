class SingleThreadedSearch : SearchAlgorithm {
    private string startDirectory; 
    private int numFiles = 0;
    public int NumFiles {
        get { return numFiles; }
        set { numFiles = value; }
    }

    private int numFolders = 0;
    public int NumFolders {
        get { return numFolders; }
        set { numFolders = value; }
    }

    private int numImages = 0;
    public int NumImages {
        get { return numImages; }
        set { numImages = value; }
    }

    public SingleThreadedSearch(string startDirectory) {
        this.startDirectory = startDirectory;
    }
    public override void Search() {
        string[] fileNames = Directory.GetFiles(startDirectory);
        string[] directories = Directory.GetDirectories(startDirectory);

        foreach (string filePath in fileNames) {
            string extension = Path.GetExtension(filePath);

            if (Constants.ImageExtensions[extension]) {
                numFiles += 1;
            }
        }
        
        foreach (string directoryPath in directories) {
            numFolders += 1;

            SingleThreadedSearch sts = new SingleThreadedSearch(directoryPath);
            sts.Search();

            numFiles += sts.NumFiles;
            numFolders += sts.NumFolders;
            numImages = sts.NumImages;
        }
    }
}