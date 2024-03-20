using System.Text;

/**This custom text writer is designed to capture 
 * and store text output (both individual characters 
 * and strings followed by newline characters) written 
 * to it.**/
public class CustomTextWriter : TextWriter
{
    //List will be used to store the lines of text written to the custom text writer
    private List<string> _outputLines = new List<string>();
    //Specifies the character encoding used by custom text writer
    public override Encoding Encoding => Encoding.UTF8;

    //Appending a single character to the buffer
    public override void Write(char value)
    {
        _outputLines.Add(value.ToString());
    }

    //Appending a string followed by a newline character to the buffer
    public override void WriteLine(string value)
    {
        _outputLines.Add(value);
    }

    //Returning the captured output as a string array
    public string[] GetCapturedOutput()
    {
        return _outputLines.ToArray();
    }

    //Clearing the captured output
    public void ClearCapturedOutput()
    {
        _outputLines.Clear();
    }
}
