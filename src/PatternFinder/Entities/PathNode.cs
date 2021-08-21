using TextManipulator.Interfaces;
using System.IO;

namespace TextManipulator.Entities
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