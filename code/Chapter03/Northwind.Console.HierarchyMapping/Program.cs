using Microsoft.EntityFrameworkCore; // GenerateCreateScript()
using Northwind.Models; // HierarchyDb, Person, Student, Employee

DbContextOptionsBuilder<HierarchyDb> options = new();

// Modify this connection string if necessary.
options.UseSqlServer("Data Source=.;Initial Catalog=HierarchyMapping;Integrated Security=true;TrustServerCertificate=true;");

using (HierarchyDb db = new(options.Options))
{
  bool deleted = await db.Database.EnsureDeletedAsync();
  WriteLine($"Database deleted: {deleted}");

  bool created = await db.Database.EnsureCreatedAsync();
  WriteLine($"Database created: {created}");

  WriteLine("SQL script used to create the database:");
  WriteLine(db.Database.GenerateCreateScript());

  if ((db.Employees is not null) && (db.Students is not null))
  {
    db.Students.Add(new Student { Name = "Connor Roy", 
      Subject = "Politics" });

    db.Employees.Add(new Employee { Name = "Kerry Castellabate", 
      HireDate = DateTime.UtcNow });
    
    int result = db.SaveChanges();
    WriteLine($"{result} people added.");
  }

  if (db.Students is null || !db.Students.Any())
  {
    WriteLine("There are no students.");
  }
  else
  {
    foreach (Student student in db.Students)
    {
      WriteLine("{0} studies {1}",
        student.Name, student.Subject);
    }
  }

  if (db.Employees is null || !db.Employees.Any())
  {
    WriteLine("There are no employees.");
  }
  else
  {
    foreach (Employee employee in db.Employees)
    {
      WriteLine("{0} was hired on {1}",
        employee.Name, employee.HireDate);
    }
  }

  if (db.People is null || !db.People.Any())
  {
    WriteLine("There are no people.");
  }
  else
  {
    foreach (Person person in db.People)
    {
      WriteLine("{0} has ID of {1}",
        person.Name, person.Id);
    }
  }
}
