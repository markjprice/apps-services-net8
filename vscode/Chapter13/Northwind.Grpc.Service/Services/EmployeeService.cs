using Grpc.Core; // To use ServerCallContext.
using Microsoft.Data.SqlClient; // To use SqlConnection and so on.
using System.Data; // To use CommandType.
using Google.Protobuf; // To use ByteString.
using Google.Protobuf.WellKnownTypes; // To use ToTimestamp method.

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
    EmployeeReply e = new();

    e.EmployeeId = r.GetInt32("EmployeeId");
    e.LastName = r.GetString("LastName");
    e.FirstName = r.GetString("FirstName");
    e.Title = r.GetString("Title");
    e.TitleOfCourtesy = r.GetString("TitleOfCourtesy");

    // We must convert DateTime to UTC DateTime and then to Timestamp.
    e.BirthDate = r.GetDateTime("BirthDate").ToUniversalTime().ToTimestamp();
    e.HireDate = r.GetDateTime("HireDate").ToUniversalTime().ToTimestamp();

    e.Address = r.GetString("Address");
    e.City = r.GetString("City");
    e.Region = r.IsDBNull("Region") ? string.Empty : r.GetString("Region");
    e.PostalCode = r.GetString("PostalCode");
    e.Country = r.GetString("Country");
    e.HomePhone = r.GetString("HomePhone");
    e.Extension = r.GetString("Extension");

    // We must convert byte[] to ByteString.
    e.Photo = r.IsDBNull("Photo") ? ByteString.Empty 
      : ByteString.FromStream(r.GetStream("Photo"));

    // Any nullable column must be checked for DBNull.
    e.Notes = r.IsDBNull("Notes") ? string.Empty : r.GetString("Notes");
    e.ReportsTo = r.IsDBNull("ReportsTo") ? 0 : r.GetInt32("ReportsTo");
    e.PhotoPath = r.IsDBNull("PhotoPath") ? string.Empty : r.GetString("PhotoPath");

    return e;
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
