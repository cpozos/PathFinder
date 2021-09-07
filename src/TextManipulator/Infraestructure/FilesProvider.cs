using System.Collections.Generic;
using System.IO;
using TextManipulator.App.Interfaces;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.Infraestructure
{
   public class FilesProvider : IFilesProvider
   {
      private readonly IPatternsMatcher _dirNameMatcher;
      private readonly IPathNode _pathNode;

      public FilesProvider(IPatternsMatcher dirNameMatcher, IPathNode pathNode)
      {
         _dirNameMatcher = dirNameMatcher;
         _pathNode = pathNode;
      }

      public IEnumerable<FileInfo> GetFiles(string[] filePatterns, string[] dirPatterns)
      {
         if (_pathNode.IsDirectory)
         {
            if (filePatterns is null)
               filePatterns = new string[] { "*.*" };

            return GetFilesFromDirectory(filePatterns, dirPatterns);
         }
         else
         {
            return GetFile();
         }
      }

      private IEnumerable<FileInfo> GetFilesFromDirectory(string[] filePatterns, string[] dirPatterns)
      {
         var dirInfo = new DirectoryInfo(_pathNode.Path);

         foreach (var pattern in filePatterns)
         {
            var files = dirInfo.EnumerateFiles(pattern, System.IO.SearchOption.AllDirectories);
            foreach (var file in files)
            {
               if (file.DirectoryName is not string dirName || string.Equals(dirName.ToString(), dirInfo.FullName.ToString()))
               {
                  yield return file;
                  continue;
               }

               if (_dirNameMatcher.MatchAnyPattern(file.Directory.Name, dirPatterns))
                  yield return file;
            }
         }
      }

      private IEnumerable<FileInfo> GetFile()
      {
         return new List<FileInfo>() { new FileInfo(_pathNode.Path) };
      }
   }
}
