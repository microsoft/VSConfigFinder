In Visual Studio, workloads contain the components you need for the programing language or platform you are using. 
VSConfigFinder can produce a consolidated .vsconfig file which contains workload/component information or output a command with workload/component arguments.

##commandline
Usage:VSConfigFinder.exe -folderPath <path to root folder> 
Output: command-line parameters with component/workload ids to be passed in
Output Example: "--add Microsoft.VisualStudio.Component.Roslyn.Compiler Microsoft.Net.Component.4.8.SDK"

##.vsconfig file
Usage: VSConfigFinder.exe --folderPath <path to root folder> --createFile <optional: path for the output file to be placed> 
Output: a consolidated .vsconfig file that contains workloads/component ids outputted to current directory or specified location