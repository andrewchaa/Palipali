using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;

namespace Palipali
{
    public class AppViewModel : PropertyChangedBase
    {
        private IEnumerable<SearchResult> _allProgramFiles;
        private string _searchText;

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

        private int _searchResultsSelectedIndex;
        public int SearchResultsSelectedIndex
        {
            get { return _searchResultsSelectedIndex; }
            set
            {
                if (value < 0 || value > SearchResults.Count() - 1) 
                    return;

                _searchResultsSelectedIndex = value;
                NotifyOfPropertyChange(() => SearchResultsSelectedIndex);
            }
        }

        public SearchResult SearchResultSelectedItem { get; set; }

        public void SearchTextChanged(string text, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.Down:
                    SearchResultsSelectedIndex++;
                    break;

                case Key.Up:
                    SearchResultsSelectedIndex--;
                    break;
                
                case Key.Enter:
                    System.Diagnostics.Process.Start(SearchResultSelectedItem.Shortcut);
                    break;

                default:
                    if (_searchText != text)
                    {
                        SearchResults = _allProgramFiles.Where(s => s.Name.ToLower().Contains(text.ToLower()));
                        _searchText = text;
                    }
                    break;
            }

        }

        private void ReadProgramsInStartMenu()
        {
            var menuRepository = new MenuRepository();
            _allProgramFiles = menuRepository.List(System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE") + @"\Start Menu\Programs").ToList();
        }
    }
}