using TextManipulator.App.Configuration;
using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;
using System.Collections.Generic;
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

         var lineMatches = _config.LineMatches;
         uint lineIndex = 0;
         foreach (var line in File.ReadLines(_config.FilePath))
         {
            var matches = lineMatches.FirstOrDefault(l => l.LineIndex == lineIndex)?.Matches;
            if (matches is not null)
            {
               sb.AppendLine(ReplaceMatches(line, matches, _config.MatchReplacer));
            }
            else
            {
               sb.AppendLine(line);
            }
            lineIndex++;
         }

         File.WriteAllText(_config.FilePath, sb.ToString());
      }

      public static string ReplaceMatches(string originalString, List<MatchInfo> matchesInfo, IMatchReplacer matchReplacer)
      {
         StringBuilder sb = new(originalString);

         int replaced = 0;
         while (replaced < matchesInfo.Count)
         {
            var matchInfo = matchesInfo[replaced];
            sb.Remove((int)matchInfo.Column, matchInfo.Value.Length);
            sb.Insert((int)matchInfo.Column, matchReplacer.Replace(matchInfo));
            replaced++;
         }

         return sb.ToString();
      }
   }
}