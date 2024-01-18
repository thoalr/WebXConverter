using System.Diagnostics;

namespace WebFileConverter
{
    internal class Program
    {

        static void ConvertFileList(IEnumerable<string> files)
        {
            foreach (string file in files)
            {
                var fi = new FileInfo(file);
                if(file.EndsWith(".webp"))
                {
                    Console.WriteLine("File:");
                    Console.WriteLine(fi.Name);
                    var newFile = ConvertWEBP.ConvertFile(fi.FullName);

                    Console.WriteLine("Converted to: " + Path.GetExtension(newFile));
                }
                else if(file.EndsWith(".webm"))
                {
                    Console.WriteLine("File:");
                    Console.WriteLine(fi.Name);
                    var convertedFile = ConvertWEBM.ConverFile(fi.FullName);
                    Console.WriteLine("Converted to: \n" + convertedFile);
                }
                Thread.Sleep(10);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Called with arguments: " 
                 + string.Join(", ", args));
#if DEBUG
            if(Debugger.IsAttached)
            {
                Console.WriteLine("Converting files from direcoty:");
                string debugDir = "tedDirectory";
                Console.WriteLine(debugDir);
                ConvertFileList(Directory.EnumerateFiles(debugDir));
                return;
            }
#endif
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: WebXConverter [path of directory]");
                Console.WriteLine("Write \".\" to convert current directory");
                
            }
            else
            {
                if (args[0] == "h" || args[0] == "\\h" || args[0] == "?" || args[0] == "\\?" || args[0] == "/?" || args[0] == "--help" || args[0] == "help" || args[0] == "-h")
                {
                    Console.WriteLine("Usage: WebXConverter [path of directory]");
                    Console.WriteLine("Write \".\" to convert current directory");
                }
                
                IEnumerable<string> fileList;
                if (args[0] == ".")
                {
                    fileList = Directory.EnumerateFiles(Directory.GetCurrentDirectory());
                }
                else
                {
                    bool dirExists = Directory.Exists(args[0]);
                    if(!dirExists)
                    {
                        Console.WriteLine("Direcotry does not exits");
                        return;
                    }
                    fileList = Directory.EnumerateFiles(args[0]);
                }
                Console.WriteLine("Converting files from direcoty:");
                Console.WriteLine(args[1]);
                ConvertFileList(fileList);
            }
        }
    }
}
