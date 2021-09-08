using TextManipulator.App.Configurations;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App
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