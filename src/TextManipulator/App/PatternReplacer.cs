using TextManipulator.App.Configurations;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;
using System.IO;
using System.Linq;
using System.Text;

namespace TextManipulator.App
{
   public class PatternReplacer
   {
      private readonly PatternReplacerConfiguration _config;

      public PatternReplacer(PatternReplacerConfiguration config)
         => _config = config;

      public void ReplaceAsync()
      {
         StringBuilder sb = new();
         
         using (var allLinesEnumerator = File.ReadLines(_config.FilePath).GetEnumerator())
         {
            var allMatches = _config.Matches;
            uint lineIndex = 0;
            while (allLinesEnumerator.MoveNext())
            {
               var match = allMatches.FirstOrDefault(l => l.StartPosition.LineIndex == lineIndex);
               if (match is not null)
               {
                  StringBuilder sbNewText = new(allLinesEnumerator.Current);

                  while (++lineIndex <= match.EndPosition.LineIndex)
                  {
                     lineIndex++;
                     allLinesEnumerator.MoveNext();
                     sbNewText.AppendLine(allLinesEnumerator.Current);
                  }

                  // Replace pattern inside sb
                  Replace(ref sbNewText, match, _config.MatchReplacer);

                  // Append data
                  sb.AppendLine(sbNewText.ToString());
               }
               else
               {
                  sb.AppendLine(allLinesEnumerator.Current);
                  lineIndex++;
               }
            }
         }

         // Removes the last NewLine
         foreach (var @char in System.Environment.NewLine)
         {
            sb.Length--;
         }

         File.WriteAllText(_config.FilePath, sb.ToString());
      }

      private static void Replace(ref StringBuilder sb, FileMatch match, IMatchReplacer matchReplacer)
      {
         sb.Remove(match.StartPosition.ColumnIndex, match.Value.Length);
         sb.Insert(match.StartPosition.ColumnIndex, matchReplacer.Replace(match));
      }
   }
}