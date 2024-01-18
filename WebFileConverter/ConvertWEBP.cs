using FFMpegCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebFileConverter
{
    public enum WEBPConversionOptions
    {
        Default,
        ToPNG,
        TOJPG
    }
    public class ConvertWEBP
    {

        public static string ConvertFile(string file, WEBPConversionOptions conversionOptions = WEBPConversionOptions.Default, bool deleteOriginal = false)
        {
            var item = new FileInfo(file);
            if (!(item.Extension == ".webp"))
            {
                return "";
            }
            using var image = SKBitmap.Decode(item.FullName);

            //var pixelData = image.GetPixelSpan();
            using var spng = new MemoryStream();
            using var sjpeg = new MemoryStream();
            string newFileName = "";
            switch (conversionOptions)
            {
                case WEBPConversionOptions.Default:
                    var dp = image.Encode(SKEncodedImageFormat.Png, 100);
                    dp.SaveTo(spng);
                    var dj = image.Encode(SKEncodedImageFormat.Jpeg, 99);
                    dj.SaveTo(sjpeg);
                    Console.WriteLine("Length of png: " + spng.Length);
                    Console.WriteLine("Length of jpg: " + sjpeg.Length);
                    if (spng.Length * 0.7 > sjpeg.Length) // if size of jpg is less than 30% of png convert to jpg else png
                    {
                        
                        newFileName = item.FullName.Replace(".webp", ".jpg");
                        File.OpenWrite(newFileName).Write(spng.GetBuffer());
                    }
                    else
                    {
                        newFileName = item.FullName.Replace(".webp", ".png");
                        File.OpenWrite(newFileName).Write(spng.GetBuffer());
                    }
                    break;
                case WEBPConversionOptions.ToPNG:
                    dp = image.Encode(SKEncodedImageFormat.Png, 100);

                    dp.SaveTo(spng);
                    newFileName = item.FullName.Replace(".webp", ".png");
                    File.OpenWrite(newFileName).Write(spng.GetBuffer());
                    break;
                case WEBPConversionOptions.TOJPG:
                    dj = image.Encode(SKEncodedImageFormat.Jpeg, 99);
                    dj.SaveTo(sjpeg);
                    break;
            }
            return newFileName;
        }

        public static void ConvertFiles(IEnumerable<string> files, WEBPConversionOptions options = WEBPConversionOptions.Default)
        {
            foreach (var item in files.Select(f => new FileInfo(f)).Where(f => f.Extension == ".webp"))
            {
                using var image = SKBitmap.Decode(item.FullName);

                //var pixelData = image.GetPixelSpan();
                using var spng = new MemoryStream();
                using var sjpeg = new MemoryStream();
                switch (options)
                {
                    case WEBPConversionOptions.Default:
                        break;
                    case WEBPConversionOptions.ToPNG:
                        var dp = image.Encode(SKEncodedImageFormat.Png, 100);

                        dp.SaveTo(spng);
                        var newFileName = item.FullName.Replace(".webp", ".png");
                        File.OpenWrite(newFileName).Write(spng.GetBuffer());
                        break;
                    case WEBPConversionOptions.TOJPG:
                        var dj = image.Encode(SKEncodedImageFormat.Jpeg, 99);


                        dj.SaveTo(sjpeg);
                        newFileName = item.FullName.Replace(".webp", ".jpg");
                        File.OpenWrite(newFileName).Write(spng.GetBuffer());
                        break;
                    default:
                        break;
                }
            };
        }
    }
}
