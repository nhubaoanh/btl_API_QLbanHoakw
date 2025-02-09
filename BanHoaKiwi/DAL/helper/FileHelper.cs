using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.helper
{
    public class FileHelper
    {
        private readonly string _rootPath;

        public FileHelper(string rootPath)
        {
            _rootPath = rootPath;
        }

        public string SaveFileFromBase64String(string relativePathFileName, string base64Data)
        {
            if (base64Data.Contains("base64,"))
            {
                base64Data = base64Data.Substring(base64Data.IndexOf("base64,") + 7);
            }
            return WriteFile(relativePathFileName, base64Data);
        }

        public string WriteFile(string relativePathFileName, string base64Data)
        {
            try
            {
                string fullPath = Path.Combine(_rootPath, relativePathFileName);
                string folderPath = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                File.WriteAllBytes(fullPath, Convert.FromBase64String(base64Data));
                return fullPath;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public string CreatePathFile(string RelativePathFileName)
        {
            try
            {
                string serverRootPathFolder = _rootPath;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                return fullPathFile;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // convert ảnh sang base64
        public string ConvertImageToBase64(string imgepath)
        {
            try
            {
                // đọc ảnh dưới dạng byte
                byte[] imageBytes = File.ReadAllBytes(imgepath);

                // chuyển đổi byte[] thành chuỗi base64
                string base64string = Convert.ToBase64String(imageBytes);
                return base64string;
            }catch (Exception ex)
            {
                return $"Error : {ex.Message}";
            }
        }
    }
}
