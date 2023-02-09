namespace VSConfigFinder
{
    internal interface ICommandLineOptions
    {
        string FilePath { get; set; }

        string Output { get; set; }
    }
}
