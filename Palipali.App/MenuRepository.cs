using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palipali
{
    public class MenuRepository
    {
        private IList<SearchResult> _searchResults;

        public IEnumerable<SearchResult> List(string path)
        {
            _searchResults = new List<SearchResult>();
            LoadStartMenu(path);

            return _searchResults;
        } 

        private void LoadStartMenu(string path)
        {
            var shell = new IWshRuntimeLibrary.WshShell();

            foreach (string file in System.IO.Directory.GetFiles(path))
            {
                var fileinfo = new System.IO.FileInfo(file);

                if (fileinfo.Extension.ToLower() == ".lnk")
                {
                    IWshRuntimeLibrary.WshShortcut link = shell.CreateShortcut(file) as IWshRuntimeLibrary.WshShortcut;
                    SearchResult sr = new SearchResult(fileinfo.Name.Substring(0, fileinfo.Name.Length - 4), link.TargetPath, file);
                    _searchResults.Add(sr);
                }
            }

            // recurse through the subfolders
            foreach (string dir in System.IO.Directory.GetDirectories(path))
            {
                LoadStartMenu(dir);
            }

        }

    }
}
