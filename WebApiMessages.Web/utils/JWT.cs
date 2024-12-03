using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Messages.Web.Utils;

public static class JWT
{
    internal static void AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:7166",
                ValidAudience = "https://localhost:7166",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SUPER-message-superSecretKey@345"))
            };
        });
    }

    internal static string GetToken(string id)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SUPER-message-superSecretKey@345"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:7166",
            audience: "https://localhost:7166",
            claims: new List<Claim>() { new Claim("id", id) },
            expires: DateTime.Now.AddMinutes(60000),
            signingCredentials: signinCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }

    public static Guid DecodeJwtAndReturnId(string jwt)
    {    
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        //var keyId = token.Header.Kid;
        //var audience = token.Audiences.ToList();
        //var claims = token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
        var result = token.Claims.FirstOrDefault(n => n.Type == "id").Value;
        var id = Guid.Parse(result);
        return id;
    }
}

