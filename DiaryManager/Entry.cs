using System;

namespace DiaryManager
{
    public class Entry
    {
        public string Date { get; private set; }
        public string Content { get; private set; }

        public Entry(string date, string content)
        {
            Date = date;
            Content = content;
        }

        public override string ToString()
        {
            return $"{Date}: {Content}";
        }
    }
}
