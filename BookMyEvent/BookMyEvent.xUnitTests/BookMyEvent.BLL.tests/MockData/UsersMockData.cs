using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMyEvent.BLL.Models;
using db.Models;
namespace BookMyEvent.xUnitTests.BookMyEvent.BLL.tests.MockData
{
    public class UsersMockData
    {
        public static List<BLUser> GetUsers()
        {
            return new List<BLUser>()
            {
                new BLUser()
                {
                    UserId = new Guid("b2c2b4a0-0b7a-4b1a-9b1a-0b1a0b1a0b1a"),
                    Name = "Tester",
                    Email = "tester@abc.com"
                },
                new BLUser()
                {
                    UserId = new Guid("b2c2b4a0-0b7a-4b1a-9b1a-0b1a0b1a0b1b"),
                    Name = "Tester1",
                    Email = "tester1@abc.com"
                },
                new BLUser()
                {
                    UserId = new Guid("b2c2b4a0-0b7a-4b1a-9b1a-0b1a0b1a0b1c"),
                    Name = "Tester2",
                    Email = "tester2@abc.com"
                },
                new BLUser()
                {
                    UserId = new Guid("b2c2b4a0-0b7a-4b1a-9b1a-0b1a0b1a0b1d"),
                    Name = "Tester3",
                    Email = "tester3@abc.com"
                }
            };
        }

        public static BLUser GetUser(Guid userId)
        {
            return new BLUser()
            {
                UserId = userId,
                Name = "Tester",
                Email = "tester@abc.com",
                AccountCredentialsId = Guid.NewGuid()
            };
        }

        public static BLUser CreateUser()
        {
            return new BLUser()
            {
                Name = "Tester",
                Email = "tester@abc.com"
            };
        }
    }
}
