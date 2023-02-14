namespace VSConfigFinder
{

    public interface IFileSystem
    {
        public string[] GetFileSystemEntries(string path, string pattern, bool recursive = false);

        public Stream OpenFile(string path);
    }
}
