
namespace Lecture4_CORS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("https://trusted-frontend.com", "https://anotherdomain.com", "https://localhost:7204") // Allow specific origin
                          .AllowAnyHeader() // Allow any HTTP headers
                          .AllowAnyMethod() // Allow any HTTP methods (GET, POST, etc.)
                          .AllowCredentials(); // Allow sending credentials (e.g., cookies)
                });

                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin() // Allow all origins
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
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

            app.UseAuthorization();
            // Enable the CORS middleware
            app.UseCors("AllowSpecificOrigin");


            app.MapControllers();

            app.Run();
        }
    }
}
