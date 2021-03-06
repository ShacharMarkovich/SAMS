﻿using BE;

namespace PLApp.Pages.Analysis.CategoryPieChart
{
    internal class CategoryPieChartModel
    {
        public Category category;
        public double totalPriceInCategory;
        public int productsCount;
        public double avgPriceInCategory;

        public BL.DataHandle db { get; private set; }

        public CategoryPieChartModel() => db = new BL.DataHandle();
    }
}
