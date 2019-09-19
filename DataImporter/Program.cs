using DataImporter.Core;
using DataImporter.CrossCuttingConcern.Logging;
using DataImporter.XmlImport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.korayspor.com/grisport.xml";
            string configFilePath = "SupplierConfigFiles\\KoraySpor\\config.xml";
            if(args.Length > 0)
            {
                url = args[0];
                configFilePath = args[1];
            }

            string programPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            configFilePath = Path.Combine(programPath, configFilePath);
            
            ImportManeger importManeger = 
                new ImportManeger(new XmlImporter(url, configFilePath), new Logger());
            importManeger.TransferDatas();
        }
    }
}
