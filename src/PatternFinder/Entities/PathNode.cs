using PatternFinder.Interfaces;
using System.IO;

namespace PatternFinder.Entities
{
   public class PathNode : IPathNode
   {
      public string Path { get; init; }

      public bool IsDirectory =>
         Directory.Exists(Path) &&
         File.GetAttributes(Path).HasFlag(FileAttributes.Directory);

      public bool IsFile => File.Exists(Path);

      public PathNode(string path)
      {
         Path = path;
      }
   }
}