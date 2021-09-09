using TextManipulator.Domain.Entities;

namespace TextManipulator.Domain.Interfaces
{
   public interface IFilePatternMatcher
   {
      FileMatches Match(System.IO.FileInfo fileInfo);
   }
}
