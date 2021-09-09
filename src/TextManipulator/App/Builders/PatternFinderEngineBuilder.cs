using System.Collections.Generic;
using TextManipulator.App.Configurations;
using TextManipulator.App.Interfaces;
using TextManipulator.App.Matchers;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;
using TextManipulator.Infraestructure;

namespace TextManipulator.App
{
   public class PatternFinderEngineBuilder
   {

      public static PatternFinderEngine Build(
         IPathNode pPathNode,
         IFilePatternMatcher pFileMatcher,
         ILinePatternMatcher pMatcher,
         IFilesProvider filesProvider,
         FilterConfiguration pFilterConfig)
      {
         var config = new PatternFinderConfiguration(pPathNode, filesProvider, pFilterConfig, pFileMatcher, pMatcher);
         return new PatternFinderEngine(config);
      }

      public static PatternFinderEngine Build(IPathNode pPathNode, IFilePatternMatcher pFileMatcher, ILinePatternMatcher pMatcher, IFilesProvider pFilesProvider)
      {
         // Default values
         var filterConfig = new FilterConfiguration();

         return Build(pPathNode, pFileMatcher, pMatcher, pFilesProvider, filterConfig);
      }

      public static PatternFinderEngine Build(IPathNode pPathNode, IFilePatternMatcher pFileMatcher, ILinePatternMatcher pMatcher, FilterConfiguration pFilterConfig = null)
      {
         // Default values
         var dirNameMatcher = new PatternsMatcher();
         var filesProvider = new FilesProvider(dirNameMatcher, pPathNode);

         return Build(pPathNode, pFileMatcher, pMatcher, filesProvider, pFilterConfig);
      }

      public static PatternFinderEngine Build(string pPattern, IPathNode pPathNode, FilterConfiguration pFilterConfig = null)
      {
         // Default values
         var patternMatcher = new SimplePatternMatcher(pPattern);
         var fileMatcher = new FilePatternMatcher(patternMatcher);
         var lineMatcher = new LineRegexPatternMatcher(pPattern);

         return Build(pPathNode, fileMatcher, lineMatcher, pFilterConfig);
      }
   }
}