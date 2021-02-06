using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeUtility.Model
{
    public class TextValue
    {
        public TextValue(string text)
        {
            Text = text;
        }
        public TextValue(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }
    }
}
