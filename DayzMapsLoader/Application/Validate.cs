using System.Reflection;

namespace RequestsHub.Application;

internal static class Validate
{
    //TODO: write comment.
    internal static string PathToSave(string pathsToSave)
    {
        if (Directory.Exists(pathsToSave))
        {
            return pathsToSave;
        }
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            ?? throw new ArgumentNullException("Can't get a path to save.");
    }
}