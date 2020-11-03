namespace Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Repository.Models;

    public interface IMerchantRepository : IBaseRepository<Merchant> 
    {
        Task<bool> Exists(Guid id);
    }
}