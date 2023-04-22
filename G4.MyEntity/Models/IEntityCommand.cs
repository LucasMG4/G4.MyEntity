namespace G4.MyEntity.Models {
    public interface IEntityCommand {

        int ExecuteCommand(string commandString, object? parameters = null);

        List<Entity> Get<Entity>(string commandString, object? parameters = null);
        Entity? GetFirstOrDefault<Entity>(string commandString, object? parameters = null);


        void CloseConnection();

    }
}
