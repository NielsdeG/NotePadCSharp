namespace TextEditor2;

public class Editorstate
{
    public List<String> Lines
    {
        get;
        set;
    }
    public int LineRows
    {
        get;
        set;
    }
    public int LineColumns
    {
        get;
        set;
    }

    public Editorstate(List<string> lines, int lineRows, int lineColumns)
    {
        this.Lines = new List<String>(lines); 
        this.LineRows = lineRows;
        this.LineColumns = lineColumns;
    }
}