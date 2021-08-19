namespace PatternFinder.Configuration
{
   public class FilterConfiguration
   {
      public string[] FilesFilterPattern { get; init; }
      public string[] DirectoriesFilterPattern { get; init; }

      public FilterConfiguration(string filesFilterPattern = "*", string directoriesFilterPattern = "*")
      {
         FilesFilterPattern = TrimSplit(filesFilterPattern, ";");
         DirectoriesFilterPattern = TrimSplit(directoriesFilterPattern, ";");
      }

      private string[] TrimSplit(string pStr, string separator)
      {
         string[] items = pStr.Split(separator);
         for (int i = 0; i < items.Length; i++)
         {
            items[0] = items[0].Trim();
         }

         return items;
      }
   }
}