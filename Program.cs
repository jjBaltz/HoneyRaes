using HoneyRaesAPI.Models;

List<Customer> customers = new List<Customer>
{
    new Customer
    {
        Id = 1,
        Name = "Heather",
        Address = "3522 Jarvisville Road"
    },
    new Customer
    {
        Id = 2,
        Name = "Roger",
        Address = "438 Harley Vincent Drive"
    },
    new Customer
    {
        Id = 3,
        Name = "Charlie",
        Address = "3382 Deans Lane"
    }
};

List<Employee> employees = new List<Employee>
{
    new Employee
    {
        Id = 1,
        Name = "Tatiana",
        Specialty = "Graphic Designer"
    },
    new Employee
    {
        Id = 2,
        Name = "Reuben",
        Specialty = "Screen Printer"
    }
};

List<ServiceTicket> serviceTickets = new List<ServiceTicket>
{
    new ServiceTicket
    {
        Id = 1,
        CustomerId = 1,
        EmployeeId = 2,
        Description = "200 tshirts before the end of the month for 5K charity run",
        Emergency = true,
        DateCompleted = DateTime.Now
    },
    new ServiceTicket
    {
        Id = 2,
        CustomerId = 2,
        EmployeeId = 1,
        Description = "final logo design approval",
        Emergency = false
    },
    new ServiceTicket
    {
        Id = 3,
        CustomerId = 2,
        EmployeeId = 2,
        Description = "add extra sleeve print before shipping",
        Emergency = false,
        DateCompleted = DateTime.Now
    },
    new ServiceTicket
    {
        Id = 4,
        CustomerId = 3,
        EmployeeId = 2,
        Description = "Screen Print Demo for art crawl",
        Emergency = true,
        DateCompleted = DateTime.Now
    },
    new ServiceTicket
    {
        Id = 5,
        CustomerId = 1,
        EmployeeId = 1,
        Description = "consultation for new restaurant merch logo design",
        Emergency = false
    }
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/customers", () =>
{
    return customers;
});

app.MapGet("/customers/{id}", (int id) =>
{
    Customer customer = customers.FirstOrDefault(cust => cust.Id == id);
    if (customer == null)
    {
        return Results.NotFound();
    }
    customer.ServiceTickets = serviceTickets.Where(st => st.CustomerId == id).ToList();
    return Results.Ok(customer);
});

app.MapGet("/employees", () =>
{
    return employees;
});

app.MapGet("/employees/{id}", (int id) =>
{
    Employee employee = employees.FirstOrDefault(em => em.Id == id);
    if (employee == null)
    {
        return Results.NotFound();
    }
    employee.ServiceTickets = serviceTickets.Where(st => st.EmployeeId == id).ToList();
    return Results.Ok(employee);
});

app.MapGet("/servicetickets", () =>
{
    return serviceTickets;
});

app.MapGet("/servicetickets/{id}", (int id) =>
{
    ServiceTicket serviceTicket = serviceTickets.FirstOrDefault(st => st.Id == id);
    if (serviceTicket == null)
    {
        return Results.NotFound();
    }
    serviceTicket.Employee = employees.FirstOrDefault(em => em.Id == serviceTicket.EmployeeId);
    serviceTicket.Customer = customers.FirstOrDefault(cust => cust.Id == serviceTicket.CustomerId);
    return Results.Ok(serviceTicket);
});

app.Run();
