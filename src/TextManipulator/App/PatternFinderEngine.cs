using System.Collections.Generic;
using System.Threading.Tasks;
using TextManipulator.Domain.Entities;
using TextManipulator.App.Configuration;
using TextManipulator.Infraestructure;
using TextManipulator.App.Interfaces;
using TextManipulator.App.Matchers;

namespace TextManipulator.App
{
   public class PatternFinderEngine
   {
      private static readonly object listLocker = new();
      private readonly SortedList<int, FileMatchesInfo> _matchesList = new();
      private readonly PatternFinderConfiguration _configuration;

      public bool IsDirectory => _configuration.PathNode.IsDirectory;

      internal PatternFinderEngine(PatternFinderConfiguration config)
      {
         _configuration = config;
      }

      public async Task<IEnumerable<FileMatchesInfo>> FindMatchesAsync()
      {
         _matchesList.Clear();

         return await FindMatchesInDirectoryAsync();
      }

      private IEnumerable<FileMatchesInfo> FindMatchesInDirectory()
      {
         var path = _configuration.PathNode.Path;
         var provider = _configuration.FilesProvider;

         var files = provider.GetFiles(_configuration.FilterConfiguration.FilesFilterPattern, _configuration.FilterConfiguration.DirectoriesFilterPattern);

         Parallel.ForEach(files, () => new FileMatchesInfo(), (file, state, matchesInfo) =>
         {
            matchesInfo = GetMatchesInfoAsync(file).GetAwaiter().GetResult();
            return matchesInfo;
         },
         (finalMatchesInfo) =>
         {
            AddMatches(finalMatchesInfo);
         });

         return GetFoundMatches();
      }

      public IEnumerable<FileMatchesInfo> GetFoundMatches()
      {
         foreach (var kvp in _matchesList)
         {
            yield return kvp.Value;
         }
      }

      private async Task<FileMatchesInfo> GetMatchesInfoAsync(System.IO.FileInfo fileInfo)
      {
         return await Task.Run(() => new FileLineMatcher().Match(fileInfo, _configuration.LineMatcher));
      }

      private async Task<IEnumerable<FileMatchesInfo>> FindMatchesInDirectoryAsync()
      {
         return await Task.Run(() => FindMatchesInDirectory());
      }

      private void AddMatches(FileMatchesInfo value)
      {
         if (!value.Success)
            return;

         lock (listLocker)
            _matchesList.Add(value.GetHashCode(), value);
      }
   }
}