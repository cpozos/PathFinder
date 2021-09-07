namespace TextManipulator.Domain.Entities
{
   public class Match
   {
      public FilePosition StartPosition { get; set; } = new FilePosition();
      public FilePosition EndPosition { get; set; } = new FilePosition();
      public string Value { get; set; }
   }
}