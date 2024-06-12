using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteplat.Models
{
    public class TextDocument
    {
        public string Filename { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public TextDocument() { }
        public TextDocument(string filename, string contents) {
            Filename = filename;
            Text = contents;
        }   
    }
}
