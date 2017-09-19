using System.Collections.Generic;
using System.Linq;
using Dapper;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using DojoLeague.Models;
 
namespace DojoLeague.Factory
{
    public class DojoFactory : IFactory<Dojo>
    {
        private string connectionString;
        public DojoFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=DojoLeague;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Dojo item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO dojos (name, location, info) VALUES (@Name, @Location, @Info)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public IEnumerable<Dojo> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM dojos");
            }
        }
        public Dojo GrabOne(int myId){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Dojo>($"SELECT * FROM dojos WHERE id = {myId}").First();
            }
        }

        public IEnumerable<Ninja> AllNinjas(int dojoId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>($"SELECT * FROM ninjas WHERE dojo_id = {dojoId}");
            }
        }

        public void Banish(int ninjaId, int dojoId){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                Console.WriteLine("INSIDE Banish");
                dbConnection.Execute($"UPDATE ninjas SET dojo_id = NULL WHERE ninjas.id = {ninjaId}");
            }
        }

        public void Recruit(int ninjaId, int dojoId){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                Console.WriteLine("INSIDE RECRUIT");
                dbConnection.Execute($"UPDATE ninjas SET dojo_id = {dojoId} WHERE ninjas.id = {ninjaId}");
            }
        }

    }
}