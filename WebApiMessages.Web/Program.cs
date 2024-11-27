using FluentValidation;
using FluentValidation.AspNetCore;
using Messages.Bll.Interfaces;
using Messages.Bll;
using Messages.Web.Models.Requests.Validators;
using Messages.Dal.Interfaces;
using Messages.Dal;


namespace Messages.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            // Add services to the container.

            JWT.AddAuth(builder);
            
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

            app.MapControllers();

            app.Run();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
