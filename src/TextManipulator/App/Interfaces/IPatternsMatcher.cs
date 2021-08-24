namespace TextManipulator.App.Interfaces
{
   public interface IPatternsMatcher
   {
      public bool MatchAnyPattern(string value, string[] patterns);
   }
}