using System;
using System.IO;

namespace simple_event
{
    public class FileFoundArgs : EventArgs
    {
        public string FoundFile { get; }
        public bool CancelRequested { get; set; }
        public FileFoundArgs(string fileName) => FoundFile = fileName;
    }

    public class FileSearcher
    {
        internal class SearchDirectoryArgs : EventArgs
        {
            internal string CurrentSearchDirectory { get; }
            internal int TotalDirs { get; }
            internal int CompletedDirs { get; }

            public SearchDirectoryArgs(string currentSearchDirectory, int totalDirs, int completeDirs)
            {
                CurrentSearchDirectory = currentSearchDirectory;
                TotalDirs = totalDirs;
                CompletedDirs = completeDirs;
            }
        }

        public event EventHandler<FileFoundArgs> FileFound;

        internal event EventHandler<SearchDirectoryArgs> DirectoryChanged
        {
            add { _directoryChanged += value; }
            remove { _directoryChanged -= value; }
        }

        private EventHandler<SearchDirectoryArgs> _directoryChanged;

        public void Search(string directory, string searchPattern)
        {
            foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
            {
                RaiseFileFound(file);
            }
        }

        public void Search(string directory, string searchPattern, bool searchSubDirs = false)
        {
            if (searchSubDirs)
            {
                var allDirectories = Directory.GetDirectories(directory, "*.*", SearchOption.AllDirectories);
                var completedDirs = 0;
                var totalDirs = allDirectories.Length + 1;
                foreach (var dir in allDirectories)
                {
                    RaiseSearchDirectoryChanged(dir, totalDirs, completedDirs++);
                    // Search 'dir' and its subdirectories for files that match the search pattern:
                    SearchDirectory(dir, searchPattern);
                }

                // Include the Current Directory:
                RaiseSearchDirectoryChanged(directory, totalDirs, completedDirs++);

                SearchDirectory(directory, searchPattern);
            }
            else
            {
                SearchDirectory(directory, searchPattern);
            }
        }

        private void SearchDirectory(string directory, string searchPattern)
        {
            foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
            {
                FileFoundArgs args = RaiseFileFound(file);
                if (args.CancelRequested)
                {
                    break;
                }
            }
        }

        private void RaiseSearchDirectoryChanged(
            string directory, int totalDirs, int completedDirs) =>
            _directoryChanged?.Invoke(
                this,
                new SearchDirectoryArgs(directory, totalDirs, completedDirs));

        private FileFoundArgs RaiseFileFound(string file)
        {
            var args = new FileFoundArgs(file);
            FileFound?.Invoke(this, args);
            return args;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var fileList = new FileSearcher();
            int filesFound = 0;
            EventHandler<FileFoundArgs> onFileFound = (sender, foundArgs) =>
            {
                Console.WriteLine(foundArgs.FoundFile);
                filesFound++;
            };

            fileList.DirectoryChanged += (sender, eventArgs) =>
            {
                Console.Write($"Entering '{eventArgs.CurrentSearchDirectory}'.");
                Console.WriteLine($" {eventArgs.CompletedDirs} of {eventArgs.TotalDirs} completed...");
            };
            fileList.FileFound += onFileFound;


            fileList.Search(@"C:\Users\patrick\RiderProjects\delegation_event\simple_event", "hell.txt", true);
            Console.WriteLine("{0} file found", filesFound);
        }
    }
}