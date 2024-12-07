using FluentValidation;
using FluentValidation.AspNetCore;
using Messages.Bll.Interfaces;
using Messages.Bll;
using Messages.Web.Models.Requests.Validators;
using Messages.Dal.Interfaces;
using Messages.Dal;
using Messages.Web.Utils;
using Microsoft.EntityFrameworkCore;
using Messages.Web.Mappings;
using Messages.Bll.Mappings;

namespace Messages.Web;

public class Program()
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
 
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
            .AddCommandLine(args)
            .AddEnvironmentVariables()
            .Build();
        var connectionString = builder.Configuration.GetConnectionString("DBConnectionString"); 

        builder.AddAuth();

        builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));
        
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IMessageRepository, MessageRepository>();

        builder.Services.AddAutoMapper(
            typeof(MessageMapperProfileBll),
            typeof(UserMapperProfile),
            typeof(UserMapperProfileBll),
            typeof(ContactMapperProfileBll)
        );
        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<RegistrationUserRequestValidator>();

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

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddleware>();

        app.MapControllers();

        app.Run();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
