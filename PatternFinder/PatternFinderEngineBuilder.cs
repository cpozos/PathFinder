using System;

namespace PatternFinder
{
   public class PatternFinderEngineBuilder
   {
      public static PatternFinderEngine Build(PatternFinderConfiguration settings)
      {
         ValidateConfiguration(settings);
         return new PatternFinderEngine(settings);
      }

      public static PatternFinderEngine Build(LookingFileConfiguration fileConfig, string pattern)
      {
         var settings = new PatternFinderConfiguration
         {
            FileConfiguration = fileConfig,
            Matcher = new LineRegexPatternMatcher(pattern)
         };

         return new PatternFinderEngine(settings);
      }

      public static PatternFinderEngine Build(LookingDirectoryConfiguration dirConfig, string pattern)
      {
         var settings = new PatternFinderConfiguration
         {
            DirectoryConfiguration = dirConfig,
            Matcher = new LineRegexPatternMatcher(pattern)
         };

         return new PatternFinderEngine(settings);
      }

      public static PatternFinderEngine Build(PathConfiguration pathConfig, string pattern, FilterConfiguration filterConfiguration = null)
      {
         var settings = new PatternFinderConfiguration
         {
            FilterConfiguration = filterConfiguration?? new FilterConfiguration(),
            PathConfiguration = pathConfig,
            Matcher = new LineRegexPatternMatcher(pattern)
         };

         return new PatternFinderEngine(settings);
      }

      private static void ValidateConfiguration(PatternFinderConfiguration config)
      {
         if (config is null)
            throw new ArgumentNullException("Settings are null");

         if (config.Matcher is null)
            throw new ArgumentNullException("Macther is null");

         if (config.DirectoryConfiguration is null && config.FileConfiguration is null)
            throw new ArgumentNullException("DirectoryConfiguration is null and FileConfiguration is null");
      }
   }
}
