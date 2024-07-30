using System.Data;

namespace WA.Domain.Base
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
