using PatternFinder.Interfaces;
using PatternFinder.Models;

namespace PatternFinder
{
   public record PatternFinderConfiguration
   {
      public LookingDirectoryConfiguration DirectoryConfiguration { get; set; }
      public LookingFileConfiguration FileConfiguration { get; set; }

      public FilterConfiguration FilterConfiguration { get; set; }
      public PathConfiguration PathConfiguration { get; set; }
      public ILinePatternMatcher Matcher { get; set; }

      internal PatternFinderConfiguration()
      {
      }

      public PatternFinderConfiguration(LookingFileConfiguration fileConfiguration, ILinePatternMatcher matcher)
         : this(matcher)
      {
         FileConfiguration = fileConfiguration;
      }

      public PatternFinderConfiguration(LookingDirectoryConfiguration directoryConfiguration, ILinePatternMatcher matcher)
         : this(matcher)
      {
         DirectoryConfiguration = directoryConfiguration;
      }

      public PatternFinderConfiguration(PathConfiguration pathConfiguration, FilterConfiguration filterConfiguration, ILinePatternMatcher matcher)
         : this(matcher)
      {
         FilterConfiguration = filterConfiguration;
         PathConfiguration = pathConfiguration;
      }

      private PatternFinderConfiguration(ILinePatternMatcher matcher)
      {
         Matcher = matcher;
      }
   }

   public record LookingDirectoryConfiguration
   {
      public Directory Directory { get; init; }
      public bool Recursive { get; init; }
   }

   public record LookingFileConfiguration
   {
      public File File { get; init; }
   }
}
