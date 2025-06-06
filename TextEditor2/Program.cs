namespace TextEditor2;
public class Program
{
    private static Stack<Editorstate> UndoStack = new Stack<Editorstate>();
    private static Stack<Editorstate> RedoStack = new Stack<Editorstate>();

    
    private static List<String> lines = new List<String> {"123"};
        
    private static int lineColumns = 0;
    private static int lineRows = 0;

    public static void Main(string[] args)
    {
        Console.CursorVisible = true;
        
        
        

        
        while (true) {
            Console.Clear();
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(lines[i]);
            }
            
            Console.SetCursorPosition(lineColumns, lineRows);
            
             ConsoleKeyInfo key = Console.ReadKey(true);

             if (key.Modifiers == ConsoleModifiers.Control)
             {
                 if (key.Key == ConsoleKey.Z)
                 {
                     Undo();
                 }

                 if (key.Key == ConsoleKey.Y)
                 {
                     Redo();
                 }
             }
             
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
                 
                 case ConsoleKey.Enter:
                     UndoStack.Push(new Editorstate(lines, lineRows, lineColumns));
                     RedoStack.Clear();
                     String afterCusor = "";
                     if (lineColumns < lines[lineRows].Length)
                     {
                         afterCusor = lines[lineRows].Substring(lineColumns);
                         lines[lineRows] = lines[lineRows].Substring(0, lineColumns);
                     }
                     lines.Insert(lineRows + 1, afterCusor);
                     lineColumns = 0;
                     lineRows++;
                     break;
                 
                 case ConsoleKey.UpArrow:
                     if (lineRows > 0)
                     {
                         lineRows--;
                         lineColumns = Math.Min(lineColumns, lines[lineRows].Length);
                     }
                     break;
                 
                 case ConsoleKey.DownArrow:
                     if (lineRows < lines.Count - 1)
                     {
                         lineRows++;
                         lineColumns = Math.Min(lineColumns, lines[lineRows].Length);
                     }
                     break;
                 
                 case ConsoleKey.Backspace:
                     if (lineColumns > 0)
                     {
                         UndoStack.Push(new Editorstate(lines, lineRows, lineColumns));
                         RedoStack.Clear();
                         lines[lineRows] = lines[lineRows].Remove(lineColumns -1, 1);
                         lineColumns--;
                     } else if (lineRows > 0)
                     {
                         UndoStack.Push(new Editorstate(lines, lineRows, lineColumns));
                         RedoStack.Clear();
                         String line = lines[lineRows]; 
                         lines.RemoveAt(lineRows);
                         lineRows--;
                         lineColumns = lines[lineRows].Length;
                         lines[lineRows] += line;
                     }
                     break;
                 
                 case ConsoleKey.Delete:
                     if (lineColumns < lines[lineRows].Length)
                     {
                         UndoStack.Push(new Editorstate(lines, lineRows, lineColumns));
                         RedoStack.Clear();
                         lines[lineRows] = lines[lineRows].Remove(lineColumns, 1);
                     }else if (lineRows < lines.Count - 1)
                     {
                         UndoStack.Push(new Editorstate(lines, lineRows, lineColumns));
                         RedoStack.Clear();
                         lines[lineRows] += lines[lineRows + 1];
                         lines.RemoveAt(lineRows + 1);
                     }

                     break;

                 default:
                     if (key.KeyChar >= ' ' && key.KeyChar <= '~' || char.IsLetterOrDigit(key.KeyChar))
                     {
                         UndoStack.Push(new Editorstate(lines, lineRows, lineColumns));
                         lines[lineRows] = lines[lineRows].Insert(lineColumns, key.KeyChar.ToString());
                         lineColumns++;
                         RedoStack.Clear();
                     }
                     break;
             }
        }
    }

    private static void Undo()
    {
        if (UndoStack.Count > 0)
        {
            Editorstate undoState = UndoStack.Peek();
            UndoStack.Pop();
            RedoStack.Push(new Editorstate(lines, lineRows, lineColumns));
            lines = new List<string>(undoState.Lines);
            lineRows = undoState.LineRows;
            lineColumns = undoState.LineColumns;
            
        }
    }
    
    private static void Redo()
    {
        if (RedoStack.Count > 0)
        {
            Editorstate redoState = RedoStack.Peek();
            RedoStack.Pop();
            UndoStack.Push(new Editorstate(lines, lineRows, lineColumns));
            lines = new List<string>(redoState.Lines);
            lineRows = redoState.LineRows;
            lineColumns = redoState.LineColumns;
        }
    }
    
}