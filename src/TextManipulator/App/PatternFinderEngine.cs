using System.Collections.Generic;
using System.Threading.Tasks;
using TextManipulator.Domain.Entities;
using TextManipulator.App.Configurations;
using TextManipulator.Infraestructure;
using TextManipulator.App.Interfaces;
using TextManipulator.App.Matchers;

namespace TextManipulator.App
{
   public class PatternFinderEngine
   {
      private static readonly object listLocker = new();
      private readonly SortedList<int, FileMatches> _matchesList = new();
      private readonly PatternFinderConfiguration _configuration;

      public bool IsDirectory => _configuration.PathNode.IsDirectory;

      internal PatternFinderEngine(PatternFinderConfiguration config)
      {
         _configuration = config;
      }

      public async Task<IEnumerable<FileMatches>> FindMatchesAsync()
      {
         _matchesList.Clear();

         return await FindMatchesInDirectoryAsync();
      }

      private IEnumerable<FileMatches> FindMatchesInDirectory()
      {
         var path = _configuration.PathNode.Path;
         var provider = _configuration.FilesProvider;

         var files = provider.GetFiles(_configuration.FilterConfiguration.FilesFilterPattern, _configuration.FilterConfiguration.DirectoriesFilterPattern);

         Parallel.ForEach(files, () => new FileMatches(), (file, state, matchesInfo) =>
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

      public IEnumerable<FileMatches> GetFoundMatches()
      {
         foreach (var kvp in _matchesList)
         {
            yield return kvp.Value;
         }
      }

      private async Task<FileMatches> GetMatchesInfoAsync(System.IO.FileInfo fileInfo)
      {
         return await Task.Run(() => _configuration.FilePatternMatcher.Match(fileInfo));
      }

      private async Task<IEnumerable<FileMatches>> FindMatchesInDirectoryAsync()
      {
         return await Task.Run(() => FindMatchesInDirectory());
      }

      private void AddMatches(FileMatches value)
      {
         if (!value.Success)
            return;

         lock (listLocker)
            _matchesList.Add(value.GetHashCode(), value);
      }
   }
}