using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Utilities.Extentions
{
    public static class FileExtention
    {
        public static string FileCreate(this IFormFile formFile,string env,string path)
        {
            string imagename = Guid.NewGuid() + formFile.FileName;
            string fullpath = Path.Combine(env, path, imagename);
            using (FileStream file = new FileStream(fullpath, FileMode.Create))
            {
               formFile.CopyTo(file);
            }
            return imagename;
        }
        
    }
}
