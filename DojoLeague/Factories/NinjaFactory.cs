using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using DojoLeague.Models;
using System;
 
namespace DojoLeague.Factory
{
    public class NinjaFactory : IFactory<Ninja>
    {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=DojoLeague;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Ninja item, int myDojo)
        {
            Console.WriteLine("my dojo value printing below");
            Console.WriteLine(myDojo);
            item.Dojo = new Dojo();
            using (IDbConnection dbConnection = Connection) {
                if (myDojo == 0){
                    string query =  $"INSERT INTO ninjas (name, level, description, dojo_id) VALUES (@Name, @Level, @Description, NULL)";
                    dbConnection.Open();
                    dbConnection.Execute(query, item);
                } else {
                    string query =  $"INSERT INTO ninjas (name, level, description, dojo_id) VALUES (@Name, @Level, @Description, {myDojo})";
                    dbConnection.Open();
                    dbConnection.Execute(query, item);
                }
            }
        }

        public IEnumerable<Ninja> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas");
            }
        }

        public IEnumerable<object> HomeInfo()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query("SELECT ninjas.id as ninja_id, ninjas.name as ninja_name, dojos.name as dojo_name, dojo_id FROM ninjas JOIN dojos ON dojo_id = dojos.id");
            }
            
        }

        public IEnumerable<Ninja> GrabRogues()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas WHERE dojo_id IS NULL");
            }
        }

        public Ninja GrabOne(int myId){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>($"SELECT * FROM ninjas WHERE id = {myId}").First();
            }
        }

        public object CurrentDojo(int myId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query($"SELECT dojos.name as dojo_name, dojo_id FROM ninjas JOIN dojos ON ninjas.dojo_id = dojos.id WHERE ninjas.id = {myId}").First();
            }
            
        }

    }
}