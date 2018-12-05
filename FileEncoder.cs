using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BenjaminAbt.FileEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = @"\in\";
            string target = @"\out\";

            Encoding fromEncoding = CodePagesEncodingProvider.Instance.GetEncoding(1252);
            Encoding toEncoding = Encoding.GetEncoding("UTF-8");

            var fileFilter = ".php|.html|.htm|.css|.js|.xml|.tpl".Split('|');

            var files = Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories);


            foreach (string fileName in files)
            {

                string fileNameRelative = fileName.Substring(source.Length);

                string newFileName = Path.Combine(target, fileNameRelative);
                string newFileDirectory = Path.GetDirectoryName(newFileName);

                Directory.CreateDirectory(newFileDirectory);

                var fileEx = Path.GetExtension(fileName).ToLower();
                if (fileFilter.Contains(fileEx))
                {
                    string contents = File.ReadAllText(fileName, fromEncoding);
                    File.WriteAllText(newFileName, contents, toEncoding);
                }
                else
                {
                    File.Copy(fileName, newFileName);
                }

                Console.WriteLine(newFileName);
            }

            Console.WriteLine("Done");
        }
    }
}
