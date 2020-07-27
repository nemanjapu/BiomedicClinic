using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace BiomedicClinic.Helpers
{
    public class KeywordStream : MemoryStream
    {
        private readonly Stream responseStream;

        public KeywordStream(Stream stream)
        {
            responseStream = stream;
        }

        public override void Write(byte[] buffer,
        int offset, int count)
        {
            string html = Encoding.UTF8.GetString(buffer);
            html = html.Replace("<p>About</p>",
                   "<span class='keyword'>USA</span>");
            buffer = Encoding.UTF8.GetBytes(html);
            responseStream.Write(buffer, offset, buffer.Length);

        }

    }
}