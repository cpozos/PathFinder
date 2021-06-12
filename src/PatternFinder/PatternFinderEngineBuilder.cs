using PatternFinder.Interfaces;
using System;

namespace PatternFinder
{
   public class PatternFinderEngineBuilder
   {
      public static PatternFinderEngine Build(ILinePatternMatcher pMatcher, PathConfiguration pPathConfig, FilterConfiguration pFilterConfig = null)
      {
         var filterConfig = pFilterConfig ?? new FilterConfiguration();
         var settings = new PatternFinderConfiguration(pPathConfig, filterConfig, pMatcher);
         return new PatternFinderEngine(settings);
      }

      public static PatternFinderEngine Build(string pPattern, PathConfiguration pPathConfig, FilterConfiguration pFilterConfig = null)
      {
         var matcher = new LineRegexPatternMatcher(pPattern);
         return Build(matcher, pPathConfig, pFilterConfig);
      }
   }
}
