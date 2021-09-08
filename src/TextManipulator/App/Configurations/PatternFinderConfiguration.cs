using TextManipulator.Domain.Interfaces;
using System;
using TextManipulator.App.Interfaces;

namespace TextManipulator.App.Configurations
{
   public record PatternFinderConfiguration
   {
      public IPathNode PathNode { get; init; }
      public FilterConfiguration FilterConfiguration { get; init; }
      public ILinePatternMatcher LineMatcher { get; init; }
      public IFilesProvider FilesProvider { get; init; }

      internal PatternFinderConfiguration(IPathNode pPathNode, IFilesProvider filesProvider, FilterConfiguration filterConfiguration, ILinePatternMatcher matcher)
      {
         PathNode = pPathNode;
         FilterConfiguration = filterConfiguration;
         LineMatcher = matcher;
         FilesProvider = filesProvider;

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

         if (config.FilesProvider is null)
            throw new ArgumentNullException($"{nameof(config.FilesProvider)} is null");
      }
   }
}