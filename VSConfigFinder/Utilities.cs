using System.Diagnostics.CodeAnalysis;

namespace VSConfigFinder
{
    internal class Utilities
    {
        private static readonly string[] AcceptedOutputOptions = new[] { "config", "commandline" };

        public static void ValidateOutputParameter([NotNull] string s, string paramName)
        {
            ValidateIsNotNullOrEmpty(s, paramName);

            if (!AcceptedOutputOptions.Contains(s))
            {
                throw new ArgumentException($"The --output parameter accepts only two options: {string.Join(",", AcceptedOutputOptions)}.");
            }
        }

        public static void ValidateIsNotNullOrEmpty([NotNull] string s, string paramName)
        {
            IsNotNull(s, paramName);
            IsNotEmpty(s, paramName);
        }

        public static void IsNotNull([NotNull] object o, string paramName)
        {
            if (o is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void IsNotEmpty([NotNull] string s, string paramName)
        {
            if (s == string.Empty)
            {
                throw new ArgumentException("The string is empty.", paramName);
            }
        }
    }
}
