using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using MySql.Data.MySqlClient;

namespace DAL
{
	//Connected Data Access
	//Connection is kept open till operation get completed.
	//Explicitly connectin object has been managed with opening and closing by code
	//comd.ExecuteScalar :::::::::::::for Aggrdate functions with SELECT
	//comd.ExecuteNonQuery:::::::::::::for DML  (insert, update or delete command)
	//comd.ExecuteReader::::::::::::::::for SQL (Select command)

	//Helper class

	//DAL-Data access layer
	public class SalesmanDAL
    {
		// step 1:craete a Connection_string 
		public static string connectionString = @"server=localhost;user=root;password='root';database=knowitdb";

		public static List<Salesman>  GetAll()
        {
			//bool status = false;

			List<Salesman> salesman = new List<Salesman>();
			
			//step 2 :create a conn object whcih access a connection String
			IDbConnection conn = new MySqlConnection(connectionString);

            try
            {
				conn.Open();// step 3:open a connection

				//step 4: write a query to aaccesdata fro databse table

				string query = "SELECT * from salesman";

				// step 5 : pass this query as a command to database
				IDbCommand cmd = new MySqlCommand(query, conn  as MySqlConnection);
				
				//step 6: -write a command type like text or stored procedure/trigger/function
				cmd.CommandType = CommandType.Text;

				//step 7: wite a datareader according two 3 tyape
				//1) DML commnads (insert,delete ,update) use :comd.ExecuteNonQuery:
				//2) DDL comand {select stmt } use cmd .ExecuteReader
				//3) aggregate function (max count and so on) :use -cmd.ExecuteScalar

				IDataReader reader = cmd.ExecuteReader();

				//step 8:read data one by one using loop

				while(reader.Read())
                {
					Salesman salesman1 = new Salesman();
					salesman1.id = int.Parse(reader["id"].ToString());
					salesman1.name = reader["name"].ToString();
					salesman1.area = reader["area"].ToString();
					salesman1.totalsales = int.Parse(reader["totalsales"].ToString());

					salesman.Add(salesman1);

                }
				reader.Close();

            }
			catch(MySqlException e )
            {
				
				Console.WriteLine(e);
            }
            finally
            {
				conn.Close();
            }

			return salesman;

        }

		public static BOL.Salesman GetById(int id)
        {
			BOL.Salesman emp = new BOL.Salesman();
			List<Salesman> employess = new List<Salesman>();
			IDbConnection conn = new MySqlConnection(connectionString);
            try
            {
				conn.Open();
				string query = " SELECT * from salesman where id =" + id;

				IDbCommand cmd = new MySqlCommand(query, conn as MySqlConnection);
				cmd.CommandType = CommandType.Text;

				IDataReader reader = cmd.ExecuteReader();
				if(reader.Read())
                {
					emp.id = int.Parse(reader["id"].ToString());
					emp.name = reader["name"].ToString();
					emp.area = reader["area"].ToString();
					emp.totalsales = int.Parse(reader["totalsales"].ToString());

				}

				reader.Close();
			}
			catch(MySqlException ex)
            {
				string er = ex.Message;
            }
            finally
            {
				conn.Close();
            }
			return emp;

        }

		public static bool Delete(int id)
        {
			bool status = false;

			IDbConnection conn = new MySqlConnection(connectionString);
            try
            {
				conn.Open();
				string query = "DELETE from salesman where id =" + id;
				IDbCommand cmd = new MySqlCommand(query, conn as MySqlConnection);
				cmd.CommandType = CommandType.Text;
				int rowAffected = cmd.ExecuteNonQuery();//DML command 

				if(rowAffected > 0)
                {
					status = true;
				}

            }
			catch(MySqlException s)
            {
				string e = s.Message;
				Console.WriteLine(s);
            }
            finally
            {
				conn.Close();

            }
			return status;
        }

		public static bool Insert(Salesman data)
        {
			bool status = false;
			IDbConnection conn = new MySqlConnection(connectionString);
            try
            {
				conn.Open();

				 string query ="Insert into salesman(id,name,area,totalsales) values ("+data.id + " ,' "+data.name+"' ,' "+data.area+"' ,"+data.totalsales+" ) ";
				MySqlCommand cmd = new MySqlCommand(query,conn as MySqlConnection);
				cmd.CommandType = CommandType.Text;
				int rowaffected = cmd.ExecuteNonQuery();

				if(rowaffected>0)
                {
					status = true;
                }

            }
			catch (MySqlException e )
            {
				Console.WriteLine(e);
            }
            finally
            {
				conn.Close();
            }
			return status;
        }
		public static bool Update(Salesman emp)
		{
			bool status = false;
			IDbConnection conn = new MySqlConnection(connectionString);
			try
			{
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "UPDATE Salesman SET name='" + emp.name + "',area='"
									+ emp.area + "', totalsales=" + emp.totalsales + " WHERE ID=" + emp.id;
				cmd.Connection = (MySqlConnection)conn;
				conn.Open();
				int rowaffected = cmd.ExecuteNonQuery();

				if (rowaffected > 0)
				{
					status = true;
				}

			}
			catch (MySqlException e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				conn.Close();
			}
			return status;
		}
	}
}
 
// comment added by saurabh 
