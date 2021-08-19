using PatternFinder.Entities;

namespace PatternFinder.Interfaces
{
   public interface IMatchReplacer
   {
      string Replace(MatchInfo match);
   }
}
