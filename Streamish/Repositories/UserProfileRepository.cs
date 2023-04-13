using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Streamish.Models;
using Streamish.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Streamish.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        

        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT Id, Name, Email, ImageUrl, DateCreated
                            FROM UserProfile
                        ORDER BY DateCreated
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            userProfiles.Add(new UserProfile()
                            {
                                //What is DbUtils? It's not a part of .NET. It's a class we created in order help
                                //simplify some of our database interaction code, particularly with regards to dealing
                                //with null values. You might have noticed that the Email column in the UserProfile table
                                //is nullable, so we could use the help.
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),

                            });
                        }

                        return userProfiles;
                    }
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT v.Name, v.Email, v.ImageUrl, v.DateCreated, up.Name
                            FROM UserProfile up
                            
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        UserProfile UserProfile = null;
                        if (reader.Read())
                        {
                           UserProfile userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                            };                                                   
                        }

                        return UserProfile;
                    }
                }
            }
        }

        public void Add(UserProfile UserProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Name, Email, ImageUrl, DateCreated)
                                        OUTPUT INSERTED.ID
                                        VALUES (@name, @email, @imageUrl, @dateCreated)";

                    DbUtils.AddParameter(cmd, "@Name", UserProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", UserProfile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", UserProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", UserProfile.ImageUrl);

                    UserProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile UserProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Email = @Email,
                               DateCreated = @DateCreated,
                               ImageUrl = @ImageUrl,
                          
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", UserProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", UserProfile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", UserProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", UserProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Id", UserProfile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM UserProfile WHERE Id = @Id;
                        DELETE FROM Video WHERE UserProfileId = @Id;
                        DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
       
        public UserProfile GetByIdWithVideos(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT v.Name, v.Email, v.ImageUrl, v.DateCreated, up.Name
                            FROM UserProfile up
                            
                           WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        UserProfile UserProfile = null;
                        if (reader.Read())
                        {
                            UserProfile userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                            };
                        }

                        return UserProfile;
                    }
                }
            }
        }
    }
}
