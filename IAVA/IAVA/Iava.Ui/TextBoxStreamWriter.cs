using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Controls;

namespace Iava.Ui
{
    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _text = null;

        public TextBoxStreamWriter(TextBox outputBox)
        {
            _text = outputBox;
        }

        public override void Write(string value)
        {
            base.Write(value);
            _text.Dispatcher.Invoke(new Action(() => _text.AppendText(value)));
            _text.Dispatcher.Invoke(new Action(() => _text.ScrollToEnd()));
        }

        public override void WriteLine(string value) {
            base.WriteLine(value);
            _text.Dispatcher.Invoke(new Action(() => _text.AppendText(value + '\n')));
            _text.Dispatcher.Invoke(new Action(() => _text.ScrollToEnd()));
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
