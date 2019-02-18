namespace Domain
{
    public class ProductInCategory
    {
        public int ProductInCategoryId { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}