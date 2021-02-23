using AutoCompleteTextBox.Editors;
using BE;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace PLApp.Providers
{
    public class ItemSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable<BE.Item> ListOfStates { get; set; }

        public Item GetExactSuggestion(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            return ListOfStates.FirstOrDefault(item => item.BarcodeNumber.ToString().Contains(filter));
        }

        public IEnumerable<Item> GetSuggestions(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            System.Threading.Thread.Sleep(1000);
            return ListOfStates.Where(item => item.BarcodeNumber.ToString().Contains(filter)).ToList();
        }

        IEnumerable ISuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }
        public ItemSuggestionProvider()
        {
            ListOfStates = new List<Item>();
        }
        public ItemSuggestionProvider(List<Item> items)
        {
            ListOfStates = items;
        }
    }
}
