namespace VSConfigFinder
{
    using CommandLine;

    /// <inheritdoc/>
    public class CommandLineOptions : ICommandLineOptions
    {
        /// <inheritdoc/>
        [Option("folderpath", Required = true, HelpText = "Set the source folder path to use as the root. The search will start from the root towards the bottom.")]
        public string FolderPath { get; set; }

        /// <inheritdoc/>
        [Option("createfile", Required = false, HelpText = "Set the folder path to output the consolidated .vsconfig.\n" +
            "If the argument (--createfile) is not passed at all, the program will output the cli arguments that can be fed into the Visual Studio Installer setup arguments. (e.g. --add Microsoft.VisualStudio.Component.Roslyn.Compiler Microsoft.Net.Component.4.8.SDK)\n" +
            "If the argument is passed without a following folder path, the program will create and output the final .vsconfig into the current directory.\n" +
            "If the argument is passed with a following folder path, the program will create and output the final .vsconfig into the specified folder path that is followed by --createFile.")]
        public string? CreateFile { get; set; }
    }
}
