using PatternFinder.Interfaces;

namespace PatternFinder.Entities
{
   public class FileReplaceInfo
   {
      public IMatchReplacer MatchReplacer { get; }
      public FileMatchesInfo MatchesInfo { get; }

      public FileReplaceInfo(IMatchReplacer replacer, FileMatchesInfo matchesInfo)
      {
         //TODO validate
         MatchReplacer = replacer;
         MatchesInfo = matchesInfo;
      }
   }
}