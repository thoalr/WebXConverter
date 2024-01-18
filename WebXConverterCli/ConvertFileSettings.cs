using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebFileConverter;

namespace WebXConverterCli
{

    class StringToWEBPOptions : TypeConverter
    {

    }
    internal class ConvertFileSettings : CommandSettings
    {
        [Description("File to convert")]
        //[CommandOption("-f|--file <filePath>")]
        [CommandArgument(0, "[filePath]")] 
        public required string FilePath { get; init; }

        [Description("Preferred output file format. Only for .webp. Either .png or .jpg.")]
        [CommandOption("--format <fileFormat>")]
        public WEBPConversionOptions PreferredExtension { get; init; }


        [Description("Delete file after conversion")]
        [CommandOption("-d|--delete")]
        [DefaultValue(false)]
        public bool DelteAfterConversion { get; init; }

        public override ValidationResult Validate()
        {
            var fileExt = Path.GetExtension(FilePath);
            bool validExt = fileExt == ".webp" || fileExt == ".webm";
            return validExt
                ? ValidationResult.Error("File must be either .webp or .webm file")
                : ValidationResult.Success();
        }
    }
}
