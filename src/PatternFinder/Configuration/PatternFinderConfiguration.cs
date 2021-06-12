using PatternFinder.Interfaces;
using System;

namespace PatternFinder.Configuration
{
   public record PatternFinderConfiguration
   {
      public IPathNode PathNode { get; init; }
      public FilterConfiguration FilterConfiguration { get; init; }
      public ILinePatternMatcher Matcher { get; init; }

      internal PatternFinderConfiguration(IPathNode pPathNode, FilterConfiguration filterConfiguration, ILinePatternMatcher matcher)
      {
         PathNode = pPathNode;
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

         if (config.PathNode is null)
            throw new ArgumentNullException($"{nameof(config.PathNode)} is null");
      }
   }
}
