using PatternFinder.Interfaces;
using System;

namespace PatternFinder
{
   public record PatternFinderConfiguration
   {
      public PathConfiguration PathConfiguration { get; init; }
      public FilterConfiguration FilterConfiguration { get; init; }
      public ILinePatternMatcher Matcher { get; init; }

      internal PatternFinderConfiguration(PathConfiguration pathConfiguration, FilterConfiguration filterConfiguration, ILinePatternMatcher matcher)
      {
         PathConfiguration = pathConfiguration;
         FilterConfiguration = filterConfiguration;
         Matcher = matcher;

         ValidateConfiguration(this);
      }

      private static void ValidateConfiguration(PatternFinderConfiguration config)
      {
         if (config is null)
            throw new ArgumentNullException("Settings are null");

         if (config.Matcher is null)
            throw new ArgumentNullException($"{nameof(config.Matcher)} is null");

         if (config.PathConfiguration is null)
            throw new ArgumentNullException($"{nameof(config.PathConfiguration)} is null");
      }
   }
}
