using TextManipulator.Configuration;
using TextManipulator.Entities;
using TextManipulator.Interfaces;
using System.Collections.Generic;

namespace TextManipulator
{
   public class PatternReplacerBuilder
   {
      public static PatternReplacer Build(FileMatchesInfo fileMatchesInfo, IMatchReplacer replacer, string filePath = null)
      {
         FileReplaceInfo info = new(replacer, fileMatchesInfo);
         return new PatternReplacer(new PatternReplacerConfiguration(info, filePath));
      }

      public static PatternReplacer Build(FileMatchesInfo fileMatchesInfo, string newValue, string filePath = null)
      {
         return Build(fileMatchesInfo, new SimpleMatchReplacer(newValue), filePath);
      }
   }
}