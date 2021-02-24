using System.Collections.Generic;

namespace PLApp.Pages.Recommends.ShoppingRecommends
{
    public class ShoppingRecommendsViewModel
    {
        public List<BE.Item> RecommendedItems { get => shoppingRecommendsModel.RecommendedItems; set => shoppingRecommendsModel.RecommendedItems = value; }
        public List<Rule> Rules { get => shoppingRecommendsModel.Rules; set => shoppingRecommendsModel.Rules = value; }

        private ShoppingRecommendsModel shoppingRecommendsModel;
        public ShoppingRecommendsViewModel()
        {
            shoppingRecommendsModel = new ShoppingRecommendsModel();
        }
    }
    
}
