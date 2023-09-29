using Microsoft.Extensions.Options;
using Pulse.Core.DTOs;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;
using Pulse.Core.Interfaces.Validations;
using Pulse.Core.Services;
using Pulse.Core.Settings;
using Pulse.Core.Validations;
using Pulse.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .Configure<MongoSettings>(builder.Configuration.GetSection("PulseDbSettings"));

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
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
