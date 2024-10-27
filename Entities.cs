using System.ComponentModel.DataAnnotations;

namespace DbSeeder;

public class Employee
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime EmploymentDate { get; set; }
    public DateTime BirthDate { get; set; }

    public List<ReturnEmployees> ReturnEmployees { get; set; } = [];
}

public class ReturnEmployees
{
    [Key]
    public Guid Id { get; set; }
    public Guid ReturnId { get; set; } 
    public Guid EmployeeId { get; set; }
}

public class Return
{
    [Key]
    public Guid Id { get; set; }
    public ReturnStatus Status { get; set; }
    public Guid ProductId { get; set; }
    public decimal CompanyCost { get; set; }
    public required DateTime ProcessingStarted { get; set; }
    public required DateTime ProcessingFinished { get; set; }

    public List<ReturnEmployees> ReturnEmployees { get; set; } = [];
}

public enum ReturnStatus
{
    Submitted,
    Processing,
    InRepair,
    Denied,
    Accepted
}

public class Complaint
{
    [Key]
    public Guid Id { get; set; }
    public Guid ReturnId { get; set; }
    public required DateTime IssueDate { get; set; }
    public required DateTime ResolveDate { get; set; }
    public required string Value { get; set; }
}

public class Product
{
    [Key]
    public Guid Id { get; set; }    
    public required string Name { get; set; }
    public DateTime IssueYear { get; set; } 
    public required string MainBuildingMaterial { get; set; }
    public List<Return> Returns { get; set; } = [];
}

