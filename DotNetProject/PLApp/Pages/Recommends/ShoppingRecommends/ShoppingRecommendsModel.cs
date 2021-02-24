using BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PLApp.Pages.Recommends.ShoppingRecommends
{
    public class ShoppingRecommendsModel
    {
        public List<Item> RecommendedItems { get; set; }
        public List<Rule> Rules { get; set; }
        public BL.DataHandle db { get; private set; }
        public ShoppingRecommendsModel()
        {
            db = new BL.DataHandle();
            Rules = db.GetAssociationRules().Select(r=> new Rule() { X = SortedListToString(r.X), Y = SortedListToString(r.Y), Confidence = r.Confidence }).ToList();
            RecommendedItems = db.RecomendedItems();
        }
        private string SortedListToString(SortedSet<Item> s)
        {
            string output = "";
            foreach (Item item in s)
            {
                output += item.ItemName + " ";
            }
            return output;
        }

    }
    public class Rule
    {
        public String X { get; set; }
        public String Y { get; set; }
        public Double Confidence { get; set; }
    }
}
