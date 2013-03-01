using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palipali
{
    public class Cache
    {
        // the search hint
        private string hint;
        // list of cached items that are able be found by the search
        List<SearchResult> cached_items;
        // collection of results that match the search hint
        ObservableCollection<SearchResult> results;

        public Cache()
        {
            cached_items = new List<SearchResult>();
            results = new ObservableCollection<SearchResult>();

            // load start menu items
            LoadStartMenu(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
//            LoadStartMenu(System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE") + @"\Start Menu");

            // A default hint. Not really necessary but can be handy during testing.
            hint = "Calc";
        }

        public string Hint
        {
            get
            {
                return hint;
            }
            set
            {
                hint = value;
                results.Clear();
                foreach (SearchResult sr in cached_items)
                {
                    // really simple case-insensitive substring search. anyone can make this better
                    // and it's not really the point of this tutorial, so we'll go ahead and use it. :)
                    if (sr.Name.ToLower().Contains(hint.ToLower()))
                        results.Add(sr);
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Hint"));
            }
        }

        public ReadOnlyObservableCollection<SearchResult> Results
        {
            get
            {
                return new ReadOnlyObservableCollection<SearchResult>(results);
            }
        }

        private void LoadStartMenu(string path)
        {
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();

            foreach (string file in System.IO.Directory.GetFiles(path))
            {
                System.IO.FileInfo fileinfo = new System.IO.FileInfo(file);

                if (fileinfo.Extension.ToLower() == ".lnk")
                {
                    IWshRuntimeLibrary.WshShortcut link = shell.CreateShortcut(file) as IWshRuntimeLibrary.WshShortcut;
                    SearchResult sr = new SearchResult(fileinfo.Name.Substring(0, fileinfo.Name.Length - 4), link.TargetPath, file);
                    cached_items.Add(sr);
                }
            }

            // recurse through the subfolders
            foreach (string dir in System.IO.Directory.GetDirectories(path))
            {
                LoadStartMenu(dir);
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler h = PropertyChanged;
            if (h != null)
                h(this, e);
        }
        #endregion
    }
}
