using PatternFinder.Entities;
using PatternFinder.Interfaces;
using System.Collections.Generic;

namespace PatternFinder.Configuration
{
   public class PatternReplacerConfiguration
   {
      public string FilePath { get; }
      public List<LineMatchInfo> LineMatches { get; }
      public IMatchReplacer MatchReplacer { get; }

      public PatternReplacerConfiguration(FileReplaceInfo replaceInfo, string pathNewFile = null)
      {
         FilePath = pathNewFile ?? replaceInfo.MatchesInfo.FileInfo.FullName;
         LineMatches = replaceInfo.MatchesInfo.Matches;
         MatchReplacer = replaceInfo.MatchReplacer;
      }
   }
}
