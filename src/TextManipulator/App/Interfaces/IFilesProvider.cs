using System.Collections.Generic;

namespace TextManipulator.App.Interfaces
{
   public interface IFilesProvider
   {
      public IEnumerable<System.IO.FileInfo> GetFiles(string[] filePatterns, string[] dirPatterns);
   }
}
