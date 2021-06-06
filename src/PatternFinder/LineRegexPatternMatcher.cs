using System.Collections.Generic;
using System.Text.RegularExpressions;
using PatternFinder.Interfaces;
using PatternFinder.Models;

namespace PatternFinder
{
   public class LineRegexPatternMatcher : ILinePatternMatcher
   {
      private readonly Regex _regex;

      public LineRegexPatternMatcher(string pattern)
      {
         _regex = new Regex(pattern);
      }

      public List<LineMatchInfo> Match(string line, uint lineIndex)
      {
         var lineMatches = new List<LineMatchInfo>();
         var matches = _regex.Matches(line);

         if (matches.Count > 0)
         {
            foreach (Match match in matches)
            {
               if (!match.Success)
                  continue;

               var matchInfo = new LineMatchInfo
               {
                  Line = lineIndex,
                  Column = (uint)match.Index,
                  Value = match.Value
               };
               lineMatches.Add(matchInfo);
            }
         }

         return lineMatches;
      }
   }
}