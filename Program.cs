
var builder = WebApplication.CreateBuilder(args);

// add service to the timer
builder.Services.AddScoped<IMyDependency, MyDependency>();
builder.Services.AddHostedService<TimedTaskService>();

// Add services to the container.
builder.Services.AddControllers();
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
app.UseDefaultFiles();
app.UseStaticFiles();//启动静态资源文件访问index.html

app.UseAuthorization();
app.MapControllers();
//app.Run();
await app.RunAsync();
