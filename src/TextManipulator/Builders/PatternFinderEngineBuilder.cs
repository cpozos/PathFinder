using TextManipulator.App.Configuration;
using TextManipulator.App.Interfaces;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;
using TextManipulator.Infraestructure;

namespace TextManipulator.App
{
   public class PatternFinderEngineBuilder
   {
      public static PatternFinderEngine Build(
         IPathNode pPathNode, 
         ILinePatternMatcher pMatcher, 
         IFilesProvider filesProvider,
         FilterConfiguration pFilterConfig = null)
      {
         var filterConfig = pFilterConfig ?? new FilterConfiguration();
         return new PatternFinderEngine(new(pPathNode, filterConfig, pMatcher), filesProvider);
      }

      public static PatternFinderEngine Build(IPathNode pPathNode, ILinePatternMatcher pMatcher, FilterConfiguration pFilterConfig = null)
      {
         return Build(pPathNode, pMatcher, new FilesProvider(new PatternsMatcher()), pFilterConfig);
      }

      public static PatternFinderEngine Build(string pPattern, IPathNode pPathNode, FilterConfiguration pFilterConfig = null)
      {
         var matcher = new LineRegexPatternMatcher(pPattern);
         return Build(pPathNode, matcher, pFilterConfig);
      }
   }
}