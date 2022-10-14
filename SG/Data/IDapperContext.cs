using System.Data;

namespace SG.Data
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}