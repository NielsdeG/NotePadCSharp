namespace TextEditor2;
public class Program
{
    public static void Main(string[] args)
    {
        Console.CursorVisible = true;
        
        List<String> lines = new List<String> {"123"};

        int lineColumns = 0;
        int lineRows = 0;
        
        while (true) {
            Console.Clear();
            for (int i = 0; i < lines.Count; i++)
            {
            Console.WriteLine(lines[i]);
            }
            
            Console.SetCursorPosition(lineColumns, lineRows);
            
             ConsoleKeyInfo key = Console.ReadKey(true);
             switch (key.Key)
             {
                 case ConsoleKey.LeftArrow:
                     if (lineColumns > 0)
                        lineColumns--;
                     break;
                 case ConsoleKey.RightArrow:
                     if (lines[lineRows].Length > lineColumns)
                         lineColumns++;
                     break;
                 default:
                     if (key.KeyChar >= ' ' && key.KeyChar <= '~' || char.IsLetterOrDigit(key.KeyChar))
                     {
                         lines[lineRows] = lines[lineRows].Insert(lineColumns, key.KeyChar.ToString());
                         lineColumns++;
                     }
                     break;
             }
        }

    }
}