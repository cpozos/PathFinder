using TextManipulator.Entities;

namespace TextManipulator.Interfaces
{
   public interface ILinePatternMatcher
   {
      LineMatchInfo Match(string line, uint lineIndex);
   }
}