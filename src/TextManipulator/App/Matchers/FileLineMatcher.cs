using System.IO;
using System.Threading.Tasks;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App.Matchers
{
   public class FileLineMatcher : IFileLineMatcher
   {
      public FileMatchesInfo Match(FileInfo fileInfo, ILinePatternMatcher lineMatcher)
      {
         var matchesInfo = new FileMatchesInfo(fileInfo);

         Parallel.ForEach(File.ReadLines(fileInfo.FullName), (line, status, index) =>
         {
            var matches = lineMatcher.Match(line, (uint)index);
            matchesInfo.AddMatch(matches);
         });

         return matchesInfo;
      }
   }
}