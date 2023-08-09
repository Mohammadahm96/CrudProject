using CrudProject;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudProject
{
    class dbconnection
    {
        
        public string dbconnect()
        {
            string conn = "server=localhost;user=root;password=12345;database=db_crud";
                return conn;
        }
    }
}









