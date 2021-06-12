using System;
using System.Threading.Tasks;
using PatternFinder;
using PatternFinder.Models;

namespace Test
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var dirConfig = new PathConfiguration(@"D:\TextFinderTestDir");
         var filter = new FilterConfiguration("!*.txt;*.py", "!dir*");

         var finder = PatternFinderEngineBuilder.Build(dirConfig, "Hola", filter);

         var res = await finder.FindMatchesAsync();

         foreach (var r in res)
         {
            var a = r.FileInfo;
         }

      }
   }
}
