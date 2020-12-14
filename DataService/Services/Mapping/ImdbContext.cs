using DataService.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DataService
{
    public class ImdbContext : DbContext
    {
        PostgresSQL_Connect_String myConnection = new PostgresSQL_Connect_String();

        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql(myConnection.ToString());
        }

        public DbSet<Genre> genre { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Title_Genre> title_genre { get; set; }
        public DbSet<Akas_Type> akas_type { get; set; }
        public DbSet<Person_known_title> PersonKnownTitles { get; set; }

        public DbSet<Person_Profession> PersonProfessions { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Rating> rating { get; set; }
        public DbSet<Title_Rating> title_rating { get; set; }
        public DbSet<Search_History> search_history { get; set; }
        public DbSet<Person_Bookmark> person_bookmarks { get; set; }
        public DbSet<Person_Bookmark_list> person_bookmark_list { get; set; }
        public DbSet<Akas_Attribute> akas_attributes { get; set; }
        public DbSet<Akas> akas { get; set; }
        public DbSet<Title_Person> TitlePersons { get; set; }
        public DbSet<Title_Bookmark> title_bookmarks { get; set; }
        public DbSet<Title_Bookmark_List> title_bookmark_list { get; set; }
        public DbSet<Title_Episode> title_episode { get; set; }
        public DbSet<Title_Search> title_search { get; set; }
        public DbSet<TitleType> type { get; set; }
        public DbSet<Person_Rating> PersonRatings { get; set; }
        public DbSet<Title> title { get; set; }
        
        public DbSet<OmdbData> omdb_data { get; set; }
        
        public DbSet<TopPoster> TopPosters { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<TitleRuntime> TitleRuntime { get; set; }
        
        public DbSet<PersonWithProfession> PersonWithProfessions { get; set; }
        public DbSet<TitlePersonsInMovie> TitlePersonsInMovies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //genre
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<Genre>().Property(x => x.Id).HasColumnName("genre_id");
            modelBuilder.Entity<Genre>().Property(x => x.Name).HasColumnName("genre_name");
            
            //genre
            modelBuilder.Entity<Person_Rating>().ToTable("person_rating");
            modelBuilder.Entity<Person_Rating>().Property(x => x.Id).HasColumnName("person_id");
            modelBuilder.Entity<Person_Rating>().Property(x => x.PersonRating).HasColumnName("person_rating");
            
            //Title_genre
            modelBuilder.Entity<Title_Genre>().ToTable("title_genre");
            modelBuilder.Entity<Title_Genre>().Property(x => x.Id).HasColumnName("title_genre_id");
            modelBuilder.Entity<Title_Genre>().Property(x => x.GenreId).HasColumnName("genre_id");
            modelBuilder.Entity<Title_Genre>().Property(x => x.TitleId).HasColumnName("title_id");
            
            modelBuilder.Entity<Title_Person>().ToTable("title_person");
            modelBuilder.Entity<Title_Person>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Title_Person>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<Title_Person>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Title_Person>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<Title_Person>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<Title_Person>().Property(x => x.Job).HasColumnName("job");
            modelBuilder.Entity<Title_Person>().Property(x => x.Character).HasColumnName("character");
            
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(x => x.Surname).HasColumnName("surname");
            modelBuilder.Entity<User>().Property(x => x.Lastname).HasColumnName("last_name");
            modelBuilder.Entity<User>().Property(x => x.Age).HasColumnName("age");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
            
            //Title_bookmark
            modelBuilder.Entity<Title_Bookmark>().ToTable("title_bookmarks");
            modelBuilder.Entity<Title_Bookmark>().Property(x => x.Id).HasColumnName("bookmark_id");
            modelBuilder.Entity<Title_Bookmark>().Property(x => x.ListId).HasColumnName("list_id");
            modelBuilder.Entity<Title_Bookmark>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Title_Bookmark>().HasOne(x => x.Title).WithMany(c => c.TitleBookmarks)
                .HasForeignKey(v => v.TitleId);
            modelBuilder.Entity<Title_Bookmark>().HasOne(x => x.TitleBookmarkList)
                .WithMany(c => c.TitleBookmarks)
                .HasForeignKey(v => v.ListId);
            
            //Title_bookmark_list
            modelBuilder.Entity<Title_Bookmark_List>().ToTable("title_bookmark_list");
            modelBuilder.Entity<Title_Bookmark_List>().Property(x => x.Id).HasColumnName("list_id");
            modelBuilder.Entity<Title_Bookmark_List>().Property(x => x.UserId).HasColumnName("user_id");
            modelBuilder.Entity<Title_Bookmark_List>().Property(x => x.ListName).HasColumnName("list_name");
            modelBuilder.Entity<Title_Bookmark_List>().HasMany(x => x.TitleBookmarks)
                .WithOne(c => c.TitleBookmarkList)
                .HasForeignKey(v => v.ListId);
            
            //Title_episode
            modelBuilder.Entity<Title_Episode>().ToTable("title_episodes");
            modelBuilder.Entity<Title_Episode>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Title_Episode>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Title_Episode>().Property(x => x.ParentId).HasColumnName("parent_id");
            modelBuilder.Entity<Title_Episode>().Property(x => x.SeasonNumber).HasColumnName("season_number");
            modelBuilder.Entity<Title_Episode>().Property(x => x.EpisodeNumber).HasColumnName("episode_number");
            modelBuilder.Entity<Title_Episode>()
                .HasOne(x => x.Title)
                .WithOne(x => x.TitleEpisode);


            //Title_Search
            modelBuilder.Entity<Title_Search>().ToTable("title_search");
            modelBuilder.Entity<Title_Search>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<Title_Search>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<Title_Search>().Property(x => x.Field).HasColumnName("field");
            modelBuilder.Entity<Title_Search>().Property(x => x.Lexeme).HasColumnName("lexeme");
            
            // Title
            modelBuilder.Entity<Title>().ToTable("title");
            modelBuilder.Entity<Title>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<Title>().Property(x => x.TypeId).HasColumnName("type_id");
            modelBuilder.Entity<Title>().Property(x => x.PrimaryTitle).HasColumnName("primary_title");
            modelBuilder.Entity<Title>().Property(x => x.OriginalTitle).HasColumnName("original_title");
            modelBuilder.Entity<Title>().Property(x => x.IsAdult).HasColumnName("is_adult");
            modelBuilder.Entity<Title>().Property(x => x.StartYear).HasColumnName("start_year");
            modelBuilder.Entity<Title>().Property(x => x.EndYear).HasColumnName("end_year");
            modelBuilder.Entity<Title>().HasMany(x => x.PersonKnownTitles).WithOne(c => c.title)
                .HasForeignKey(v => v.TitleId);
            modelBuilder.Entity<Title>()
                .HasOne(x => x.TitleRuntime)
                .WithOne(o => o.Title)
                .HasForeignKey<TitleRuntime>(x => x.Id);
            modelBuilder.Entity<Title>()
                .HasOne(x => x.TitleEpisode)
                .WithOne(x => x.Title)
                .HasForeignKey<Title_Episode>(x => x.TitleId);

            // Title Runtime
            modelBuilder.Entity<TitleRuntime>().ToTable("title_runtime");
            modelBuilder.Entity<TitleRuntime>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<TitleRuntime>().Property(x => x.Runtime).HasColumnName("runtime_minut");
            
            //Type
            modelBuilder.Entity<TitleType>().ToTable("type");
            modelBuilder.Entity<TitleType>().Property(x => x.Id).HasColumnName("type_id");
            modelBuilder.Entity<TitleType>().Property(x => x.TypeName).HasColumnName("type_name");
            
            //User
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
            modelBuilder.Entity<User>().Property(x => x.Surname).HasColumnName("surname");
            modelBuilder.Entity<User>().Property(x => x.Lastname).HasColumnName("last_name");
            modelBuilder.Entity<User>().Property(x => x.Age).HasColumnName("age");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            
            //Rating
            modelBuilder.Entity<Rating>().ToTable("rating");
            modelBuilder.Entity<Rating>().Property(x => x.Id).HasColumnName("rating_id");
            modelBuilder.Entity<Rating>().Property(x => x.User_Id).HasColumnName("user_id");
            modelBuilder.Entity<Rating>().Property(x => x.Title_Id).HasColumnName("title_id");
            modelBuilder.Entity<Rating>().Property(x => x.Rating_).HasColumnName("rating");

            //Person
            modelBuilder.Entity<Person>().ToTable("person");
            modelBuilder.Entity<Person>().Property(x => x.Id).HasColumnName("person_id");
            modelBuilder.Entity<Person>().Property(x => x.Name).HasColumnName("primary_name");
            modelBuilder.Entity<Person>().Property(x => x.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<Person>().Property(x => x.DeathYear).HasColumnName("death_year");
            modelBuilder.Entity<Person>().HasMany(x => x.PersonKnownTitles)
                .WithOne(c => c.person)
                .HasForeignKey(v => v.Id);
            modelBuilder.Entity<Person>().HasMany(x => x.PersonBookmark)
                .WithOne(c => c.Persons)
                .HasForeignKey(v => v.Person_Id);

            //Person Bookmarks
            modelBuilder.Entity<Person_Bookmark>().ToTable("person_bookmarks");
            modelBuilder.Entity<Person_Bookmark>().Property(x => x.Id).HasColumnName("bookmark_id");
            modelBuilder.Entity<Person_Bookmark>().Property(x => x.List_Id).HasColumnName("list_id");
            modelBuilder.Entity<Person_Bookmark>().Property(x => x.Person_Id).HasColumnName("person_id"); 
            
            modelBuilder.Entity<Person_Bookmark>().HasOne(x => x.Persons)
                .WithMany(c => c.PersonBookmark)
                .HasForeignKey(v => v.Person_Id);
            
            modelBuilder.Entity<Person_Bookmark>().HasOne(x => x.PersonBookmarkList)
                .WithMany(c => c.PersonBookmarks)
                .HasForeignKey(v => v.List_Id);
            
            //Person Bookmarklist
            modelBuilder.Entity<Person_Bookmark_list>().ToTable("person_bookmark_list");
            modelBuilder.Entity<Person_Bookmark_list>().Property(x => x.Id).HasColumnName("list_id");
            modelBuilder.Entity<Person_Bookmark_list>().Property(x => x.UserId).HasColumnName("user_id");
            modelBuilder.Entity<Person_Bookmark_list>().Property(x => x.ListName).HasColumnName("list_name");
            modelBuilder.Entity<Person_Bookmark_list>().HasMany(x => x.PersonBookmarks)
                .WithOne(c => c.PersonBookmarkList)
                .HasForeignKey(v => v.List_Id);

            //Search History
            modelBuilder.Entity<Search_History>().ToTable("search_history");
            modelBuilder.Entity<Search_History>().Property(x => x.Id).HasColumnName("search_id");
            modelBuilder.Entity<Search_History>().Property(x => x.User_Id).HasColumnName("user_id");
            modelBuilder.Entity<Search_History>().Property(x => x.Search_Name).HasColumnName("search_name");
            modelBuilder.Entity<Search_History>().Property(x => x.Timestamp).HasColumnName("timestamp");

            //Akas_type
            modelBuilder.Entity<Akas_Type>().ToTable("akas_type");
            modelBuilder.Entity<Akas_Type>().Property(x => x.Id).HasColumnName("type_id");
            modelBuilder.Entity<Akas_Type>().Property(x => x.Name).HasColumnName("type_name");
            modelBuilder.Entity<Akas_Type>()
                .HasMany(x => x.Akases)
                .WithOne(x => x.AkasType)
                .HasForeignKey(x => x.Type_id);

            //Akas_Attribute
            modelBuilder.Entity<Akas_Attribute>().ToTable("akas_attribute");
            modelBuilder.Entity<Akas_Attribute>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Akas_Attribute>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Akas_Attribute>().Property(x => x.AttributeName).HasColumnName("attribute_name");
            
            //Akas
            modelBuilder.Entity<Akas>().ToTable("akas");
            modelBuilder.Entity<Akas>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Akas>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Akas>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<Akas>().Property(x => x.AkasName).HasColumnName("akas_name");
            modelBuilder.Entity<Akas>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<Akas>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<Akas>().Property(x => x.IsOriginalTitle).HasColumnName("is_original_title");
            modelBuilder.Entity<Akas>().Property(x => x.Type_id).HasColumnName("type");
            modelBuilder.Entity<Akas>()
                .HasOne(x => x.AkasType)
                .WithMany(x => x.Akases)
                .HasForeignKey(x => x.Type_id);
            
               
            
            //Person known title
            modelBuilder.Entity<Person_known_title>().ToTable("person_known_title");
            modelBuilder.Entity<Person_known_title>().Property(x => x.Id).HasColumnName("person_id");
            modelBuilder.Entity<Person_known_title>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Person_known_title>().HasKey(x => new {x.Id, x.TitleId});
            modelBuilder.Entity<Person_known_title>().HasOne(x => x.person).WithMany(c => c.PersonKnownTitles)
                .HasForeignKey(v => v.Id);
            modelBuilder.Entity<Person_known_title>().HasOne(x => x.title).WithMany(c => c.PersonKnownTitles)
                .HasForeignKey(v => v.TitleId);

            //Person_profession
            modelBuilder.Entity<Person_Profession>().ToTable("person_profession");
            modelBuilder.Entity<Person_Profession>().Property(x => x.Id).HasColumnName("person_profession_id");
            modelBuilder.Entity<Person_Profession>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<Person_Profession>().Property(x => x.ProfessionId).HasColumnName("profession_id");
            
            //Profession
            modelBuilder.Entity<Profession>().ToTable("profession");
            modelBuilder.Entity<Profession>().Property(x => x.Id).HasColumnName("profession_id");
            modelBuilder.Entity<Profession>().Property(x => x.ProfessionName).HasColumnName("profession_name");
            
            //title_rating
            modelBuilder.Entity<Title_Rating>().ToTable("title_rating");
            modelBuilder.Entity<Title_Rating>().Property(x => x.Id).HasColumnName("title_rating_id");
            modelBuilder.Entity<Title_Rating>().Property(x => x.Title_Id).HasColumnName("title_id");
            modelBuilder.Entity<Title_Rating>().Property(x => x.Average_Rating).HasColumnName("average_rating");
            modelBuilder.Entity<Title_Rating>().Property(x => x.Num_Votes).HasColumnName("num_votes");
            
            // omdb_data
            modelBuilder.Entity<OmdbData>().ToTable("omdb_data");
            modelBuilder.Entity<OmdbData>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<OmdbData>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<OmdbData>().Property(x => x.Awards).HasColumnName("awards");
            modelBuilder.Entity<OmdbData>().Property(x => x.Plot).HasColumnName("plot");
            modelBuilder.Entity<OmdbData>()
                .HasOne(x => x.Title)
                .WithOne(x => x.OmdbData)
                .HasForeignKey<Title>();
            
            //Top10PosterForHome
            modelBuilder.Entity<TopPoster>().HasNoKey();
            modelBuilder.Entity<TopPoster>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<TopPoster>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<TopPoster>().Property(x => x.Awards).HasColumnName("awards");
            modelBuilder.Entity<TopPoster>().Property(x => x.Plot).HasColumnName("plot");
            
            //Movies for moviepage
            modelBuilder.Entity<Movies>().HasNoKey();
            modelBuilder.Entity<Movies>().Property(x => x.title_id).HasColumnName("title_id");
            modelBuilder.Entity<Movies>().Property(x => x.title_name).HasColumnName("title_name");
            modelBuilder.Entity<Movies>().Property(x => x.poster).HasColumnName("poster");
            modelBuilder.Entity<Movies>().Property(x => x.plot).HasColumnName("plot");
            modelBuilder.Entity<Movies>().Property(x => x.votes).HasColumnName("votes");
            modelBuilder.Entity<Movies>().Property(x => x.rating).HasColumnName("rating");
            modelBuilder.Entity<Movies>().Property(x => x.type).HasColumnName("type");
            
            //Movies for personwithprofession
            modelBuilder.Entity<PersonWithProfession>().HasNoKey();
            modelBuilder.Entity<PersonWithProfession>().Property(x => x.person_id).HasColumnName("person_id");
            modelBuilder.Entity<PersonWithProfession>().Property(x => x.primary_name).HasColumnName("primary_name");
            modelBuilder.Entity<PersonWithProfession>().Property(x => x.birth_year).HasColumnName("birth_year");
            modelBuilder.Entity<PersonWithProfession>().Property(x => x.death_year).HasColumnName("death_year");
            
            //Persons for movies
            modelBuilder.Entity<TitlePersonsInMovie>().HasNoKey();
            modelBuilder.Entity<TitlePersonsInMovie>().Property(x => x.Name).HasColumnName("name");
            modelBuilder.Entity<TitlePersonsInMovie>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePersonsInMovie>().Property(x => x.Character).HasColumnName("moviecharacter");

            
            



            base.OnModelCreating(modelBuilder);
        }
    }
}