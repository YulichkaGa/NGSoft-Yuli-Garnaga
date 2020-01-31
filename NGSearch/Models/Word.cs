using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGSearch.Models
{

    public class Word
    {
        public string Name { get; set; }
        public Object Tag { get; set; }
    }
    public class WordCount
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class BothTable
    {
        public List<WordCount> wordCount { get; set; }
        public List<Word> words { get; set; }
        public List<WordsForDropBox> wordsForDropBox { get; set; }



    }
    public class
        WordsForDropBox
    {
        public string Name { get; set; }
    }
}