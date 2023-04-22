using G4.MyEntity.Entities;
using G4.MyEntity.Enums;
using G4.MyEntity.Models;

namespace G4.MyEntity {
    public class EntityContext {

        public string ConnectionString { get; set; } = "";

        public DatabaseEngine DatabaseEngine { get; set; } = DatabaseEngine.MySQL;

        public IEntityCommand CreateCommand() {

            if (DatabaseEngine == DatabaseEngine.MySQL)
                return new MySQLDatabaseCommand(ConnectionString);

            throw new ArgumentException("Selected engine is not implemented.");


        }

    }
}
