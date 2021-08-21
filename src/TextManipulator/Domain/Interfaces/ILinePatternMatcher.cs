using TextManipulator.Domain.Entities;

namespace TextManipulator.Domain.Interfaces
{
   public interface ILinePatternMatcher
   {
      LineMatchInfo Match(string line, uint lineIndex);
   }
}