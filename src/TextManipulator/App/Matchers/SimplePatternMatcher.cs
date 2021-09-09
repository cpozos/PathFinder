using System.Text.RegularExpressions;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App.Matchers
{
   public class SimplePatternMatcher : IPatternMatcher
   {
      private readonly Regex _regex;

      public SimplePatternMatcher(string pattern)
      {
         _regex = new Regex(pattern);
      }

      public MatchCollection GetMatches(string input)
      {
         return _regex.Matches(input);
      }
   }
}