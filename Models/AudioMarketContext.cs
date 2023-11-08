using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN_Project.Models
{

    [Table("Audio")]
    public class Audio
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("User")]
        public int? artistId { get; set; }

        [ForeignKey("Genre")]
        public int? genreId { get; set; }

        [ForeignKey("Mood")]
        public int? moodId { get; set; }

        [Required]
        public bool status { get; set; }

        [MaxLength(255)]
        public string filename { get; set; }

        [MaxLength(255)]
        public string image { get; set; }

        [MaxLength(255)]
        public string title { get; set; }

        public virtual User User { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Mood Mood { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? discountAmount { get; set; }

        public int? quantity { get; set; }

        [Required]
        public bool status { get; set; }

        public virtual List<Order> Orders { get; set; }
    }

    [Table("Favorite")]
    public class Favorite
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("User")]
        public int userId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Audio")]
        public int audioId { get; set; }

        public virtual User User { get; set; }
        public virtual Audio Audio { get; set; }
    }

    [Table("Genre")]
    public class Genre
    {
        [Key]
        public int id { get; set; }

        [MaxLength(255)]
        public string genre { get; set; }

        [Required]
        public bool status { get; set; }
    }

    [Table("Mood")]
    public class Mood
    {
        [Key]
        public int id { get; set; }

        [MaxLength(255)]
        public string moodName { get; set; }

        [Required]
        public bool status { get; set; }
    }

    [Table("Order")]
    public class Order
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("User")]
        public int? buyerId { get; set; }

        [ForeignKey("Audio")]
        public int? audioId { get; set; }

        public int status { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime purchaseDate { get; set; }

        public virtual User User { get; set; }
        public virtual Audio Audio { get; set; }
    }

    [Table("Review")]
    public class Review
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("User")]
        public int? userId { get; set; }

        [ForeignKey("Audio")]
        public int? audioId { get; set; }

        public int? rating { get; set; }

        [MaxLength]
        public string comment { get; set; }

        public int status { get; set; }
        public virtual User User { get; set; }
    }

    [Table("User")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [MaxLength(255)]
        public string username { get; set; }

        [MaxLength(255)]
        public string password { get; set; }

        public int role { get; set; }

        [MaxLength(255)]
        public string name { get; set; }

        [Required]
        public bool status { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
    }


    public class AudioMarketContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.UseSqlServer(config["ConnectionStrings:DbConnection"]);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>().HasKey(u => new
            {
                u.userId,
                u.audioId
            });
        }
    }

}
