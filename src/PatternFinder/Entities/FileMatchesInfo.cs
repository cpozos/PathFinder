using System.Collections.Generic;
using System.IO;

namespace PatternFinder.Entities
{
   public class FileMatchesInfo
   {
      public FileInfo FileInfo { get; set; }
      public List<LineMatchInfo> Matches { get; init; } = new List<LineMatchInfo>();
      public bool Success => Matches.Count > 0;
      public FileMatchesInfo()
      {

      }

      public FileMatchesInfo(FileInfo fileInfo)
      {
         FileInfo = fileInfo;
      }

      public void AddMatch(LineMatchInfo match)
      {
         if (match is null)
            return;

         Matches.Add(match);
      }

      public void AddMatches(List<LineMatchInfo> matchInfos)
      {
         if (!(matchInfos?.Count > 0))
            return;

         Matches.AddRange(matchInfos);
      }

      public override int GetHashCode()
         => FileInfo.FullName.GetHashCode(System.StringComparison.CurrentCulture);
   }
}
