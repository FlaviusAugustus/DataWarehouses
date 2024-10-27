using Bogus;
using Bogus.DataSets;

namespace DbSeeder;

public class DbSeeder
{
   public IReadOnlyCollection<Product> Products { get; set; } = [];
   public IReadOnlyCollection<Complaint> Complaints { get; set; } = [];
   public IReadOnlyCollection<Employee> Employees { get; set; } = [];
   public IReadOnlyCollection<Return> Returns { get; set; } = [];
   public IReadOnlyCollection<ReturnEmployees> ReturnEmployees { get; set; } = [];

   public void Seed(int count)
   {
      Products = CreateProductFaker().Generate((int)(count * 0.7));
      Employees = CreateEmployeeFaker().Generate((int)(count * 0.5));
      Returns = CreateReturnFaker(Products).Generate((int)(count * 0.4));
      Complaints = CreateComplaintFaker(Returns).Generate((int)(count * 0.1));
      ReturnEmployees = CreateReturnEmployeesFaker(Employees, Returns).Generate((int)(count * 0.5));
   }

   private static Faker<Product> CreateProductFaker()
   {
      var productFaker = new Faker<Product>()
         .RuleFor(p => p.Id, _ => Guid.NewGuid())
         .RuleFor(p => p.Name, f => f.Commerce.ProductName())
         .RuleFor(p => p.IssueYear, f => f.Date.Past())
         .RuleFor(p => p.MainBuildingMaterial, f => f.Commerce.ProductMaterial());

      return productFaker;
   }

   private static Faker<Complaint> CreateComplaintFaker(IEnumerable<Return> returns)
   {
      var complaintFaker = new Faker<Complaint>()
         .RuleFor(c => c.Id, _ => Guid.NewGuid())
         .RuleFor(c => c.Value, f => f.Lorem.Paragraph(1))
         .RuleFor(c => c.IssueDate, f => f.Date.Past())
         .RuleFor(c => c.ResolveDate, f => f.Date.Recent())
         .RuleFor(c => c.ReturnId, f => f.PickRandom(returns).Id);

      return complaintFaker;
   }

   private static Faker<Return> CreateReturnFaker(IEnumerable<Product> products)
   {
      var returnFaker = new Faker<Return>()
         .RuleFor(r => r.Id, _ => Guid.NewGuid())
         .RuleFor(r => r.Status, f => f.Random.Enum<ReturnStatus>())
         .RuleFor(r => r.ProcessingStarted, f => f.Date.Past())
         .RuleFor(r => r.ProcessingFinished,
            (f, current) => current.ProcessingStarted.AddDays(Random.Shared.Next(7, 30)))
         .RuleFor(r => r.ProductId, f => f.PickRandom(products).Id)
         .RuleFor(r => r.CompanyCost, f => f.Random.Decimal(10, 30));
      
      return returnFaker;
   }

   private static Faker<Employee> CreateEmployeeFaker()
   {
      var employeeFaker = new Faker<Employee>()
         .RuleFor(e => e.Id, _ => Guid.NewGuid())
         .RuleFor(e => e.Name, f => f.Name.FullName())
         .RuleFor(e => e.BirthDate, f => f.Person.DateOfBirth)
         .RuleFor(e => e.EmploymentDate, f => f.Date.Past());

      return employeeFaker;
   }

   private static Faker<ReturnEmployees> CreateReturnEmployeesFaker(
      IEnumerable<Employee> employees,
      IEnumerable<Return> returns)
   {
      var returnEmployeesFaker = new Faker<ReturnEmployees>()
         .RuleFor(r => r.Id, _ => Guid.NewGuid())
         .RuleFor(r => r.EmployeeId, f => f.PickRandom(employees).Id)
         .RuleFor(r => r.ReturnId, f => f.PickRandom(returns).Id);

      return returnEmployeesFaker;
   }
}