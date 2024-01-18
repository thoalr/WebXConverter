using FFMpegCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFileConverter
{

    public class ConvertWEBM
    {

        public static string ConverFile(string path, bool deleteOriginal = false)
        {
            if (!path.EndsWith(".webm")) return "";

            string newFileName;

            var analysis = FFProbe.Analyse(path);
            

            int nAudioStream = analysis.AudioStreams.Count;
            if (nAudioStream > 0)
            {
                newFileName = path.Replace(".webm", ".mp4");
                if (FFMpegArguments.FromFileInput(path).OutputToFile(newFileName).ProcessSynchronously())
                    return Path.GetFileName(newFileName);
                else
                {
                    return "Failed to convert to mp4";
                }
            }
            else
            {
                newFileName = path.Replace(".webm", ".gif");
                if (FFMpegArguments.FromFileInput(path).OutputToFile(newFileName).ProcessSynchronously())
                    return Path.GetFileName( newFileName);
                else
                {
                    return "Failed to convert to gif";
                }
            }

        }
    }
}
