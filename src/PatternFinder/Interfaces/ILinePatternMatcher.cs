using System.Collections.Generic;
using PatternFinder.Models;

namespace PatternFinder.Interfaces
{
   public interface ILinePatternMatcher
   {
      LineMatchInfo Match(string line, uint lineIndex);
   }
}