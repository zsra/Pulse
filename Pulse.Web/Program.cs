using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pulse.Core.DTOs;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;
using Pulse.Core.Interfaces.Validations;
using Pulse.Core.Services;
using Pulse.Core.Settings;
using Pulse.Core.Validations;
using Pulse.Infrastructure.Repositories;
using Pulse.Infrastructure.Seeds;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .Configure<MongoSettings>(builder.Configuration.GetSection("PulseDbSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services
    .AddSingleton<IMongoSettings>(
        provider => provider.GetRequiredService<IOptions<MongoSettings>>().Value);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();

builder.Services.AddTransient<IValidation<CreateCommentDto>, CreateCommentValidation>();
builder.Services.AddTransient<IValidation<CreatePostDto>, CreatePostValidation>();
builder.Services.AddTransient<IValidation<SignInDto>, SignInValidation>();
builder.Services.AddTransient<IValidation<SignUpDto>, SignUpValidation>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

InitSeed seed = new InitSeed(app.Services.GetService<IUserRepository>()!);

seed.Add().GetAwaiter();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
