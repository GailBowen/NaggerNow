using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NaggerLibrary
{
    public interface ICard
    {
        ColumnType AssignColumn();

        void Fetch(SqlDataReader rdr);

        void Update();
        
    }
}
