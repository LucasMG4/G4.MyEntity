using G4.MyEntity.Controllers;
using G4.MyEntity.Models;
using MySql.Data.MySqlClient;

namespace G4.MyEntity.Entities {
    public class MySQLDatabaseCommand : IEntityCommand {

        private MySqlConnection Connection;

        public MySQLDatabaseCommand(string connectionString) {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void CloseConnection() {
            Connection.Close();
        }

        private MySqlCommand CreateCommand(string commandString, object? parameters) {

            var command = new MySqlCommand(commandString, Connection);

            GetParameters(parameters).ForEach(parameter => command.Parameters.AddWithValue(parameter.Name, parameter.Value));

            return command;

        }

        public int ExecuteCommand(string commandString, object? parameters = null) => CreateCommand(commandString, parameters).ExecuteNonQuery();   

        public List<Entity> Get<Entity>(string commandString, object? parameters = null) {
            
            var command = CreateCommand(commandString, parameters);
            var reader = command.ExecuteReader();
            var result = new List<Entity>();

            var propertiesFromObj = typeof(Entity).GetProperties();

            while (reader.Read()) {

                var obj = Activator.CreateInstance<Entity>();

                if (obj == null)
                    throw new NullReferenceException($"Is not possible to create a new instance of '{typeof(Entity).FullName}'");

                for(int fieldPosition = 0; fieldPosition < reader.FieldCount; fieldPosition++) {

                    var propertyObj = propertiesFromObj.Where(x => x.Name.ToLower().Equals(reader.GetName(fieldPosition).ToLower())).FirstOrDefault();

                    if(propertyObj != null) {

                        propertyObj.SetValue(obj, reader.GetValue(fieldPosition));

                    }
                    
                }

                result.Add(obj);

            }

            reader.Close();

            return result;

        }

        public Entity? GetFirstOrDefault<Entity>(string commandString, object? parameters = null) {
            return Get<Entity>(commandString, parameters).FirstOrDefault();
        }

        private List<EntityParameter> GetParameters(object? parameters) => parameters == null ? new List<EntityParameter>() : parameters.TransformObjectToParameters();

    }
}
