public abstract class SearchAlgorithm {
    public abstract void Search();
    public abstract long TotalBytes { get; set; }
    public abstract long TotalImageBytes { get; set; }
    public abstract int NumFiles { get; set; }
    public abstract int NumFolders { get; set; }
    public abstract int NumImages { get; set; }
    public abstract bool ImagesFoundInDirectory { get; set; }
}