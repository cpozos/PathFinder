using PatternFinder.Interfaces;

namespace PatternFinder.Models
{
   public class Directory : IPathNode
   {
      public string Path { get; set; }

      public Directory(string path)
      {
         Path = path;
      }
   }
}