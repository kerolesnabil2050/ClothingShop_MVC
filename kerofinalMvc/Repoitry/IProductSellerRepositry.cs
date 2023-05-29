using Project.Models;

namespace Project.Repoitry
{
    public interface IProductSellerRepositry:Irepositry<ProdectSeller>
    {
        ProdectSeller GetspecificItem(int id,string include);
       prodect? GetProductByName(string name, string idseller);
        List<ProdectSeller> GetspecificItems(int id,string include=null);
        List<ProdectSeller> GetAllwithinclude(Func<prodect, bool> predicate, string include);
        List<ProdectSeller> GetAllProductsForSeller(string SellerId);
        //product seller id
        ProdectSeller EditProduct(int Id);

    }
}

