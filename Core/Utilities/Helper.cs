using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public static class Helper
    {
        public static void FileDelete(string env,string path,string image)
        {
            string fullpath=Path.Combine(env,path,image);
            File.Delete(fullpath);
        }
    }
}
