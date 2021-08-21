using System.Collections.Generic;

namespace TextManipulator.Entities
{
   public class LineMatchInfo
   {
      public uint LineIndex { get; set; }

      public List<MatchInfo> Matches { get; init; } = new();
   }
}