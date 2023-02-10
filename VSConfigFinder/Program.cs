namespace VSConfigFinder
{
    using CommandLine;

    /// <summary>
    /// <see cref="Program"/> class for the .vsconfig finder tool.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the tool.
        /// </summary>
        /// <param name="args">The program arguments</param>
        public static void Main(string[] args)
        {
            var parser = new Parser(with =>
            {
                with.CaseSensitive = false;
            });

            // Take in the command line arguments
            parser.ParseArguments<CommandLineOptions>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        private static void Run(CommandLineOptions options)
        {
            Console.WriteLine("Hello! I succeeded!");
            Console.WriteLine($"--createFile: {options.CreateFile}");
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Oops, failed");
        }
    }
}