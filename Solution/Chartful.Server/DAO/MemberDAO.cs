using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Chartful.Server.Bean;

namespace Chartful.Server.DAO
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;

        private const string tableName = "Member";

        private MySqlConnection connection;
        private MySqlCommand command;

        public static MemberDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new MemberDAO();
                return instance;
            }
        }

        private MemberDAO()
        {
            try
            {
                this.connection = Database.GetConnection();
            }
            catch (Exception e) { Tools.Log(Tools.LogType.Error, e.Message); }
        }

        public bool create(Member member)
        {
            bool success = false;
            try
            {
                this.connection.Open();
                string query = "INSERT INTO " + tableName + " (Login, Salt, Hash, Email) VALUES(@login, @salt, @hash, @email);";
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                command.Parameters.AddWithValue("@login", member.Login);
                command.Parameters.AddWithValue("@salt", member.Salt);
                command.Parameters.AddWithValue("@hash", member.Hash);
                command.Parameters.AddWithValue("@email", member.Email);

                //command.Parameters["@login"].Value = member.Login;
                //command.Parameters["@salt"].Value = member.Salt;
                //command.Parameters["@hash"].Value = member.Hash;
                //command.Parameters["@email"].Value = member.Email;

                command.ExecuteNonQuery();
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            finally { this.connection.Close(); }
            return success;
        }

        public bool create(string login, string password, string email)
        {
            Member newMember = new Member(login, password, email);
            return this.create(newMember);
        }

        public Member select(int id)
        {
            try
            {
                this.connection.Open();
                string query = "SELECT * FROM " + tableName + " WHERE Id = @id;";
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();

                return this.transformReader(reader).First();
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            finally { this.connection.Close(); }
            return null;
        }

        public Member select(string login)
        {
            try
            {
                this.connection.Open();
                string query = "SELECT * FROM " + tableName + " WHERE Login = @login;";
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                command.Parameters.AddWithValue("@login", login);
                MySqlDataReader reader = command.ExecuteReader();

                return this.transformReader(reader).First();
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            finally { this.connection.Close(); }
            return null;
        }

        public List<Member> select()
        {
            try
            {
                this.connection.Open();
                string query = "SELECT * FROM " + tableName;
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                MySqlDataReader reader = command.ExecuteReader();

                return this.transformReader(reader);
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            finally { this.connection.Close(); }
            return null;
        }

        public Member update(Member member)
        {
            try
            {
                this.connection.Open();
                string query = "UPDATE " + tableName + " SET Login= @login, Salt = @salt, Hash = @hash, Email = @email WHERE Id = @id;";
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                command.Parameters.AddWithValue("@id", member.Id);
                command.Parameters.AddWithValue("@login", member.Login);
                command.Parameters.AddWithValue("@salt", member.Salt);
                command.Parameters.AddWithValue("@hash", member.Hash);
                command.Parameters.AddWithValue("@email", member.Email);

                command.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            finally { this.connection.Close(); }
            return null;
        }

        public void delete(Member member)
        {
            // TODO Auto-generated method stub
        }

        private List<Member> transformReader(MySqlDataReader reader)
        {
            int id;
            String login, salt, hash, email;

            try
            {
                List<Member> memberList = new List<Member>();
                while (reader.Read())
                {
                    id = reader.GetInt32("Id");
                    login = reader.GetString("Login");
                    salt = reader.GetString("Salt");
                    hash = reader.GetString("Hash");
                    email = reader.GetString("Email");

                    Member member = new Member(id, login, salt, hash, email);
                    memberList.Add(member);
                }
                return memberList;
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            return null;
        }
    }
}