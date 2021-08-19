using System.Collections.Generic;
using System.IO;

namespace PatternFinder.Entities
{
   public class FileMatchesInfo
   {
      private readonly object _locker = new();
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

      public void AddMatch(LineMatchInfo matchInfo)
      {
         if (matchInfo is null || matchInfo.Matches.Count < 1)
            return;

         AddThreadSafe(matchInfo);
      }

      public void AddMatches(List<LineMatchInfo> matchInfos)
      {
         if (matchInfos is null)
            return;

         foreach (var matchInfo in matchInfos)
         {
            AddMatch(matchInfo);
         }
      }

      private void AddThreadSafe(LineMatchInfo matchInfo)
      {
         lock (_locker)
            Matches.Add(matchInfo);
      }

      public override int GetHashCode()
         => FileInfo.FullName.GetHashCode(System.StringComparison.CurrentCulture);
   }
}