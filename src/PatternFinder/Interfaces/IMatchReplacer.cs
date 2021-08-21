using TextManipulator.Entities;

namespace TextManipulator.Interfaces
{
   public interface IMatchReplacer
   {
      string Replace(MatchInfo match);
   }
}
