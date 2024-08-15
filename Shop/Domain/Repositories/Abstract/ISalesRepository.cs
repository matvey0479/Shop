using Shop.Models;

namespace Shop.Domain.Repositories.Abstract
{
    public interface ISalesRepository
    {
        public List<Sale> GetSales();
    }
}
