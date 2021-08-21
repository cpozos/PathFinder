using TextManipulator.Domain.Interfaces;

namespace TextManipulator.Domain.Entities
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