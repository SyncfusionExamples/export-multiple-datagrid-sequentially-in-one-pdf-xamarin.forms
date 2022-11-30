using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataGridXamarin
{
    public interface ISave
    {
        void Save(string filename, string contentType, MemoryStream stream);
    }
    public interface ISaveWindowsPhone
    {
        Task Save(string filename, string contentType, MemoryStream stream);
    }

    public interface ISaveWindows
    {
        Task Save(string filename, string contentType, MemoryStream stream);
    }

}
