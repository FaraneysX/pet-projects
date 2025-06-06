namespace Lab_1;

public partial class Form : System.Windows.Forms.Form
{
    private VisualController _threadController;

    public Form()
    {
        InitializeComponent();
        _threadController = new VisualController(LogsTextBox, BufferTextBox);
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        StartButton.Enabled = false;

        _threadController.Start();
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        _threadController.Stop();
        Close();
    }
}
