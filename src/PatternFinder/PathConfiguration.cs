using System.IO;

namespace PatternFinder
{
   public class PathConfiguration
   {
      public string Path { get; init; } 
      public bool IsDirectory { get; init; }
      public PathConfiguration(string path)
      {
         if (!File.Exists(path) && !Directory.Exists(path))
         {
            throw new DirectoryNotFoundException();
         }
            

         IsDirectory = File.GetAttributes(path).HasFlag(FileAttributes.Directory);
         Path = path;
      }
   }
}
