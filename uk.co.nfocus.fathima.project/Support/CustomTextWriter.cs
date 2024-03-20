using System.Text;

public class CustomTextWriter : TextWriter
{
    private List<string> _outputLines = new List<string>(); // Creating a list to store the written output lines

    public override Encoding Encoding => Encoding.UTF8;

    // Appending a single character to the buffer
    public override void Write(char value)
    {
        _outputLines.Add(value.ToString());
    }

    // Appending a string followed by a newline character to the buffer
    public override void WriteLine(string value)
    {
        _outputLines.Add(value);
    }

    // Returning the captured output as a string array
    public string[] GetCapturedOutput()
    {
        return _outputLines.ToArray();
    }
}
