using TextManipulator.Domain.Interfaces;

namespace TextManipulator.Domain.Entities
{
   public class FileReplaceInfo
   {
      public IMatchReplacer MatchReplacer { get; }
      public FileMatches MatchesInfo { get; }

      public FileReplaceInfo(IMatchReplacer replacer, FileMatches matchesInfo)
      {
         //TODO validate
         MatchReplacer = replacer;
         MatchesInfo = matchesInfo;
      }
   }
}