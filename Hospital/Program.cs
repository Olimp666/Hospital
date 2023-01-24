using Database.Repository;
using Domain.Repositories;
using Domain.UseCases;
using Hospital.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<Database.AppContext>(options =>
            options.UseNpgsql($"Host=localhost;Port=5432;Database=backend_db;Username=postgres;Password=niggers"));
        builder.Services.AddDbContext<Database.AppContext>(options =>
            options.EnableSensitiveDataLogging(true));
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();
        builder.Services.AddTransient<ISessionRepository, SessionsRepository>();
        builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
        builder.Services.AddTransient<ISpecializationRepository, SpecializationRepository>();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddTransient<DoctorService>();
        builder.Services.AddTransient<ScheduleService>();
        builder.Services.AddTransient<SessionService>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
        builder.Services.AddControllersWithViews();

        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
            opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer"
            });
            opt.OperationFilter<AuthenticationRequirementsOperationFilter>();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapRazorPages();

        app.Run();
    }
}