namespace Project.Models
{
    public class Category
    {
        public int? Id { get; set; } 
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<prodect>? Prodects { get; set; }
    } 
}
