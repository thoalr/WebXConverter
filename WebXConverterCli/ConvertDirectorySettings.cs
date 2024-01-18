using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFileConverter
{
    internal class ConvertDirectorySettings : CommandSettings
    {
        [Description("Directory to convert")]
        //[CommandOption("-f|--file <filePath>")]
        [CommandArgument(0, "[directoryPath]")]
        public required string DirectoryPath { get; init; }

        [Description("Preferred output file format. Only for .webp. Either .png or .jpg.")]
        [CommandOption("--format <fileFormat>")]
        public WEBPConversionOptions PreferredExtension { get; init; }


        [Description("Delete file after conversion")]
        [CommandOption("-d|--delete")]
        [DefaultValue(false)]
        public bool DelteAfterConversion { get; init; }

        [Description("Recusrse sub directories")]
        [CommandOption("-r|--recurse")]
        [DefaultValue(false)]
        public bool RecurseSubDirectories { get; init; }

        public override ValidationResult Validate()
        //{
        //    var fileExt = Path.GetExtension(FilePath);
        //    bool validExt = fileExt == ".webp" || fileExt == ".webm";
        //    return validExt
        //        ? ValidationResult.Error("File must be either .webp or .webm file")
        //        : ValidationResult.Success();
        {
            return ValidationResult.Success();
        }
        
    }
}
