namespace VSConfigFinder
{
    internal class CommandLineOptions : ICommandLineOptions
    {
        public CommandLineOptions(string filePath, string output)
        {
            Utilities.ValidateIsNotNullOrEmpty(filePath, nameof(filePath));
            Utilities.ValidateOutputParameter(output, nameof(output));

            FilePath = filePath;
            Output = output;
        }

        public string FilePath { get; set; }

        public string Output { get; set; }
    }
}
