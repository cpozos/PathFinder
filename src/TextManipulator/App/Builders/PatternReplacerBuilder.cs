using TextManipulator.App.Configurations;
using TextManipulator.App.Replacers;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App
{
   public class PatternReplacerBuilder
   {
      public static PatternReplacer Build(FileMatches pFileMatches, IMatchReplacer pReplacer, string pFilePath = null)
      {
         FileReplaceInfo info = new(pReplacer, pFileMatches);
         return new PatternReplacer(new PatternReplacerConfiguration(info, pFilePath));
      }

      public static PatternReplacer Build(FileMatches pFileMatches, string pNewValue, string pFilePath = null)
      {
         var replacer = new SimpleMatchReplacer(pNewValue);
         return Build(pFileMatches, replacer, pFilePath);
      }
   }
}