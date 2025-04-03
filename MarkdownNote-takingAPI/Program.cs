namespace MarkdownNote_takingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


            builder.Services.AddScoped<IEntityBaseRepository<Note>, EntityBaseRepository<Note>>();
            builder.Services.AddScoped<IEntityBaseRepository<NoteFile>, EntityBaseRepository<NoteFile>>();
            builder.Services.AddScoped<IEntityBaseRepository<Category>, EntityBaseRepository<Category>>();
            builder.Services.AddScoped<IEntityBaseRepository<Collaboration>, EntityBaseRepository<Collaboration>>();
            builder.Services.AddScoped<IEntityBaseRepository<Settings>, EntityBaseRepository<Settings>>();
            builder.Services.AddScoped<IEntityBaseRepository<VersionHistory>, EntityBaseRepository<VersionHistory>>();
            builder.Services.AddScoped<IAccountService,AccountService>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<ICollaborationService,CollaborationService>();
            builder.Services.AddScoped<INoteService,NoteService>();
            builder.Services.AddScoped<ISettingsService,SettingsService>();
            builder.Services.AddScoped<IVersionHistoryService,VersionHistoryService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.RequireHttpsMetadata = true;
                op.SaveToken = true;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
                };

            });

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

            app.UseRouting();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}