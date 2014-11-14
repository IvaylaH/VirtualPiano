namespace VirtualPiano.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using VirtualPiano.Common;
    using VirtualPiano.Models;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            //TODO: Set to false later on
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.SeedArtists(context);

            this.SeedMusicCategories(context);

            this.SeedMusicSheets(context);

            this.SeedRoles(context);

            var authorId = this.SeedInitialAdminAndUser(context);

            this.SeedAdCategories(context);

            this.SeedMusicAds(context, authorId);
        }

        private void SeedAdCategories(ApplicationDbContext context)
        {
            if (context.MusicAds.Any())
            {
                return;
            }

            var categories = new List<AdCategory>()
            {
                new AdCategory()
                {
                    Name = "Auditions"
                },
                new AdCategory()
                {
                    Name = "Musical instruments"
                },
                new AdCategory()
                {
                    Name = "Concerts"
                },
                new AdCategory()
                {
                    Name = "Bands"
                },
                new AdCategory()
                {
                    Name = "Musicians Wanted"
                }
            };

            context.AdCategories.AddOrUpdate(categories.ToArray());
            context.SaveChanges();
        }

        private void SeedMusicAds(ApplicationDbContext context, string userId)
        {
            if (context.MusicAds.Any())
            {
                return;
            }

            var authorId = context.Users.Where(user => user.Id == userId).Select(u => u.Id).First();
            var categoryId = context.AdCategories.Where(cat => cat.Id == 1).Select(u => u.Id).First();

            var musicAds = new List<MusicAd>()
            {
                new MusicAd()
                {
                    Title = "MusicCalling - Phantom of the Opera",
                    Content = "We are currently looking for singers professionals and otherwise for a production of Andrew Lloyd Webber's \"Phantom of the Opera\"",
                    Status = RequestStatus.Approved,
                    AuthorId = authorId,
                    CategoryId = categoryId
                },
                new MusicAd()
                {
                    Title = "MusicCalling - Dr. Horrible's Sing-along Blog",
                    Content = "We are currently looking for singers professionals and otherwise for a production of \"Dr. Horrible's Sing-along Blog\"",
                    Status = RequestStatus.Approved,
                    AuthorId = authorId,
                    CategoryId = categoryId
                },
                new MusicAd()
                {
                    Title = "MusicCalling - Sweety Todd",
                    Content = "We are currently looking for singers professionals and otherwise for a production of \"Sweety Todd\"",
                    Status = RequestStatus.Approved,
                    AuthorId = authorId,
                    CategoryId = categoryId
                },
                new MusicAd()
                {
                    Title = "MusicCalling - Wicked",
                    Content = "We are currently looking for singers professionals and otherwise for a production of \"Wicked\"",
                    Status = RequestStatus.Approved,
                    AuthorId = authorId,
                    CategoryId = categoryId
                }
            };

            context.MusicAds.AddOrUpdate(musicAds.ToArray());
            context.SaveChanges();
        }

        private string SeedInitialAdminAndUser(ApplicationDbContext context)
        {
            string username = "admin@vp.com";
            string password = "admin@vp.com";
            string firstName = "AdminChe";
            string lastName = "AdminChe";

            string regUserUsername = "1@3.com";
            string regUserPass = "123456";
            string regUserFName = "1";
            string regUserLName = "@3";

            if (context.Users.Any(u => u.UserName == username))
            {
                return "";
            }

            var admin = new ApplicationUser()
            {
                UserName = username,
                Email = username,
                FirstName = firstName,
                LastName = lastName,
            };

            var user = new ApplicationUser()
            {
                UserName = regUserUsername,
                Email = regUserUsername,
                FirstName = regUserFName,
                LastName = regUserLName
            };

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!userManager.Users.Any(u => u.UserName == username))
            {
                userManager.Create(admin, password);
                userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
            }

            if (!userManager.Users.Any(u => u.UserName == regUserUsername))
            {
                userManager.Create(user, regUserPass);
                userManager.AddToRole(user.Id, GlobalConstants.RegularUserRoleName);
            }

            return user.Id.ToString();
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.RegularUserRoleName));
            context.SaveChanges();
        }

        private void SeedArtists(ApplicationDbContext context)
        {
            if (context.Artists.Any())
            {
                return;
            }

            var artists = new List<Artist>()
            {
                new Artist()
                {
                    Name = "The Beatles"
                },
                new Artist()
                {
                    Name = "John Legend"
                }
            };

            context.Artists.AddOrUpdate(artists.ToArray());
            context.SaveChanges();
        }

        private void SeedMusicCategories(ApplicationDbContext context)
        {
            if (context.MusicCategories.Any())
            {
                return;
            }

            var categories = new List<MusicSheetsCategory>()
            {
                new MusicSheetsCategory()
                {
                    Name = "Rock and Roll"
                },
                new MusicSheetsCategory()
                {
                    Name = "Pop"
                },
                new MusicSheetsCategory()
                {
                    Name = "Jazz",
                },
                new MusicSheetsCategory()
                {
                    Name = "Hip hop"
                },
                new MusicSheetsCategory()
                {
                    Name = "Classical"
                }
            };

            context.MusicCategories.AddOrUpdate(categories.ToArray());
            context.SaveChanges();
        }

        private void SeedMusicSheets(ApplicationDbContext context)
        {
            if (context.MusicSheets.Any())
            {
                return;
            }

            var musicSheets = new List<MusicSheet>
            {
                new MusicSheet()
                {
                    Title = "Hey Jude",
                    ArtistId = 1,
                    Notes = string.Format("{0} \r\n {1} \r\n {2} \r\n {3} \r\n {4} \r\n {5} \r\n {6} \r\n {7} \r\n {8} \r\n {9} \r\n {10}", 
                    "sp psdo opPggfsdsPp sdd dhgfgds iopds dsPuui", 
                    "sp psdo opPggfsdsPp sdd dhgfgds iopds dsPuui", 
                    "igddssPd gdgP gdsPs dsPpoi", 
                    "igddssPd gdgP gdsPs dsPpoi", 
                    "ipsdd ghjj", 
                    "sp psdo opPggfsdsPp sdd dhgfgds iopds dsPuui",
                    "sp psdo opPggfsdsPp sdd dhgfgds iopds dsPuui", 
                    "igddssPd gdgP gdsPs dsPpoi", 
                    "igddssPd gdgP gdsPs dsPpoi", 
                    "ipsdfghjklz", 
                    "i p s hghg hghg ds"),
                    CreatedOn = DateTime.Now,
                    CategoryId = 1
                },
                new MusicSheet()
                {
                    Title = "Yesterday",
                    ArtistId = 1,
                    Notes = string.Format("{0} \r\n {1} \r\n {2} \r\n {3} \r\n {4} \r\n {5} \r\n {6} \r\n {7} \r\n {8}", 
                        "oii paSdfgf dd ddsPpoP pp oipoyi pp",
                        "oii paSdfgf dd ddsPpoP pp oipoyi pp",
                        "p p dfgfd fdsd p",
                        "p p dfgfd fsPf g s P p",
                        "oii paSdfgf dd ddsPpoPpp oipoyi pp",
                        "p p dfgfd fdsd p",
                        "p p dfgfd fsPf g",
                        "oii paSdfgf dd ddsPpoPpp oipoyi pp",
                        "ipoyi pp"),
                    CreatedOn = DateTime.Now,
                    CategoryId = 1
                },
                new MusicSheet()
                {
                    Title = "All Of Me",
                    ArtistId = 2,
                    Notes = string.Format("{0} \r\n {1} \r\n {2} \r\n {3} \r\n {4}", 
                        "s -s -s S -S -S s -s -s P -P -P i P-s-s s-s-s s O O P-s-s s-P-s s-P-O-O-i s-s S s-O S s-O O-P",
                        "s-P i s-s-s s-P-s s-P-O-O s-s-s D-S-s s-P-O-O-i s-s S s-O O-S S s-O O-P s-P S s P-g",
                        "D S s s s-P-O-O o o-i-Y-Y i g g-D-D-S-s s-P-P-O-P s-D s-g s P O-s s-s P P P O-P-i s-s",
                        "P P P O-P-P s-s D s-g s-g-s P O-s s-s P P P O-P-i s-s P P P O-P-P s-s-S-D-H h g D",
                        "s-s P O-D-o s-s-S-D-H h g D s-s s-P i"),
                    CreatedOn = DateTime.Now,
                    CategoryId = 2
                },
            };

            context.MusicSheets.AddOrUpdate(musicSheets.ToArray());
            context.SaveChanges();
        }
    }
}
