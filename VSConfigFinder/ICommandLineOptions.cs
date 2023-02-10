namespace VSConfigFinder
{
    /// <summary>
    /// The interface for the command line options.
    /// </summary>
    public interface ICommandLineOptions
    {
        /// <summary>
        /// Gets or sets the folder path to be used as the root (starting point) of the search.
        /// </summary>
        string FolderPath { get; set; }

        /// <summary>
        /// Gets or sets the folder path to output the consolidated .vsconfig instead of the command line arguments.
        /// If a folder path is not provided after the argument, the output folder will default to the current directory.
        /// </summary>
        string? CreateFile { get; set; }
    }
}
