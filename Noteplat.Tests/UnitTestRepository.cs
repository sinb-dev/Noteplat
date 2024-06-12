using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteplat.Tests;

public class UnitTestRepository : IRepository
{
    Dictionary<string, string> Files = new();
    string _filename = "";
    public UnitTestRepository() 
    {
        Files = new()
        {
            { "random.txt", "Hello" }
        };
    }
    public void SetFilePick(string filename)
    {
        _filename = filename;
    }

    public string Load(string filename)
    {
        if (Files.ContainsKey(filename))
        {
            return Files[filename];
        }
        return "";
    }

    public async Task<string> PickFile()
    {
        return await Task.Run(() => {
            return _filename;
        });
    }

    public void Save(string filename, string contents)
    {
        Files[filename] = contents;
    }
}
