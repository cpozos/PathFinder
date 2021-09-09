using System.Collections.Generic;
using System.IO;

namespace TextManipulator.Domain.Entities
{
   public class FileMatches
   {
      private readonly object _locker = new();
      public FileInfo FileInfo { get; set; }
      public SortedList<uint, FileMatch> Matches { get; init; } = new();
      public bool Success => Matches.Count > 0;
      public FileMatches()
      {

      }
      public FileMatches(FileInfo fileInfo)
      {
         FileInfo = fileInfo;
      }

      public void Add(FileMatch pMatch)
      {
         if (pMatch is null)
            return;

         AddThreadSafe(pMatch);
      }

      public void Add(IEnumerable<FileMatch> pMatches)
      {
         if (pMatches is null)
            return;

         foreach (var matchInfo in pMatches)
         {
            Add(matchInfo);
         }
      }

      private void AddThreadSafe(FileMatch pMatch)
      {
         lock (_locker)
            Matches.Add(pMatch.Id, pMatch);
      }

      public override int GetHashCode()
         => FileInfo.FullName.GetHashCode(System.StringComparison.CurrentCulture);
   }
}