using TextManipulator.App.Configuration;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App
{
   public class PatternFinderEngineBuilder
   {
      public static PatternFinderEngine Build(IPathNode pPathNode, ILinePatternMatcher pMatcher, FilterConfiguration pFilterConfig = null)
      {
         var filterConfig = pFilterConfig ?? new FilterConfiguration();
         return new PatternFinderEngine(new(pPathNode, filterConfig, pMatcher));
      }

      public static PatternFinderEngine Build(string pPattern, IPathNode pPathNode, FilterConfiguration pFilterConfig = null)
      {
         var matcher = new LineRegexPatternMatcher(pPattern);
         return Build(pPathNode, matcher, pFilterConfig);
      }
   }
}