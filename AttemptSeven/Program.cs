using Microsoft.EntityFrameworkCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddTransient<IRequestHandler<CreateCommand, int>, UserService>();
builder.Services.AddTransient<IRequestHandler<UpdateCommand, int>, UserService>();
builder.Services.AddTransient<IRequestHandler<DeleteCommand, int>, UserService>();
builder.Services.AddTransient<IRequestHandler<GetAllQuery, List<User>>, UserService>();

builder.Logging.AddLogging();

var app = builder.Build();

app.UseSwaggerApp();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
