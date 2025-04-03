namespace MarkdownNote_takingAPI.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Note> notes { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<IdentityRole> roles { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Settings> settings { get; set; }
        public DbSet<Collaboration> collaborations { get; set; }
        public DbSet<VersionHistory> versionHistorys { get; set; }
        public DbSet<NoteFile> noteFiles { get; set; }
    }
}
