using System.Collections.Generic;
using System.Threading.Tasks;
using PatternFinder.Entities;
using PatternFinder.Configuration;
using System.Linq;

namespace PatternFinder
{
   public class PatternFinderEngine
   {
      private static readonly object listLocker = new();
      private readonly SortedList<int, FileMatchesInfo> _matchesList = new();
      private readonly PatternFinderConfiguration _configuration;
      
      public bool IsDirectory => _configuration.PathNode.IsDirectory;

      internal PatternFinderEngine(PatternFinderConfiguration config)
         => _configuration = config;

      public async Task<IEnumerable<FileMatchesInfo>> FindMatchesAsync()
      {
         _matchesList.Clear();

         if (IsDirectory)
         {
            return await Task.Run(() => FindMatchesInsideDirectory());
         }

         var fileMatches = await GetMatchesInfoAsync(new System.IO.FileInfo(_configuration.PathNode.Path));
         _matchesList.Add(fileMatches.GetHashCode(), fileMatches);
         return GetFoundMatches();
      }

      private IEnumerable<FileMatchesInfo> FindMatchesInsideDirectory()
      {
         var path = _configuration.PathNode.Path;

         var files = FilesProvider.GetFiles(path, _configuration.FilterConfiguration.FilesFilterPattern, _configuration.FilterConfiguration.DirectoriesFilterPattern);

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
               lock (listLocker)
                  _matchesList.Add(finalMatchesInfo.GetHashCode(), finalMatchesInfo);
            }
         });

         return GetFoundMatches();
      }

      private async Task<FileMatchesInfo> GetMatchesInfoAsync(System.IO.FileInfo fileInfo)
      {
         return await Task.Run(() =>
         {
            var matchesInfo = new FileMatchesInfo(fileInfo);
            object locker = new();

            Parallel.ForEach(System.IO.File.ReadAllLines(fileInfo.FullName), () => new List<LineMatchInfo>(), (line, status, index, list) =>
            {
               var matches = _configuration.Matcher.Match(line, (uint)index);
               list.Add(matches);
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

      public IEnumerable<FileMatchesInfo> GetFoundMatches()
      {
         foreach (var kvp in _matchesList)
         {
            yield return kvp.Value;
         }
      }
   }
}
