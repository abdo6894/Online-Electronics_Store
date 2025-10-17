using eCommerceApp.Application.DTOs.Product;

namespace BL.DTOs.Categoty
{
    public class GetCategory : CategoryBase
    {
        public Guid Id { get; set; }

        public ICollection<GetProduct>? Products { get; set; }
    }
}
