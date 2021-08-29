using TextManipulator.Domain.Entities;

namespace TextManipulator.Domain.Interfaces
{
   public interface IFileLineMatcher
   {
      FileMatchesInfo Match(System.IO.FileInfo fileInfo, ILinePatternMatcher linePatternMatcher);
   }
}