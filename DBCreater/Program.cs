using Messages.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBCreater
{
    public class Program
    {
        static void Main(string[] args)
        {
  
            Context.context.Database.EnsureCreated();

            //var result = context.RequestsForAdd.ToList();

            //CREATE USER ROLE
            //var roleIdA = new UserRoleDto();
            //roleIdA.Id = 3;
            //roleIdA.Role = "admin";
            //context.UserRoles.Add(roleIdA);
            //context.SaveChanges();

            //var roleIdM = new UserRoleDto();
            //roleIdM.Id = 2;
            //roleIdM.Role = "manager";
            //context.UserRoles.Add(roleIdM);
            //context.SaveChanges();

            //var roleIdU = new UserRoleDto();
            //roleIdU.Id = 1;
            //roleIdU.Role = "user";
            //context.UserRoles.Add(roleIdU);
            //context.SaveChanges();



            //var authorize = context.Users.Where(s => s.Email == "@Pochta").FirstOrDefault();
            //Console.WriteLine(authorize.Id);
            //var t = authorize.Role;
            //Console.WriteLine(t.Id);

        }
    }
}
