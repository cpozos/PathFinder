using System.Collections.Generic;
using TextManipulator.Domain.Entities;

namespace TextManipulator.Domain.Interfaces
{
   public interface ILinePatternMatcher
   {
      IEnumerable<FileMatch> Match(string line, int lineIndex);
   }
}