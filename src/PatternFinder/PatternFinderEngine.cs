using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PatternFinder.Interfaces;
using PatternFinder.Models;

namespace PatternFinder
{
   public class PatternFinderEngine
   {
      private readonly PatternFinderConfiguration _configuration;
      private readonly ILinePatternMatcher _matcher;

      public bool IsDirectory => _configuration.PathConfiguration.IsDirectory;

      internal PatternFinderEngine(PatternFinderConfiguration config)
      {
         _configuration = config;
         _matcher = config.Matcher;
      }

      public async Task<List<FileMatchesInfo>> FindMatchesAsync()
      {
         if (IsDirectory)
         {
            var list = await Task.Run(() =>
            {
               return FindMatchesInsideDirectory();
            });

            return list;
         }


         var fileMatches = await GetMatchesInfoAsync(new System.IO.FileInfo(_configuration.PathConfiguration.Path));
         return new List<FileMatchesInfo>() { 
            fileMatches 
         };
      }


      public List<FileMatchesInfo> FindMatchesInsideDirectory()
      {
         var path = _configuration.PathConfiguration.Path;

         var files = MultiEnumerateFiles(path, _configuration.FilterConfiguration.FilesFilterPattern, _configuration.FilterConfiguration.DirectoriesFilterPattern);

         var matchesList = new List<FileMatchesInfo>();
         object locker = new object();

         Parallel.ForEach(files, () => new FileMatchesInfo(),
         (file, state, matchesInfo) =>
         {
            matchesInfo = GetMatchesInfoAsync(file).GetAwaiter().GetResult();
            return matchesInfo;
         },
         (finalMatchesInfo) =>
         {
            if (finalMatchesInfo.Success)
            {
               lock (locker)
                  matchesList.Add(finalMatchesInfo);
            }
         });

         return matchesList;
      }

      public async Task<FileMatchesInfo> GetMatchesInfoAsync(System.IO.FileInfo fileInfo)
      {
         return await Task.Run(() =>
         {
            var matchesInfo = new FileMatchesInfo(fileInfo);
            object locker = new object();

            Parallel.ForEach(System.IO.File.ReadAllLines(fileInfo.FullName), () => new List<LineMatchInfo>(), (line, status, index, list) =>
            {
               var matches = _matcher.Match(line, (uint)index);
               list.AddRange(matches);
               return list;
            },
            (finalResult) =>
            {
               lock (locker)
                  matchesInfo.AddMatches(finalResult);
            });

            return matchesInfo;
         });
      }

      private static IEnumerable<System.IO.FileInfo> MultiEnumerateFiles(string path, string[] filePatterns, string[] dirPatterns)
      {
         var dirInfo = new System.IO.DirectoryInfo(path);

         foreach (var pattern in filePatterns)
         {
            var files = dirInfo.EnumerateFiles(pattern, System.IO.SearchOption.AllDirectories);
            foreach (var file in files)
            {
               var dirName = file.DirectoryName as string;

               if (dirName is null || string.Equals(dirName.ToString(), dirInfo.FullName.ToString()))
               {
                  yield return file;
                  continue;
               }

               if (MatchAnyPattern(file.Directory.Name, dirPatterns))
                  yield return file;
            }
         }
      }

      private static bool MatchAnyPattern(string directoryName, string[] directoryFilterPatterns)
      {
         bool matched = false;
         foreach (var pattern in directoryFilterPatterns)
         {
            Regex regex;
            if (pattern.StartsWith('!'))
            {
               regex = new Regex(pattern.Substring(1, pattern.Length-1));
               matched = !regex.IsMatch(directoryName);
            }
            else
            {
               regex = new Regex(pattern);
               matched = regex.IsMatch(pattern);
            }

            if (matched)
               break;
         }

         return matched;
      }
   }
}
