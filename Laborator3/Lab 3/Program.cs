
using Lab_3;
using System.Xml.Linq;
using System.Threading;

bool isRunning = true;
int choice;
string root = @"C:\Users\user\Desktop\Lab 3 POO\Test";
string commitdir = @"C:\Users\user\Desktop\Lab 3 POO\Commits";
bool calledmanually = false;

Task ChangeDetector = RunChangeDetector();

while (true)
{
    Console.WriteLine("1. Commit.");
    Console.WriteLine("2. Info.");
    Console.WriteLine("3. Status.");
    Console.WriteLine("4. Return to previous snapshots.");
    Console.WriteLine("5. Exit");
    Console.Write("Choose an option: ");
    choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 1:
            string message;
            Console.Write("Message: ");
            message = Console.ReadLine();
            string target = commitdir + "\\" + message;
            Console.WriteLine(target);
            SaveDirectory(root, target);
            break;
        case 2:
            info();
            break;
        case 3:
            calledmanually = true;
            Status();
            break;
        case 4:
            backToSnapshot();
            break;
        case 5:
            isRunning = false;
            await ChangeDetector;
            return 0;

    }
    calledmanually = false;
}

async Task RunChangeDetector()
{
    while (isRunning)
    {
        try
        {
            Status();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        await Task.Delay(10000);
    }
}
void info()
{
    string[] fileEntries = Directory.GetFiles(root);
    string extension, fname;
    foreach (string fileName in fileEntries)
    {
        fname = Path.GetFileName(fileName);
        extension = Path.GetExtension(fileName);
        DateTime creationTime = Directory.GetCreationTime(fileName);
        DateTime lastWriteTime = Directory.GetLastWriteTime(fileName);

        if (extension == ".txt")
        {
            string[] lines = null;
            List<string> fdata = new List<string>();
            int wordCount = 0;
            int charCount = 0;

            GetInfo.get_info(fileName, out lines, fdata, ref wordCount, ref charCount);
            var textFile = new TextFile(fname, extension, creationTime, lastWriteTime, fdata.Count, wordCount, charCount);
            textFile.ShowInfo();
        }
        else if (extension == ".cpp")
        {
            string[] lines = null;
            List<string> fdata = new List<string>();
            List<string> fclasses = new List<string>();
            int methodCount = 0;

            GetInfo.get_info(fileName, out lines, fdata, fclasses, ref methodCount);
            var programFile = new ProgramFile(fname, extension, creationTime, lastWriteTime, fdata.Count, fclasses.Count, methodCount);
            programFile.ShowInfo();
        }
    }
}

void SaveDirectory(string sourcepath, string targetpath)
{
    if(!Directory.Exists(targetpath))
    {
        Directory.CreateDirectory(targetpath);
        foreach(var newFile in Directory.GetFiles(sourcepath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newFile, newFile.Replace(sourcepath, targetpath), true);
        }
    }
    else
    {
        foreach(var file in Directory.GetFiles(targetpath, "*.*", SearchOption.AllDirectories))
        {
            File.Delete(file);
        }
        foreach (var newFile in Directory.GetFiles(sourcepath, "*.*", SearchOption.AllDirectories))
        {
            File.Copy(newFile, newFile.Replace(sourcepath, targetpath), true);
        }
    }
}

void Status()
{
    string[] currentFiles = Directory.GetFiles(root);
    string[] commits = Directory.GetDirectories(commitdir);
    string lastcommit = commits[commits.Length - 1];
    string[] lastcommitFiles = Directory.GetFiles(lastcommit);
    DateTime Snapshot = Directory.GetLastWriteTime(lastcommit);
    if (calledmanually)
    {
        Console.WriteLine($"Created Snapshot at: {Snapshot}");
        Console.WriteLine($"Last Commit: {Path.GetFileName(lastcommit)}");
    }
    CompareFiles(currentFiles, lastcommitFiles); 
}

void CompareFiles(string[] currentDir, string[] lastCommitDir)
{
    List<string> currentFiles = new List<string>();
    List<string> lastcommitFiles = new List<string>();

    foreach(var file in currentDir)
    {
        string fname = Path.GetFileName(file);
        currentFiles.Add(fname);
        string extension = Path.GetExtension(file);
        string[] linesf1 = File.ReadAllLines(file);
        foreach (var file2 in lastCommitDir)
        {
            string fname2 = Path.GetFileName(file2);
            string extension2 = Path.GetExtension(file2);   
            if (fname == fname2 && extension == extension2)
            {
                string[] linesf2 = File.ReadAllLines(file2);
                bool modified = false;
                if (extension == ".txt")
                {
                    List<string> f1data = new List<string>();
                    List<string> f2data = new List<string>();
                    int f1words = 0;
                    int f2words = 0;
                    int f1characters = 0;
                    int f2characters = 0;
                    GetInfo.get_info(file, out linesf1, f1data, ref f1words,ref f1characters);
                    GetInfo.get_info(file2, out linesf2, f2data, ref f2words, ref f2characters);
                    if (f1data.Count > f2data.Count)
                    {
                        Console.Write($"{fname}: ");
                        Console.WriteLine("Changed");
                        for (int i = 0; i < f2data.Count; i++)
                        {
                            if (modified == false && f1data[i] != f2data[i])
                            {
                                modified = true;
                                Console.WriteLine($"- The text starting with line {i + 1} was modified");
                            }
                        }
                        Console.WriteLine($"- There were added {f1data.Count - f2data.Count} lines of text");
                    }
                    else if (f1data.Count < f2data.Count)
                    {
                        Console.Write($"{fname}: ");
                        Console.WriteLine("Changed");
                        for (int i = 0; i < f1data.Count; i++)
                        {
                            if (modified == false && f1data[i] != f2data[i])
                            {
                                modified = true;
                                Console.WriteLine($"- The text starting with line {i + 1} was modified");
                            }
                        }
                        Console.WriteLine($"- There were removed {f2data.Count - f1data.Count} lines of text");
                    }
                    else
                    {
                        bool nameshowed = false;
                        if (calledmanually)
                        {
                            Console.Write($"{fname}: ");
                            nameshowed = true;
                        }
                        for (int i = 0; i < f1data.Count; i++)
                        {
                            if (modified == false && f1data[i] != f2data[i])
                            {
                                modified = true;
                                if(!nameshowed)
                                    Console.Write($"{fname}: ");
                                Console.WriteLine("Changed");
                                Console.WriteLine($"- The text starting with line {i + 1} was modified");
                            }
                        }
                        if (!modified && calledmanually)
                        {
                            Console.WriteLine($"No changes");
                        }
                    }
                    if (f1words > f2words)
                    {
                        Console.WriteLine($"- There are {f1words - f2words} more words.");
                    }
                    else if (f1words < f2words)
                    {
                        Console.WriteLine($"- There are {f2words - f1words} less words.");
                    }
                    if (f1characters > f2characters)
                    {
                        Console.WriteLine($"- There are {f1characters - f2characters} more characters");
                    }
                    else if(f1characters < f2characters)
                    {
                        Console.WriteLine($"- There are {f2characters - f1characters} less characters");
                    }

                }
                else if (extension == ".cpp")
                {
                    List<string> f1classes = new List<string>();
                    List<string> f2classes = new List<string>();
                    List<string> f1data = new List<string>();
                    List<string> f2data = new List<string>();
                    int f1methods = 0;
                    int f2methods = 0;
                    GetInfo.get_info(file, out linesf1, f1data, f1classes, ref f1methods);
                    GetInfo.get_info(file2, out linesf2, f2data, f2classes, ref f2methods);
                    if (f1data.Count > f2data.Count)
                    {
                        Console.Write($"{fname}: ");
                        Console.WriteLine("Changed");
                        for (int i = 0; i < f2data.Count; i++)
                        {
                            if(modified == false && f1data[i] != f2data[i])
                            {
                                modified = true;
                                Console.WriteLine($"- The code from line {i + 1} was modified");
                            }
                        }
                        Console.WriteLine($"- There were added {f1data.Count - f2data.Count} lines of code");
                    }
                    else if(f1data.Count < f2data.Count)
                    {
                        Console.Write($"{fname}: ");
                        Console.WriteLine("Changed");
                        for (int i = 0; i < f1data.Count; i++)
                        {
                            if (modified == false && f1data[i] != f2data[i])
                            {
                                modified = true;
                                Console.WriteLine($"- The code from line {i + 1} was modified");
                            }
                        }
                        Console.WriteLine($"- There were removed {f2data.Count - f1data.Count} lines of code");
                    }
                    else
                    {
                        bool nameshowed = false;
                        if (calledmanually)
                        {
                            Console.Write($"{fname}: ");
                            nameshowed = true;
                        }
                        for (int i = 0; i < f1data.Count; i++)
                        {
                            if (modified == false && f1data[i] != f2data[i])
                            {
                                modified = true;
                                if (!nameshowed)
                                    Console.Write($"{fname}: ");
                                Console.WriteLine("Changed");
                                Console.WriteLine($"- The code from line {i + 1} was modified");
                            }
                        }
                        if (!modified && calledmanually)
                        {
                            Console.WriteLine($"No changes");
                        }
                    }
                    var firstNotSecond = f1classes.Except(f2classes).ToList();
                    var secondNotFirst = f2classes.Except(f1classes).ToList();
                    if(firstNotSecond.Count!= 0)
                    {
                        foreach (var classname in firstNotSecond)
                        {
                            Console.WriteLine($"- Class {classname} was created.");
                        }
                    }
                    if(secondNotFirst.Count!=0)
                    {
                        foreach(var classname in secondNotFirst)
                        {
                            Console.WriteLine($"- Class {classname} was removed.");
                        }
                    }
                    if (f1methods > f2methods)
                    {
                        Console.WriteLine($"- There were added {f1methods - f2methods} methods.");
                    }
                    else if (f2methods > f1methods)
                    {
                        Console.WriteLine($"- There were removed {f2methods - f1methods} methods.");
                    }
                }
            }
        }
    }
    foreach (var file in lastCommitDir)
    {
        lastcommitFiles.Add(Path.GetFileName(file));
    }
    var fNotSecond = currentFiles.Except(lastcommitFiles).ToList();
    var sNotFirst = lastcommitFiles.Except(currentFiles).ToList();
    if (fNotSecond.Count != 0)
    {
        foreach (var file in fNotSecond)
        {
            Console.WriteLine($"{file} - New File");
        }
    }
    if (sNotFirst.Count != 0)
    {
        foreach (var file in sNotFirst)
        {
            Console.WriteLine($"{file} - Deleted");
        }
    }

}

void backToSnapshot()
{
    string[] snapshots = Directory.GetDirectories(commitdir);
    foreach (var snapshot in snapshots)
    {
        DateTime snaptime = Directory.GetLastWriteTime(snapshot);
        Console.WriteLine($"{Path.GetFileName(snapshot)} - {snaptime}");
    }
    Console.Write("Select the snapshot you want to return: ");
    string snapreturn = Console.ReadLine();
    bool returned = false;
    foreach (var snapshot in snapshots)
    {
        if (snapreturn == Path.GetFileName(snapshot))
        {
            SaveDirectory(snapshot, root);
            returned = true;
        }
    }
    if (!returned)
    {
        Console.WriteLine("This snapshot does not exist.");
    }
    else Console.WriteLine("Successfully returned to snapshot.");
}
