using Final.Core.Interfaces;

namespace Final.Core.Services
{
    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private readonly string _basePath;

        public FileSystemSaveLoadService(string basePath)
        {
            _basePath = basePath;
        }

        public void SaveData(string data, string identifier)
        {
            try
            {
                if (!Directory.Exists(_basePath))
                {
                    Directory.CreateDirectory(_basePath);
                }

                string filePath = Path.Combine(_basePath, $"{identifier}.txt");
                File.WriteAllText(filePath, data);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save data: {ex.Message}", ex);
            }
        }

        public string LoadData(string identifier)
        {
            try
            {
                string filePath = Path.Combine(_basePath, $"{identifier}.txt");
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to load data: {ex.Message}", ex);
            }
        }
    }
}