using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace WebXConverterCli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            
            var app = new CommandApp();
            
            app.Configure(config =>
            {
                config.AddCommand<ConvertFileCommand>("file").WithDescription("Convert a webp or webm file").WithAlias("f");
                config.AddCommand<ConvertDirectoryCommand>("directory").WithDescription("Convert all webp or webm files in directory").WithAlias("d");
            });
            
            return app.Run(args);
        }


        
    }
}
