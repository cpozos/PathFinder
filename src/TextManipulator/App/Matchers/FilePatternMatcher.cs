using System.Collections.Generic;
using System.IO;
using System.Text;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App.Matchers
{
   public class FilePatternMatcher : IFilePatternMatcher
   {
      private readonly List<IPatternMatcher> _patternMatchers = new();
      private readonly Dictionary<int, FilePosition> _charIdToFilePosition = new();

      public FilePatternMatcher(IEnumerable<IPatternMatcher> pPatternMatchers)
      {
         _patternMatchers.AddRange(pPatternMatchers);
      }

      public FilePatternMatcher(IPatternMatcher pPatternMatcher)
      {
         _patternMatchers.Add(pPatternMatcher);
      }

      public FileMatches Match(FileInfo fileInfo)
      {
         _charIdToFilePosition.Clear();

         StringBuilder sb = new();
         int lineIndex = 0;
         int charId = 0;
         foreach (var line in File.ReadLines(fileInfo.FullName))
         {
            int colIndex = 0;
            for (; colIndex < line.Length; colIndex++)
            {
               FilePosition position = new(lineIndex, colIndex);
               _charIdToFilePosition.Add(charId, position);
               charId++;
            }

            foreach (var @char in System.Environment.NewLine)
            {
               FilePosition position = new(lineIndex, ++colIndex);
               _charIdToFilePosition.Add(charId, position);
               charId++;
            }

            sb.AppendLine(line);
            lineIndex++;
         }

         var matchesInfo = new FileMatches(fileInfo);
         string input = sb.ToString();
         foreach (var matcher in _patternMatchers)
         {
            var collection = matcher.GetMatches(input);

            for (int i = 0; i < collection.Count; i++)
            {
               var match = collection[i];

               if (!match.Success)
                  continue;

               var filePosition = _charIdToFilePosition[match.Index];
               var endPosition = _charIdToFilePosition[match.Index + match.Length];

               matchesInfo.Add(new FileMatch
               {
                  StartPosition = filePosition,
                  EndPosition = endPosition,
                  Value = match.Value
               });
            }
         }

         return matchesInfo;
      }
   }
}