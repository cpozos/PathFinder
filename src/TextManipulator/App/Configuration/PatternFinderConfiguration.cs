using TextManipulator.Domain.Interfaces;
using System;

namespace TextManipulator.App.Configuration
{
   public record PatternFinderConfiguration
   {
      public IPathNode PathNode { get; init; }
      public FilterConfiguration FilterConfiguration { get; init; }
      public ILinePatternMatcher LineMatcher { get; init; }

      internal PatternFinderConfiguration(IPathNode pPathNode, FilterConfiguration filterConfiguration, ILinePatternMatcher matcher)
      {
         PathNode = pPathNode;
         FilterConfiguration = filterConfiguration;
         LineMatcher = matcher;

         ValidateConfiguration(this);
      }

      private static void ValidateConfiguration(PatternFinderConfiguration config)
      {
         if (config is null)
            throw new ArgumentNullException("Settings are null");

         if (config.LineMatcher is null)
            throw new ArgumentNullException($"{nameof(config.LineMatcher)} is null");

         if (config.PathNode is null)
            throw new ArgumentNullException($"{nameof(config.PathNode)} is null");
      }
   }
}