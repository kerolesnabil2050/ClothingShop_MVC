namespace Project.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string CustomerId { get; set; }
        public int ProdectSellerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProdectSeller ProdectSeller { get; set; }
    }
}
