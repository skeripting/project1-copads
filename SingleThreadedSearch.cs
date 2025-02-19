using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

class SingleThreadedSearch : SearchAlgorithm {
    private string startDirectory; 
    private long totalBytes; 
    public long TotalBytes {
        get { return totalBytes; }
        set { totalBytes = value; }
    }
    private long totalImageBytes = 0; 
    public long TotalImageBytes {
        get { return totalImageBytes; }
        set { totalImageBytes = value; }
    }
    private bool imagesFoundInDirectory = false;
    public bool ImagesFoundInDirectory {
        get { return imagesFoundInDirectory; }
        set { imagesFoundInDirectory = value; }
    }
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
            FileInfo fileInfo = new FileInfo(filePath);
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
    }
}