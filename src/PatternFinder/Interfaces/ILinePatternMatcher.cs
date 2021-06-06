using System.Collections.Generic;
using PatternFinder.Models;

namespace PatternFinder.Interfaces
{
   public interface ILinePatternMatcher
   {
      List<LineMatchInfo> Match(string line, uint lineIndex);
   }
}