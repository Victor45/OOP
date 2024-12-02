using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    public abstract class FileInfo
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public FileInfo(string fileName, string extension, DateTime creationDate, DateTime lastUpdatedDate)
        {
            FileName = fileName;
            Extension = extension;
            CreationDate = creationDate;
            LastUpdatedDate = lastUpdatedDate;
        }

        public abstract void ShowInfo();
    }

    public class TextFile : FileInfo
    {
        public int LineCount { get; set; }
        public int WordCount { get; set; }
        public int CharacterCount { get; set; }

        public TextFile(string fileName, string extension, DateTime creationDate, DateTime lastUpdatedDate, int lineCount, int wordCount, int characterCount)
            : base(fileName, extension, creationDate, lastUpdatedDate)
        {
            LineCount = lineCount;
            WordCount = wordCount;
            CharacterCount = characterCount;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Text File: {FileName}");
            Console.WriteLine($"Lines: {LineCount}, Words: {WordCount}, Characters: {CharacterCount}");
            Console.WriteLine($"Created: {CreationDate}");
            Console.WriteLine($"Last Updated: {LastUpdatedDate} \n");
        }
    }

    public class ProgramFile : FileInfo
    {
        public int LineCount { get; set; }
        public int ClassCount { get; set; }
        public int MethodCount { get; set; }

        public ProgramFile(string fileName, string extension, DateTime creationDate, DateTime lastUpdatedDate, int lineCount, int classCount, int methodCount)
            : base(fileName, extension, creationDate, lastUpdatedDate)
        {
            LineCount = lineCount;
            ClassCount = classCount;
            MethodCount = methodCount;
        }

        public override void ShowInfo() 
        {
            Console.WriteLine($"Program File: {FileName}");
            Console.WriteLine($"Lines: {LineCount}, Classes: {ClassCount}, Methods: {MethodCount}");
            Console.WriteLine($"Created: {CreationDate}");
            Console.WriteLine($"Last Updated: {LastUpdatedDate} \n");
        }
    }
}
