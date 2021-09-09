using System.IO;
using System.Threading.Tasks;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App.Matchers
{
   public class FileLineMatcher : IFilePatternMatcher
   {
      private readonly ILinePatternMatcher _lineMatcher;

      public FileLineMatcher(ILinePatternMatcher lineMatcher)
      {
         _lineMatcher = lineMatcher;
      }

      public FileMatches Match(FileInfo fileInfo)
      {
         var matchesInfo = new FileMatches(fileInfo);

         Parallel.ForEach(File.ReadLines(fileInfo.FullName), (line, status, index) =>
         {
            var matches = _lineMatcher.Match(line, (int)index);
            matchesInfo.Add(matches);
         });

         return matchesInfo;
      }
   }
}