class SingleThreadedSearch : SearchAlgorithm {
    private string startDirectory; 
    private long totalBytes; 
    public override long TotalBytes {
        get { return totalBytes; }
        set { totalBytes = value; }
    }
    private long totalImageBytes = 0; 
    public override long TotalImageBytes {
        get { return totalImageBytes; }
        set { totalImageBytes = value; }
    }
    private bool imagesFoundInDirectory = false;
    public override bool ImagesFoundInDirectory {
        get { return imagesFoundInDirectory; }
        set { imagesFoundInDirectory = value; }
    }
    private int numFiles = 0;
    public override int NumFiles {
        get { return numFiles; }
        set { numFiles = value; }
    }

    private int numFolders = 0;
    public override int NumFolders {
        get { return numFolders; }
        set { numFolders = value; }
    }

    private int numImages = 0;
    public override int NumImages {
        get { return numImages; }
        set { numImages = value; }
    }

    public SingleThreadedSearch(string startDirectory) {
        this.startDirectory = startDirectory;
    }

    public override bool Search() {
        string[] fileNames = [];
        string[] directories = [];

        try {
            fileNames = Directory.GetFiles(startDirectory);
            directories = Directory.GetDirectories(startDirectory);
        }
        catch (Exception) {
            Console.WriteLine("ERROR in reading the files and directories in " + startDirectory + ", skipped.");
            return false;
        }

        foreach (string filePath in fileNames) {
            FileInfo fileInfo;

            try {
                fileInfo = new FileInfo(filePath);
            }
            catch (Exception) {
                Console.WriteLine("ERROR in extracting file info for " + filePath + ", skipped.");
                continue; 
            }
            
            long fileLength = fileInfo.Length;
            string extension = Path.GetExtension(filePath);

            if (Constants.ImageExtensions.ContainsKey(extension)) {
                numImages += 1;
                totalImageBytes += fileLength;
            }

            numFiles += 1;
            totalBytes += fileLength;
        }

        if (numImages > 0) { 
            imagesFoundInDirectory = true;
        }
        
        foreach (string directoryPath in directories) {
            numFolders += 1;

            SingleThreadedSearch sts = new SingleThreadedSearch(directoryPath);
            sts.Search();

            numFiles += sts.NumFiles;
            numFolders += sts.NumFolders;
            numImages += sts.NumImages;
            totalImageBytes += sts.TotalImageBytes;
            totalBytes += sts.TotalBytes;

            if (sts.ImagesFoundInDirectory == true) {
                imagesFoundInDirectory = true;
            }
        }
        
        return true; 
    }
}