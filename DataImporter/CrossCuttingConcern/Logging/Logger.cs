using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.CrossCuttingConcern.Logging
{
    public class Logger : ILogger
    {
        public void Log(string xmlContext)
        {//burada herhangi bir loglama aracı kullanılabilinir fakat ben direk dosyaya kaydedeceğim.
            FileSecurity fSecurity = new FileSecurity();
            string fileName = DateTime.Now.ToShortDateString();
            fileName =Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())),
               "CrossCuttingConcern\\LoggFiles", fileName);
            using (FileStream fs = File.Create(fileName, 1024, FileOptions.WriteThrough))
            {
                byte[] context = new UTF8Encoding(true).GetBytes(xmlContext);
                fs.Write(context, 0, xmlContext.Length);
            }
            Console.WriteLine("Loglama işlemi gerçekleşti. " + fileName);
            
        }
    }
}
