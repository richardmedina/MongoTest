using MongoTest.Contract.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTest.Common.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserDomain> CreateAsync(UserDomain user);
        Task DeleteAsync(string id);
        Task<UserDomain> GetAsync(string id);
        Task<IEnumerable<UserDomain>> GetAllAsync();
        Task<IEnumerable<UserDomain>> GetByFullNameAsync(string fullName);
        Task UpdateAsync(string id, UserDomain user);
        //Task<IEnumerable<UserDomain>> GetAll();

    }
}
