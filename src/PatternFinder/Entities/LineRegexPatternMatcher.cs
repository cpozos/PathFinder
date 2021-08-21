using System.Text.RegularExpressions;
using TextManipulator.Interfaces;

namespace TextManipulator.Entities
{
   public class LineRegexPatternMatcher : ILinePatternMatcher
   {
      private readonly Regex _regex;

      public LineRegexPatternMatcher(string pattern)
      {
         _regex = new Regex(pattern);
      }

      public LineMatchInfo Match(string line, uint lineIndex)
      {
         var lineMatches = new LineMatchInfo
         {
            LineIndex = lineIndex
         };

         var matches = _regex.Matches(line);

         if (matches.Count > 0)
         {
            foreach (Match match in matches)
            {
               if (!match.Success)
                  continue;

               lineMatches.Matches.Add(new MatchInfo
               {
                  Column = (uint)match.Index,
                  Value = match.Value
               });
            }
         }

         return lineMatches;
      }
   }
}