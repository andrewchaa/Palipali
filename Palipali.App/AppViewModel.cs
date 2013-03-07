using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;

namespace Palipali
{
    public class AppViewModel : PropertyChangedBase
    {
        private IEnumerable<SearchResult> _allProgramFiles;

        public AppViewModel()
        {
            ReadProgramsInStartMenu();
        }

        private IEnumerable<SearchResult> _searchResults;
        public IEnumerable<SearchResult> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                NotifyOfPropertyChange(() => SearchResults);
            }
        }

        public string ActiveItemTitle
        {
            get { return "Test";  }
        }

        public void SearchBoxKeyUp(KeyEventArgs args)
        {
            
        }

        public void SearchTextChanged(string text)
        {;
            SearchResults = _allProgramFiles.Where(s => s.Name.ToLower().Contains(text.ToLower()));
        }

        private void ReadProgramsInStartMenu()
        {
            var menuRepository = new MenuRepository();
            _allProgramFiles = menuRepository.List(System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE") + @"\Start Menu\Programs").ToList();
        }
    }
}