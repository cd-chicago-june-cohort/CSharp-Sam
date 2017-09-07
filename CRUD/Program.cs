using System;
using System.Collections.Generic;
using DbConnection;
using System.Linq;

namespace CRUD
{
    class Program
    {
        // public static List<Dictionary<string, object>> Users = DbConnector.Query("SELECT * FROM users");
        public static void Read() {
            List<Dictionary<string, object>> Users = DbConnector.Query("SELECT * FROM users");
            var names = Users.ToList();            
            foreach (var name in names) {
                foreach (KeyValuePair<string,object> inner in name) {
                    Console.WriteLine(inner.Key + " " + inner.Value);
                }
            }
        }

        public static void Create(string first, string last, int num) {
            DbConnector.Execute($"INSERT INTO users (FirstName, LastName, FavNum) VALUES ('{first}','{last}',{num})");
            Console.WriteLine($"{first} {last} added to database USERS");
            Read();
        }

        public static void Update(int id, string q1 ="", string q2 ="", string q3 = "") {
            List<Dictionary<string, object>> user = DbConnector.Query($"SELECT * FROM users WHERE user_id = {id}");
            Console.WriteLine("User "+ user[0]["FirstName"] + " " + user[0]["LastName"]+ " has been updated!");
            if(q1 !=""){
                DbConnector.Execute($"UPDATE users SET {q1} WHERE user_id = {id}");
            }
            if(q2 !=""){
                DbConnector.Execute($"UPDATE users SET {q2} WHERE user_id = {id}");
            }
            if(q3 !=""){
                DbConnector.Execute($"UPDATE users SET {q3} WHERE user_id = {id}");
            }
            Read();
        }
        
        public static void Destroy(int id){
            List<Dictionary<string, object>> user = DbConnector.Query($"SELECT * FROM users WHERE user_id = {id}");
            Console.WriteLine("User "+ user[0]["FirstName"] + " " + user[0]["LastName"]+ " has been deleted!");
            DbConnector.Execute($"DELETE FROM users WHERE user_id = {id}");
            Read();
        }
        static void Main(string[] args)
        {
            Read();
            // Create("Colin", "Craighead", 3);
            Update(6, "FavNum = 2");
        }
    }
}
