using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App.Replacers
{
   public class SimpleMatchReplacer : IMatchReplacer
   {
      private readonly string _newValue;
      public SimpleMatchReplacer(string newValue)
      {
         _newValue = newValue;
      }
      public string Replace(FileMatch match)
         => _newValue;
   }
}