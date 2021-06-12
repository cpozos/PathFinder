using System.Collections.Generic;

namespace PatternFinder.Models
{
   public class LineMatchInfo
   {
      public uint LineIndex { get; set; }

      public List<MatchInfo> Matches { get; init; } = new();
   }
}