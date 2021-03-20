using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Core.Utilities.FileHelper
{
    public class FileHelper
    {
        public static string AddAsync(IFormFile formFile)
        {
            var result = createNewPath(formFile);
            try
            {
                var sourcePath = Path.GetTempFileName();
                using (var stream = new FileStream(sourcePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                File.Move(sourcePath, result.newPath);
                return result.path2;
            }
            catch (Exception exception)
            {

                return exception.Message;
            }
        }

        public static string UpdateAsync(string sourcePath, IFormFile formFile)
        {
            var result = createNewPath(formFile);

            try
            {
                using (var stream = new FileStream(result.newPath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                File.Delete(sourcePath);
                return result.path2;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public static IResult DeleteAsync(string path)
        {
            try
            {
                File.Delete(path);
                return new SuccessResult();
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
                throw;
            }
        }

        public static (string newPath, string path2) createNewPath(IFormFile formFile)
        {
            FileInfo fileInfo = new FileInfo(formFile.FileName);
            string fileExtension = fileInfo.Extension;
            var uniqueFileName = Guid.NewGuid().ToString("N") + fileExtension;
            string result = $"{Environment.CurrentDirectory + $@"\wwwroot\Images\"}{uniqueFileName}";
            return (result, $@"\Images\{uniqueFileName}");
        }
    }
}
