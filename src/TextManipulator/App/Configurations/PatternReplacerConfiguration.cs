using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;
using System.Collections.Generic;

namespace TextManipulator.App.Configurations
{
   public class PatternReplacerConfiguration
   {
      public string FilePath { get; }
      public IList<FileMatch> Matches { get; }
      public IMatchReplacer MatchReplacer { get; }

      public PatternReplacerConfiguration(FileReplaceInfo replaceInfo, string pathNewFile = null)
      {
         FilePath = pathNewFile ?? replaceInfo.MatchesInfo.FileInfo.FullName;
         Matches = replaceInfo.MatchesInfo.Matches.Values;
         MatchReplacer = replaceInfo.MatchReplacer;
      }
   }
}
