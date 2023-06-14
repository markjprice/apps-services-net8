using Grpc.Core; // To use ServerCallContext.
using Microsoft.Data.SqlClient; // To use SqlConnection and so on.
using System.Data; // To use CommandType.

namespace Northwind.Grpc.Service.Services;

public class EmployeeService : Employee.EmployeeBase
{
  private readonly ILogger<EmployeeService> _logger;

  public EmployeeService(ILogger<EmployeeService> logger)
  {
    _logger = logger;
  }

  private async Task<SqlCommand> GetCommand()
  {
    SqlConnectionStringBuilder builder = new();

    builder.InitialCatalog = "Northwind";
    builder.MultipleActiveResultSets = true;
    builder.Encrypt = true;
    builder.TrustServerCertificate = true;
    builder.ConnectTimeout = 10; // Default is 30 seconds.
    builder.DataSource = "."; // To use local SQL Server.
    builder.IntegratedSecurity = true;

    /*
    // To use SQL Server Authentication:
    builder.UserID = userId;
    builder.Password = password;
    builder.PersistSecurityInfo = false;
    */

    SqlConnection connection = new(builder.ConnectionString);

    await connection.OpenAsync();

    SqlCommand cmd = connection.CreateCommand();
    cmd.CommandType = CommandType.Text;
    return cmd;
  }

  private EmployeeReply ReaderToEmployee(SqlDataReader r)
  {
    return new()
    {
      EmployeeId = r.GetInt32("ProductId"),
      LastName = r.GetString("LastName"),
      FirstName = r.GetString("FirstName"),
    };
  }

  public override async Task<EmployeeReply?> GetEmployee(
    EmployeeRequest request, ServerCallContext context)
  {
    SqlCommand cmd = await GetCommand();
    cmd.CommandText = "SELECT * FROM Employees WHERE EmployeeId = @id";
    cmd.Parameters.AddWithValue("id", request.EmployeeId);

    SqlDataReader r = await cmd.ExecuteReaderAsync(
      CommandBehavior.SingleRow);

    EmployeeReply? employee = null;

    // Read the expected single row.
    if (await r.ReadAsync())
    {
      employee = ReaderToEmployee(r);
    }

    await r.CloseAsync();

    return employee;
  }

  public override async Task<EmployeesReply?> GetEmployees(
    EmployeesRequest request, ServerCallContext context)
  {
    SqlCommand cmd = await GetCommand();
    cmd.CommandText = "SELECT * FROM Employees";

    SqlDataReader r = await cmd.ExecuteReaderAsync(
      CommandBehavior.SingleResult);

    EmployeesReply? employees = new();

    while (await r.ReadAsync())
    {
      employees.Employees.Add(ReaderToEmployee(r));
    }

    await r.CloseAsync();

    return employees;
  }
}
