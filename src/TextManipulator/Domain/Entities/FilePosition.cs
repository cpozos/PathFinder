using System;

namespace TextManipulator.Domain.Entities
{
   public class FilePosition
   {
      public int LineIndex { get; set; }
      public int ColumnIndex { get; set; }

      public FilePosition(int pLineIndex = 0, int pColumnIndex = 0)
      {
         LineIndex = pLineIndex;
         ColumnIndex = pColumnIndex;
         ValidateData();
      }

      private void ValidateData()
      {
         if (LineIndex < 0)
            throw new ArgumentException($"{nameof(LineIndex)} is not valid");
         else if (ColumnIndex < 0)
            throw new ArgumentException($"{nameof(ColumnIndex)} is not valid");
      }
   }
}