using ServiceStack.Text;

var seeder = new DbSeeder.DbSeeder();

seeder.Seed(1000);

Directory.CreateDirectory("../../../data");

var products = CsvSerializer.SerializeToCsv(seeder.Products);
File.WriteAllText("../../../data/products.csv", products);

var complaints = CsvSerializer.SerializeToCsv(seeder.Complaints);
File.WriteAllText("../../../data/complaints.csv", complaints);

var employees = CsvSerializer.SerializeToCsv(seeder.Employees);
File.WriteAllText("../../../data/employees.csv", employees);

var returns = CsvSerializer.SerializeToCsv(seeder.Returns);
File.WriteAllText("../../../data/returns", returns);

var returnEmployees = CsvSerializer.SerializeToCsv(seeder.ReturnEmployees);
File.WriteAllText("../../../data/returnEmployees", returnEmployees);
