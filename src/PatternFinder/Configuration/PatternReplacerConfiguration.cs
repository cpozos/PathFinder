using TextManipulator.Entities;
using TextManipulator.Interfaces;
using System.Collections.Generic;

namespace TextManipulator.Configuration
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
