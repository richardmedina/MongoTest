using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTest.Contract.Domain
{
    public class UserDomain
    {
        public string? Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
