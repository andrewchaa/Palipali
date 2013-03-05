using System.Collections.Generic;
using Caliburn.Micro;

namespace Palipali
{
    public class AppViewModel : PropertyChangedBase
    {
        private BindableCollection<SearchResult> _searchResults; 
        public BindableCollection<SearchResult> SearchResults
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

        public AppViewModel()
        {
            SearchResults = new BindableCollection<SearchResult>() {new SearchResult()};
        }
    }
}