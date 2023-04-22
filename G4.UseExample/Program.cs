
using G4.MyEntity;
using G4.UseExample;
using System.Text;

// Instance and configure of lib context
var context = new EntityContext();
context.ConnectionString = "server=localhost;port=3306;user=test_user;password=regular";

// Get a instance of command
var command = context.CreateCommand();

// Create test database
command.ExecuteCommand("CREATE DATABASE IF NOT EXISTS G4_TEST");
command.ExecuteCommand("USE G4_TEST");

// Create test table
var sqlCreateTable = new StringBuilder();
sqlCreateTable.Append("CREATE TABLE IF NOT EXISTS G4_CUSTOMER (");
sqlCreateTable.AppendLine("id CHAR(36) NOT NULL,");
sqlCreateTable.AppendLine("name VARCHAR(70) NOT NULL DEFAULT '',");
sqlCreateTable.AppendLine("PRIMARY KEY(id) )");
command.ExecuteCommand(sqlCreateTable.ToString());

var menu = "";

while(!menu.Equals("3")) {

    Console.WriteLine("");
    Console.WriteLine("= 1 - New Customer");
    Console.WriteLine("= 2 - List All");
    Console.WriteLine("= 3 - Exit");

    menu = Console.ReadLine();

    if (menu == null) menu = "";

    if(menu.Equals("1")) {

        Console.WriteLine("");
        Console.WriteLine("= New Customer Name :");
        var name = Console.ReadLine();

        if(name == null) {
            Console.WriteLine("= Invalid Name");
        } else {

            var newCustomer = new Customer() {
                Id = Guid.NewGuid(),
                Name = name
            };

            var sql = "INSERT INTO G4_CUSTOMER SET id = @Id, name = @Name";

            command.ExecuteCommand(sql, newCustomer);

        }

    }

    if(menu.Equals("2")) {

        var customers = command.Get<Customer>("SELECT * FROM G4_CUSTOMER");

        foreach (var customer in customers) {

            Console.WriteLine("");
            Console.WriteLine($"ID   : {customer.Id}");
            Console.WriteLine($"Name : {customer.Name}");

        }

    }

}
