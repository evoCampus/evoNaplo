var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Register student service
builder.Services.AddSingleton<evoNaplo.Services.Interface.IStudentService, evoNaplo.Services.Services.StudentService>();
builder.Services.AddSingleton<evoNaplo.Services.Interface.IMentorService, evoNaplo.Services.Services.MentorService>();
builder.Services.AddSingleton<evoNaplo.Services.Interface.IProjectService, evoNaplo.Services.Services.ProjectService>();
builder.Services.AddSingleton<evoNaplo.Services.Interface.ITeamService, evoNaplo.Services.Services.TeamService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
