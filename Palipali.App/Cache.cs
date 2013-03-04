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
        
        readonly List<SearchResult> _cachedItems;

        // collection of results that match the search hint
        ObservableCollection<SearchResult> results;

        private readonly MenuRepository _menuRepository;

        public Cache()
        {
            results = new ObservableCollection<SearchResult>();

            _menuRepository = new MenuRepository();
            _cachedItems = _menuRepository.List(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu)).ToList();
            
//            LoadStartMenu(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
//            LoadStartMenu(System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE") + @"\Start Menu");

            // A default hint. Not really necessary but can be handy during testing.
            hint = "Calc";
        }

        public string Hint
        {
            get { return hint; }
            set
            {
                hint = value;
                results.Clear();
                foreach (SearchResult sr in _cachedItems)
                {
                    // really simple case-insensitive substring search. anyone can make this better
                    // and it's not really the point of this tutorial, so we'll go ahead and use it. :)
//                    if (sr.Name.ToLower().Contains(hint.ToLower()))
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
