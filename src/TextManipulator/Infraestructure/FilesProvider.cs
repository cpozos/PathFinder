using System.Collections.Generic;
using TextManipulator.App.Interfaces;

namespace TextManipulator.Infraestructure
{
   public class FilesProvider : IFilesProvider
   {
      private readonly IPatternsMatcher _dirNameMatcher;

      public FilesProvider(IPatternsMatcher dirNameMatcher)
      {
         _dirNameMatcher = dirNameMatcher;
      }

      public IEnumerable<System.IO.FileInfo> GetFiles(string path, string[] filePatterns, string[] dirPatterns)
      {
         var dirInfo = new System.IO.DirectoryInfo(path);

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
   }
}
