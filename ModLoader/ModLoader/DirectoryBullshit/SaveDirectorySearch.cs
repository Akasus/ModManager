// File: SaveDirectorySeracher.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

using System.Collections.Concurrent;

namespace ModLoader.DirectoryBullshit;

public static class SaveDirectorySearch
{
    public static IEnumerable<DirectoryInfo> FindDirectories(this DirectoryInfo directory, string patternMatch, SearchOption searchOption)
    {
        var foundDirs = new List<DirectoryInfo>();
        var fileAttrib = FileAttributes.Offline | FileAttributes.Hidden | FileAttributes.System |
                         FileAttributes.Temporary | FileAttributes.Archive;
        var SearchOptions = new EnumerationOptions
            { AttributesToSkip = fileAttrib, ReturnSpecialDirectories = false, IgnoreInaccessible = true, RecurseSubdirectories = searchOption == SearchOption.AllDirectories };

        if (patternMatch.Contains(Path.DirectorySeparatorChar))
        {
            var paths = patternMatch.Split(Path.DirectorySeparatorChar);
            var matches = directory.EnumerateDirectories(paths[0], SearchOptions);

            for (int i = 1; i < paths.Length; i++)
            {
                var temp = new List<DirectoryInfo>();
                foreach (var dir in matches)
                {
                    temp.AddRange(dir.FindDirectories(paths[i], SearchOption.TopDirectoryOnly));
                }
                matches = temp;
            }

            return matches;
        }

        return directory.EnumerateDirectories(patternMatch, SearchOptions);
        
    }
}