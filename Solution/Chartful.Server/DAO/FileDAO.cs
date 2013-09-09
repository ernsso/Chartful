using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Chartful.Server.Bean;

namespace Chartful.Server.DAO
{
    public class FileDAO
    {
        private static FileDAO instance = null;

        private const string tableName = "File";

        private MySqlConnection connection;
        private MySqlCommand command;

        public static FileDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new FileDAO();
                return instance;
            }
        }

        private FileDAO()
        {
            try
            {
                this.connection = Database.GetConnection();
            }
            catch (Exception e) { Tools.Log(Tools.LogType.Error, e.Message); }
        }

        public void create(File file)
        {
            try
            {
                this.connection.Open();
                string query = "INSERT INTO " + tableName + " (Name, Data) VALUES(@name, @data);";
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                command.Parameters.AddWithValue("@name", file.Name);
                command.Parameters.AddWithValue("@data", file.Content);

                command.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
        }

        public void create(string name, byte[] content)
        {
            File newFile = new File(name, content);
            this.create(newFile);
        }

        public File select(int id)
        {
            try
            {
                string query = "SELECT * FROM " + tableName + " WHERE Id = @id;";
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();

                return this.transformReader(reader).First();
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            return null;
        }

        public List<File> select()
        {
            try
            {
                string query = "SELECT * FROM " + tableName;
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                MySqlDataReader reader = command.ExecuteReader();

                return this.transformReader(reader);
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            return null;
        }

        public File update(File file)
        {
            try
            {
                this.connection.Open();
                string query = "UPDATE " + tableName + " SET Name= @name, Data = @data WHERE Id = @id;";
                command = new MySqlCommand(query, this.connection);
                command.Prepare();

                command.Parameters.AddWithValue("@id", file.Id);
                command.Parameters.AddWithValue("@name", file.Name);
                command.Parameters.AddWithValue("@data", file.Content);

                command.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            return null;
        }

        public void delete(File file)
        {
            // TODO Auto-generated method stub
        }

        private List<File> transformReader(MySqlDataReader reader)
        {
            int id;
            string name;
            byte[] content;

            try
            {
                List<File> fileList = new List<File>();
                while (reader.Read())
                {
                    id = reader.GetInt32("Id");
                    name = reader.GetString("Name");
                    content = (byte[])reader.GetValue(0);

                    File file = new File(id, name, content);
                    fileList.Add(file);
                }
                return fileList;
            }
            catch (MySqlException ex) { Tools.Log(Tools.LogType.Error, "MySQL error n°" + ex.Number + " has occurred : " + ex.Message); }
            catch (Exception ex) { Tools.Log(Tools.LogType.Error, ex.Message); }
            return null;
        }
    }
}