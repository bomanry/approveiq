using Microsoft.OpenApi.Models;

namespace BLH.ApproveIQ.API.Configuration;

public static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        var version = AssemblyReference.Assembly.GetName().Version;
        DateTime buildDate = new DateTime(2000, 1, 1)
                .AddDays(version!.Build).AddSeconds(version.Revision * 2);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "Studyville ApproveIQ API",
                Version = "v1",
                Description = $"Version {version} | Published {buildDate}"
            });

            var xmlCommentsFile = $"{BLH.ApproveIQ.Presentation.AssemblyReference.Assembly.GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            options.IncludeXmlComments(xmlCommentsFullPath);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
            });
        });
    }
}
