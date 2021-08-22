using PatternFinder.Entities;
using PatternFinder.Interfaces;

namespace PatternFinder
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