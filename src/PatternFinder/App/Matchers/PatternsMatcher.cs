using System.Text.RegularExpressions;

namespace TextManipulator.App
{
   public class PatternsMatcher
   {
      public static bool AnyMatch(string name, string[] patterns)
      {
         bool matched = false;
         foreach (var pattern in patterns)
         {
            Regex regex;
            if (pattern.StartsWith('!'))
            {
               regex = new Regex(pattern[1..]);
               matched = !regex.IsMatch(name);
            }
            else
            {
               regex = new Regex(pattern);
               matched = regex.IsMatch(name);
            }

            if (matched)
               break;
         }

         return matched;
      }
   }
}