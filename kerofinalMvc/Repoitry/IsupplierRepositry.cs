using Project.Models;

namespace Project.Repoitry
{
    public interface IsupplierRepositry:Irepositry<Seller>
    {
		Seller GetsellerById(string id);
        Seller GETSpecificsupplier(string id);
        List<Seller> GetAllWithInclude(string include);
         void  Updatesup(string id,Seller s);
    }
}
