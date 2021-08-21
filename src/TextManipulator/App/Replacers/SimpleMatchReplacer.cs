using TextManipulator.Domain.Entities;
using TextManipulator.Domain.Interfaces;

namespace TextManipulator.App
{
   public class SimpleMatchReplacer : IMatchReplacer
   {
      private readonly string _newValue;
      public SimpleMatchReplacer(string newValue)
      {
         _newValue = newValue;
      }
      public string Replace(MatchInfo match)
         => _newValue;
   }
}