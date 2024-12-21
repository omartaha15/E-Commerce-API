using E_Commerce_API.Data;
using E_Commerce_API.Interfaces;
using E_Commerce_API.Model;
using E_Commerce_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Register ApplicationDbContext (DI)
builder.Services.AddDbContext<ApplicationDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

// Register Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add AutoMapper
builder.Services.AddAutoMapper( typeof( Program ) );


builder.Services.AddSwaggerGen( c =>
{
    // Configure JWT authentication in Swagger
    c.AddSecurityDefinition( "Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    } );
    c.AddSecurityRequirement( new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    } );
} );


builder.Services.AddAuthentication( options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    } )
    .AddJwtBearer( options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration [ "JwtSettings:Key" ] ) )
        };
    } );

// Register for Services (DI)
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// Add Cashing 
// Add Authorization to some end points => Done
// Create documentation to the status codes 
// Make some validations  