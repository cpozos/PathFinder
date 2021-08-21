using TextManipulator.Entities;
using TextManipulator.Interfaces;

namespace TextManipulator
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