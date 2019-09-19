using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Core
{
    public interface IImporter
    {
        string LoadData();
        bool Validate();
        bool SaveData();
        
    }
}
