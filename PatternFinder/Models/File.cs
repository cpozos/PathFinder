using PatternFinder.Interfaces;

namespace PatternFinder.Models
{
   public class File : IPathNode
   {
      public string Path { get; set; }
      public string Extension { get; set; }
   }
}