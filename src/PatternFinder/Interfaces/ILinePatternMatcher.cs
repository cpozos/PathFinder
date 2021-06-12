using PatternFinder.Entities;

namespace PatternFinder.Interfaces
{
   public interface ILinePatternMatcher
   {
      LineMatchInfo Match(string line, uint lineIndex);
   }
}