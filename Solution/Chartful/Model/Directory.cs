using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chartful.Model
{
    public class Directory
    {
        List<Directory> directories;
        public List<Directory> Directories { get { return directories; } }

        public List<File> files;
        public List<File> Files { get { return files; } }
        string path;
        string name;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value.Split('\\')[value.Split('\\').Length - 1];
            }
        }

        public Directory()
        {
            path = System.IO.Directory.GetCurrentDirectory();
            Name = path;
            directories = new List<Directory>();

            GetChildrenDirectory();
        }

        public Directory(string p)
        {
            path = p;
            Name = path;
            directories = new List<Directory>();

            GetChildrenDirectory();
        }

        void GetChildrenDirectory()
        {
            string[] childrenPath = System.IO.Directory.GetDirectories(path);

            for (int i = 0; i < childrenPath.Length; i++)
            {
                directories.Add(new Directory(childrenPath[i]));
            }
        }
    }
}
