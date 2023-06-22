﻿using DayzMapsLoader.Core.Contracts.Infrastructure.Services;

using Newtonsoft.Json;
using System.Text;

namespace DayzMapsLoader.Infrastructure.Services;

public class FileService : IFileService
{
    public T Read<T>(string folderPath, string fileName)
    {
        var path = Path.Combine(folderPath, fileName);
        if (!File.Exists(path))
            return default!;

        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json)!;

    }

    public void Save<T>(string folderPath, string fileName, T content)
    {
        if (content == null)
            throw new ArgumentNullException(nameof(content), "Content cannot be null.");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileContent = JsonConvert.SerializeObject(content);
        File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
    }

    public void Delete(string folderPath, string? fileName)
    {
        if (fileName != null && File.Exists(Path.Combine(folderPath, fileName)))
        {
            File.Delete(Path.Combine(folderPath, fileName));
        }
    }
}