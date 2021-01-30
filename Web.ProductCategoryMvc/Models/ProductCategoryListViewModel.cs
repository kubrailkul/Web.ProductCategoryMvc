using System.Collections.Generic;

namespace Web.ProductCategoryMvc.Models
{
    public class ProductCategoryListViewModel
    {
        public List<tblProduct> Products { get; set; }
        public List<tblCategory> Categories { get; set; }

    }
}