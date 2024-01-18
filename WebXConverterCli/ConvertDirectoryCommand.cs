using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFileConverter;

namespace WebXConverterCli
{
    internal class ConvertDirectoryCommand : Command<ConvertDirectorySettings>
    {

        void ConvertFiles(IEnumerable<string> files) {

            foreach (string file in files)
            {
                var fi = new FileInfo(file);
                if (file.EndsWith(".webp"))
                {
                    Console.WriteLine("File:");
                    Console.WriteLine(fi.Name);
                    var newFile = ConvertWEBP.ConvertFile(fi.FullName);

                    Console.WriteLine("Converted to: " + Path.GetExtension(newFile));
                }
                else if (file.EndsWith(".webm"))
                {
                    Console.WriteLine("File:");
                    Console.WriteLine(fi.Name);
                    var convertedFile = ConvertWEBM.ConverFile(fi.FullName);
                    Console.WriteLine("Converted to: \n" + convertedFile);
                }
                Thread.Sleep(100);
            }

        }

        void ConvertFile(FileInfo file, bool deleteOriginal)
        {
            AnsiConsole.WriteLine("Converting file:");
            var printFile = new TextPath(file.FullName);
            AnsiConsole.Write(printFile);
            if (file.Extension == ".webp")
            {
                ConvertWEBP.ConvertFile(file.FullName, WEBPConversionOptions.Default, deleteOriginal);
                AnsiConsole.WriteLine("Converted file to .webp");
            }
            else if (file.Extension == ".webm")
            {
                var output = ConvertWEBM.ConverFile(file.FullName, deleteOriginal);
                var newEx = Path.GetExtension(output);
                AnsiConsole.WriteLine("Converted file to " + newEx);
            }
        }

        public override int Execute(CommandContext context, ConvertDirectorySettings settings)
        {
            var deleteOriginal = settings.DelteAfterConversion;
            var directory = new DirectoryInfo(settings.DirectoryPath);
            var prefered = settings.PreferredExtension;
            bool recurseive = settings.RecurseSubDirectories;
            if (!directory.Exists)
            {
                AnsiConsole.WriteLine("[bold red]Directory does not exists[/]");
                return -1;
            }
            
            var files = directory.EnumerateFiles("*.web?", recurseive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                if(file.Extension == ".webp" || file.Extension == ".webm")
                {
                    ConvertFile(file, deleteOriginal);
                }
            }

            

            return 0;
        }
    }
}
