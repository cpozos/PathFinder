namespace PatternFinder.Interfaces
{
   public interface IPathNode
   {
      public string Path { get; init; }
      public bool IsDirectory { get; }
      public bool IsFile { get; }
   }
}