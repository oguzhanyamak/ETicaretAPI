using ETicaretAPI.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileServices : IFileService
    {

        readonly IWebHostEnvironment _webHostEnvironment;
        public FileServices(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {

            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await fileStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo Log
                throw ex;
            }



        }

        public Task<string> FileRenameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<(string fileName,string path)>> uploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,path);//uygulama içinde nerede olursa olsun wwwroot yolunu getirecektir.

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if(results.TrueForAll(r => r.Equals(true)))
            {
                return datas;
            }
            return null;

            
        }
    }
}
