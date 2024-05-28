
using Emp.API.Common;
using Emp.Data;
using Emp.Data.Repositories.IRepository;
using Emp.Data.Repositories.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Database 
builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("Emp.Data"));
});
#endregion
#region Dependency Injection
builder.Services.AddTransient<IEmployeeRepository,EmployeeServiceRepository>();
builder.Services.AddTransient<IDepartmentRepository,DepartmentServiceRepository>(); 
builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericServiceRepository<>));
#endregion
#region Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
