using System.Threading.Tasks;
using PatternFinder;

namespace Test
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var dirConfig = new PathConfiguration(@"D:\TextFinderTestDir");
         var filterConfig = new FilterConfiguration("!*.txt;*.py", "!dir*");


         var finder = PatternFinderEngineBuilder.Build("Hola", dirConfig, filterConfig);
         var results = await finder.FindMatchesAsync();

         foreach (var match in results)
         {
            var info = match.FileInfo;
         }

      }
   }
}
