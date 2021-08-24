using System.Collections.Generic;
using System.Threading.Tasks;
using TextManipulator.Domain.Entities;
using TextManipulator.App.Configuration;
using TextManipulator.Infraestructure;
using TextManipulator.App.Interfaces;

namespace TextManipulator.App
{
   public class PatternFinderEngine
   {
      private static readonly object listLocker = new();
      private readonly SortedList<int, FileMatchesInfo> _matchesList = new();
      private readonly PatternFinderConfiguration _configuration;
      private readonly IFilesProvider _filesProvider;

      public bool IsDirectory => _configuration.PathNode.IsDirectory;

      internal PatternFinderEngine(PatternFinderConfiguration config, IFilesProvider filesProvider)
      {
         _configuration = config;
         _filesProvider = filesProvider;
      }

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

         var files = _filesProvider.GetFiles(path, _configuration.FilterConfiguration.FilesFilterPattern, _configuration.FilterConfiguration.DirectoriesFilterPattern);

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

            Parallel.ForEach(System.IO.File.ReadAllLines(fileInfo.FullName), (line, status, index) =>
            {
               var matches = _configuration.Matcher.Match(line, (uint)index);
               matchesInfo.AddMatch(matches);
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
