using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteplat;

public interface IRepository
{
    string Load(string filename);
    void Save(string filename, string contents);
    Task<string> PickFile();
}
