using System.Collections.Generic;
using System.Text.RegularExpressions;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App.Matchers
{
   public class LineRegexPatternMatcher : ILinePatternMatcher
   {
      private readonly Regex _regex;

      public LineRegexPatternMatcher(string pattern)
      {
         _regex = new Regex(pattern);
      }

      public IEnumerable<FileMatch> Match(string line, int lineIndex)
      {
         var lineMatches = new List<FileMatch>();

         var matches = _regex.Matches(line);

         if (matches.Count > 0)
         {
            foreach (Match match in matches)
            {
               if (!match.Success)
                  continue;

               var startPos = new FilePosition(lineIndex, match.Index);
               var endPos = new FilePosition(lineIndex, match.Index + match.Length);

               lineMatches.Add(new FileMatch
               {
                  StartPosition = startPos,
                  EndPosition = endPos,
                  Value = match.Value
               });
            }
         }

         return lineMatches;
      }
   }
}