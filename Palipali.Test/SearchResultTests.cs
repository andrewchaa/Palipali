using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace Palipali.Test
{
    public class SearchResultTests
    {
        private static MenuRepository _menuRepository;
        private static IEnumerable<SearchResult> _searchResults;

        private Establish context = () => _menuRepository = new MenuRepository();

        private Because of = () => _searchResults = _menuRepository.List(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));

        private It should_have_the_list_of_menu_programs = () => _searchResults.ShouldNotBeEmpty();
    }
}
