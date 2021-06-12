using PatternFinder.Configuration;
using PatternFinder.Entities;
using PatternFinder.Interfaces;

namespace PatternFinder
{
   public class PatternFinderEngineBuilder
   {
      public static PatternFinderEngine Build(IPathNode pPathNode, ILinePatternMatcher pMatcher, FilterConfiguration pFilterConfig = null)
      {
         var filterConfig = pFilterConfig ?? new FilterConfiguration();
         var settings = new PatternFinderConfiguration(pPathNode, filterConfig, pMatcher);
         return new PatternFinderEngine(settings);
      }

      public static PatternFinderEngine Build(string pPattern, IPathNode pPathNode, FilterConfiguration pFilterConfig = null)
      {
         var matcher = new LineRegexPatternMatcher(pPattern);
         return Build(pPathNode, matcher, pFilterConfig);
      }
   }
}
