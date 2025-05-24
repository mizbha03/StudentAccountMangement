namespace StudentAccountMangement.Helper
{
    public class ImagePathHelper
    {
        public static string GenerateFilePath(string extension)
        {
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + extension;
            string folderPath = Path.Combine("D:\\ProfileImage", filename);

            string directory = Path.GetDirectoryName(folderPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return folderPath;
        }

        public static string SaveFile(string base64, string imagepath)
        {
            string extension = Path.GetExtension(imagepath);
            string filePath = GenerateFilePath(extension);
            byte[] fileBytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(filePath, fileBytes);
            return filePath;
        }
    }
}
