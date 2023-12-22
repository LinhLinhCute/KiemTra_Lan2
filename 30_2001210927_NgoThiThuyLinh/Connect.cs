using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace _30_2001210927_NgoThiThuyLinh
{
    class Connect
    {
        public SqlConnection connect;
        public Connect()
        {
            connect = new SqlConnection("Data Source =A209PC18; Initial Catalog= mydb; Integrated Security = true");
        }
        public Connect(string strcn)
        {
            connect = new SqlConnection(strcn);
        }
    }
}
