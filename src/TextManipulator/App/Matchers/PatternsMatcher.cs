using System.Text.RegularExpressions;
using TextManipulator.App.Interfaces;

namespace TextManipulator.App
{
   public class PatternsMatcher : IPatternsMatcher
   {
      public bool MatchAnyPattern(string value, string[] patterns)
      {
         bool matched = false;
         foreach (var pattern in patterns)
         {
            Regex regex;
            if (pattern.StartsWith('!'))
            {
               regex = new Regex(pattern[1..]);
               matched = !regex.IsMatch(value);
            }
            else
            {
               regex = new Regex(pattern);
               matched = regex.IsMatch(value);
            }

            if (matched)
               break;
         }

         return matched;
      }
   }
}