using System.Threading.Tasks;
using TextManipulator.App;
using TextManipulator.App.Configurations;
using TextManipulator.Domain.Entities;

namespace Test
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var dirConfig = new PathNode(@"D:\Projects\Net\TextFinderTestDir");
         var filterConfig = new FilterConfiguration("!*.txt;*.py", "!dir*");

         var finder = PatternFinderEngineBuilder.Build("X", dirConfig, filterConfig);
         var results = await finder.FindMatchesAsync();

         foreach (var match in results)
         {
            var info = match.FileInfo;
            var replacer = PatternReplacerBuilder.Build(match, "YZYZ");
            replacer.ReplaceAsync();
         }
      }
   }
}