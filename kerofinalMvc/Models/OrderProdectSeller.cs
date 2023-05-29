namespace Project.Models
{
    public class OrderProdectSeller
    {
        public int Id { get; set; }
        public int Quntity { get; set; }    
        public int OrderId { get; set; }        
        public int ProdectSellerID { get; set; }    
        public virtual Order? Order { get; set; }    
        public virtual ProdectSeller? ProdectSeller { get; set;}
    }
}
