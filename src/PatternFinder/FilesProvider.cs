using System.Collections.Generic;

namespace TextManipulator
{
   public class FilesProvider
   {
      public static IEnumerable<System.IO.FileInfo> GetFiles(string path, string[] filePatterns, string[] dirPatterns)
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

               if (PatternsMatcher.AnyMatch(file.Directory.Name, dirPatterns))
                  yield return file;
            }
         }
      }
   }
}
