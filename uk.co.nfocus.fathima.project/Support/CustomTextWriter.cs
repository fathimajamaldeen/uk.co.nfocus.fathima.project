using System.Text;

public class CustomTextWriter : TextWriter
{
    private StringBuilder _buffer = new StringBuilder();//Creating a StringBuilder to store the written output

    public override Encoding Encoding => Encoding.UTF8;

    //Appending a single character to the buffer
    public override void Write(char value)
    {
        _buffer.Append(value);
    }

    //Appending a string followed by a newline character to the buffer
    public override void WriteLine(string value)
    {
        _buffer.AppendLine(value);
    }

    //Returning the captured output as a string
    public string GetCapturedOutput()
    {
        return _buffer.ToString();
    }
}