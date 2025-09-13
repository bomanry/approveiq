using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using BLH.ApproveIQ.API.Claims;
using BLH.ApproveIQ.API.Configuration;
using BLH.ApproveIQ.API.Headers;
using BLH.ApproveIQ.API.Models;
using BLH.ApproveIQ.Application.Services;
using BLH.ApproveIQ.Domain.Identity.Models;
using BLH.ApproveIQ.Domain.Services;
using BLH.ApproveIQ.Persistence.Extensions;
using BLH.ApproveIQ.Presentation;

namespace BLH.ApproveIQ.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Scan(
                    selector => selector.FromAssemblies(
                            Infrastructure.AssemblyReference.Assembly,
                            Persistence.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                );

            services.ConfigureAuth(Configuration);

            services.ConfigureControllers();

            services.ConfigureSwagger();

            services.ConfigureMediatR();

            services.ConfigureFluentValidation();

            services.ConfigureDatabase(Configuration);

            //services.ConfigureQuartz();

            services.AddEndpointsApiExplorer();

            services.AddCors();

            services.AddHttpContextAccessor();

            services.AddTransient<ICurrentUserService, CurrentUserService>();

            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy(Policies.AdminOnlyPolicy, policy =>
            //     {
            //         policy.RequireClaim(ApplicationIdentityConstants.AssignedRoleClaimType, 
            //             ApplicationIdentityConstants.Roles.Administrator.ToString());
            //     });
            //     options.AddPolicy(Policies.AdminOrDistrictAdminOnlyPolicy, policy =>
            //     {
            //         policy.RequireClaim(ApplicationIdentityConstants.AssignedRoleClaimType, 
            //         [
            //             ApplicationIdentityConstants.Roles.DistrictAdmin.ToString(), 
            //             ApplicationIdentityConstants.Roles.Administrator.ToString()
            //         ]);
            //     });
            //     options.AddPolicy(Policies.TutorOnly, policy =>
            //     {
            //         policy.RequireClaim(ApplicationIdentityConstants.AssignedRoleClaimType, 
            //             ApplicationIdentityConstants.Roles.Tutor.ToString());
            //         policy.RequireClaim(ApplicationIdentityConstants.AssignedTutorIdClaimType);
            //     });
            // });

            services.AddAutoMapper(Persistence.AssemblyReference.Assembly);

            // services.AddOptions<AzureOptions>()
            //     .Configure<IConfiguration>((settings, configuration) =>
            //     {
            //         configuration.GetSection("Azure").Bind(settings);
            //     })
            //     .ValidateDataAnnotations();
            // services.AddSingleton(sp => sp.GetRequiredService<IOptions<AzureOptions>>().Value);

            services.AddOptions<ResourceFileOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("ResourceFiles").Bind(settings);
                })
                .ValidateDataAnnotations();
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<ResourceFileOptions>>().Value);

            // services.ConfigureGraph();
            //
            // services.ConfigureAzureBlob(Configuration);
            //     
            // services.AddScoped<IClaimsTransformation, UserRolesClaimsTransformation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "BLH ApproveIQ API v1");
                c.RoutePrefix = "";
            });

            //app.EnsureDbIsMigrated().Wait();

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}