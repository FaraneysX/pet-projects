namespace Lab_1;

public class VisualController
{
    private readonly Controller _thread = new();

    private readonly TextBox _logsTextBox;
    private readonly TextBox _bufferTextBox;

    public VisualController(TextBox logsTextBox, TextBox bufferTextBox)
    {
        _logsTextBox = logsTextBox;
        _bufferTextBox = bufferTextBox;

        _thread.WriterWaiting += number =>
        {
            AppendText($"Писатель взял данные ({number}).");
        };

        _thread.WriterDone += data =>
        {
            AppendText($"Писатель поместил данные ({data}).");
            PrintBuffer();
        };

        _thread.ReaderDone += (reader, data) =>
        {
            AppendText($"Читатель {reader.Id} взял данные ({data}). Осталось {reader.DataCount} данных.");
            PrintBuffer();
        };

        _thread.ReaderDeleted += number =>
        {
            AppendText($"Читатель {number} закончил считывать данные.");
        };
    }

    public void Start()
    {
        _thread.Start();
    }

    public void Stop()
    {
        _thread.Stop();
    }

    private void AppendText(string text)
    {
        if (!_logsTextBox.IsDisposed && !_logsTextBox.Disposing)
        {
            void appendAction()
            {
                _logsTextBox.AppendText($"{text}{Environment.NewLine}");
            }

            if (_logsTextBox.InvokeRequired)
            {
                _logsTextBox.BeginInvoke(appendAction);
            }
            else
            {
                appendAction();
            }
        }
    }

    private void PrintBuffer()
    {
        if (!_bufferTextBox.IsDisposed && !_bufferTextBox.Disposing)
        {
            void appendAction()
            {
                _bufferTextBox.Text = string.Join(Environment.NewLine, _thread.Buffer.ToArray());
            }

            if (_bufferTextBox.InvokeRequired)
            {
                _bufferTextBox.BeginInvoke(appendAction);
            }
            else
            {
                appendAction();
            }
        }
    }
}
