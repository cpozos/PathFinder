using System.Threading.Tasks;
using TextManipulator;
using TextManipulator.Configuration;
using TextManipulator.Entities;

namespace Test
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var dirConfig = new PathNode(@"D:\TextFinderTestDir");
         var filterConfig = new FilterConfiguration("!*.txt;*.py", "!dir*");


         var finder = PatternFinderEngineBuilder.Build("Hola", dirConfig, filterConfig);
         var results = await finder.FindMatchesAsync();

         foreach (var match in results)
         {
            var info = match.FileInfo;
            var replacer = PatternReplacerBuilder.Build(match, "HOLA");
            replacer.ReplaceAsync();
         }
      }
   }
}