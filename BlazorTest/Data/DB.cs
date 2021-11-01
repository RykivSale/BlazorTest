using Microsoft.EntityFrameworkCore;

namespace BlazorTest.Data
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options) { }

    }
}
