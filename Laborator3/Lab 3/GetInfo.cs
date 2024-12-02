using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_3
{
    public class GetInfo
    {
        public static void get_info(string filename, out string[] flines, List<string> fdata, ref int fwords, ref int fcharacters)
        {
            flines = File.ReadAllLines(filename);
            foreach (var line in flines)
            {
                fdata.Add(line);
                fwords += line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                fcharacters += line.Length;
            }
        }
        public static void get_info(string filename, out string[] flines, List<string> fdata, List<string> fclasses, ref int fmethods)
        {
            flines = File.ReadAllLines(filename);
            foreach (var line in flines)
            {
                fdata.Add(line);
                if (line.Contains("class") && line[line.Length - 1] != ';')
                {
                    var firstWord = line.Substring(0, line.IndexOf(" "));
                    if (firstWord == "class")
                    {
                        fclasses.Add(line.Split(' ').Skip(1).FirstOrDefault());
                    }
                }
                if (line.Contains("{"))
                {
                    fmethods++;
                }
            }
        }
    }
}
