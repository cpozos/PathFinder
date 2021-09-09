using System.Text.RegularExpressions;

namespace TextManipulator.Domain.Interfaces
{
   public interface IPatternMatcher
   {
      public MatchCollection GetMatches(string input);
   }
}