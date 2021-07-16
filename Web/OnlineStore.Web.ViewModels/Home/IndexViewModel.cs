﻿namespace OnlineStore.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalProducts { get; set; }

        public int TotalUsers { get; set; }

        public List<ProductIndexViewModel> Products { get; set; }
    }
}
