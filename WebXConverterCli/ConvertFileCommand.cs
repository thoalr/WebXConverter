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
    internal class ConvertFileCommand : Command<ConvertFileSettings>
    {
        public override int Execute(CommandContext context, ConvertFileSettings settings)
        {
            var fileToDelete = settings.FilePath;
            var deleteOriginal = settings.DelteAfterConversion;
            var file = new FileInfo(fileToDelete);
            var prefered = settings.PreferredExtension;
            if(!file.Exists) {
                AnsiConsole.WriteLine("[bold red]File does not exists[/]");
                return -1; }
            AnsiConsole.WriteLine("Converting file:");
            var printFile = new TextPath(fileToDelete);
            AnsiConsole.Write(printFile);
            if(file.Extension == ".webp")
            {
                ConvertWEBP.ConvertFile(fileToDelete, WEBPConversionOptions.Default, deleteOriginal);
                AnsiConsole.WriteLine("Converted file to .webp");
            }
            else if(file.Extension == ".webm")
            {
                var output = ConvertWEBM.ConverFile(fileToDelete, deleteOriginal);
                var newEx = Path.GetExtension(output);
                AnsiConsole.WriteLine("Converted file to " + newEx);
            }
            
            return 0;
        }
    }
}
