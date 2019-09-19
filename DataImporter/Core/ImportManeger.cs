using DataImporter.CrossCuttingConcern.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Core
{
    public class ImportManeger
    {
        IImporter importer;
        ILogger logger;
        //bu işlem sadece xml için değil diğer aktarım türleri içinde yapılabilsin diye böyle yaptım
        //mesela excell
        public ImportManeger(IImporter _importer, ILogger _logger)
        {
            importer = _importer;
            logger = _logger;
        }
        
        public bool TransferDatas()
        {
            string xmlContext = importer.LoadData();
            if(importer.Validate())
            {
                importer.SaveData();
                logger.Log(xmlContext);
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
    }
}
