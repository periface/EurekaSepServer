using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ImageMagick;

namespace Eureka.Spe.FileUpload
{
    public class FileSaver
    {
        /// <summary>
        /// This version is not for web api
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static SaveFileOutput SaveFile(SaveFileInput input)
        {
            var relativeFolder = "/" + input.StaticFolder;
            var serverRoute = input.RootPath;
            var fileAbsoluteFolder = serverRoute + input.StaticFolder;
            var extension = Path.GetExtension(input.File.FileName);
            var uniqueFileName = Guid.NewGuid().ToString("N").Substring(0, 10);
            var storeFileName = uniqueFileName + extension;
            var sizes = new Dictionary<string, string>();
            using (var stream = input.File.InputStream)
            {
                if (!Directory.Exists(fileAbsoluteFolder))
                {
                    Directory.CreateDirectory(fileAbsoluteFolder);
                }

                var relativeFileFolder = relativeFolder + "/" + input.UniqueFolder;
                var fileAbsoluteDirectory = fileAbsoluteFolder + "\\" + input.UniqueFolder;
                if (!Directory.Exists(fileAbsoluteDirectory)) Directory.CreateDirectory(fileAbsoluteDirectory);
                if (input.ClearFolder) ClearContents(fileAbsoluteDirectory);
                var imgLocation = fileAbsoluteDirectory + "\\" + storeFileName;
                File.WriteAllBytes(imgLocation, ReadFully(stream));
                var fileLocation = GetServerPath(input) + relativeFileFolder + "/" + storeFileName;
                //Optimizations
                if (input.OptimizationOptions != null && input.OptimizationOptions.Optimize)
                {
                    foreach (var optimizationOptionsSize in input.OptimizationOptions.Sizes)
                    {
                        var fixedSize = optimizationOptionsSize.Split('x');
                        var leftSide = int.Parse(fixedSize[0]);
                        var rightSide = int.Parse(fixedSize[1]);
                        var img = ReziseImg(leftSide, rightSide, imgLocation, fileAbsoluteDirectory + "\\");
                        if (input.OptimizationOptions.Optimize)
                        {
                            if (Path.GetExtension(img.FileName) != ".gif")
                            {
                                OptimizeImg(img.FileLocation);
                            }
                        }
                        sizes.Add(optimizationOptionsSize, GetServerPath(input) + relativeFileFolder + "/" + img.FileName);

                    }
                    if (input.OptimizationOptions.Optimize)
                    {
                        if (Path.GetExtension(imgLocation) != ".gif")
                        {
                            OptimizeImg(imgLocation);
                        }
                    }
                }
                //!Optimizations

                return new SaveFileOutput()
                {
                    FileUrl = fileLocation,
                    File = imgLocation,
                    Sizes = sizes
                };
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static SaveFileOutput SaveFileFromBase64String(string bs64, string uniqueFolder, string staticFolder, string serverRoute)
        {
            var uniqueFileName = Guid.NewGuid().ToString("N").Substring(0, 10);
            var extension = GetExtensionFromBase64String(bs64);
            var storeFileName = uniqueFileName + extension;
            var fileAbsoluteFolder = serverRoute + "\\" + staticFolder;

            if (!Directory.Exists(fileAbsoluteFolder)) Directory.CreateDirectory(fileAbsoluteFolder);


            var fileAbsoluteDirectory = fileAbsoluteFolder + "\\" + uniqueFolder;
            if (!Directory.Exists(fileAbsoluteDirectory)) Directory.CreateDirectory(fileAbsoluteDirectory);

            var imgLocation = fileAbsoluteDirectory + "\\" + storeFileName;

            var base64Data = bs64.Split(',');

            File.WriteAllBytes(imgLocation, Convert.FromBase64String(base64Data[1]));

            return new SaveFileOutput()
            {
                File = imgLocation
            };
        }

        private static string GetExtensionFromBase64String(string base64String)
        {
            var split = base64String.Split('/');
            string extension = string.Empty;
            if (split.Length > 0)
            {
                extension = split[1].Split(';')[0];
            }
            if (string.IsNullOrEmpty(extension)) return string.Empty;
            return "." + extension;
        }

        private static void ClearContents(string fileAbsoluteDirectory)
        {
            var files = Directory.GetFiles(fileAbsoluteDirectory);

            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception)
                {
                    //Ignore;
                }
            }

        }

        private static void OptimizeImg(string imgLocation)
        {
            var snakewareLogo = new FileInfo(imgLocation);
            var optimizer = new ImageOptimizer { OptimalCompression = true };
            optimizer.Compress(snakewareLogo);
            snakewareLogo.Refresh();
        }

        private static ResizeResult ReziseImg(int width, int height, string img, string outputDirectory)
        {
            using (var image = new MagickImage(img))
            {
                var size = new MagickGeometry(width, height) { IgnoreAspectRatio = true };
                image.Resize(size);
                var newFileName = $"{GetImgNameWithoutExt(img)}_{width}x{height}{Path.GetExtension(img)}";
                var outputFolder =
                    $"{outputDirectory}{newFileName}";
                image.Write(outputFolder);
                return new ResizeResult
                {
                    FileLocation = outputFolder,
                    FileName = newFileName
                };
            }
        }

        private static string GetImgNameWithoutExt(string img)
        {
            return Path.GetFileNameWithoutExtension(img);
        }
        private static string GetServerPath(SaveFileInput input)
        {
            if (input.ApiEnabled)
            {
                return input.Request.Request.Url.Scheme + "://" + input.Request.Request.Url.Authority;
            }
            return string.Empty;
        }

        public static void DeleteFile(string rootFolder, string staticFolder, string uniqueFolder, string feedImg)
        {
            var fileName = Path.GetFileName(feedImg);

            try
            {
                File.Delete(rootFolder + "\\" + staticFolder + "\\" + uniqueFolder + "\\" + fileName);
            }
            catch (Exception)
            {
                //Ignore
            }

        }

        private class ResizeResult
        {
            public string FileName { get; set; }
            public string FileLocation { get; set; }
        }

        public static string ConverToBase64(string fileLocationFile)
        {
            var bytes = File.ReadAllBytes(fileLocationFile);
            var file = Convert.ToBase64String(bytes);
            return file;
        }
    }
}
