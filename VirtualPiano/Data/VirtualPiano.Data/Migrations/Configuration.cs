namespace VirtualPiano.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using VirtualPiano.Models;

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
        }

//        private void SeedFirstArtistAndCategoryAndMusicSheetsObjects
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
                    Notes = string.Format("{0} /r {1} /r {2} /r {3} /r {4} /r {5} /r {6} /r {7} /r {8} /r {9} /r {10}", 
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
                    Notes = string.Format("{0} /r {1} /r {2} /r {3} /r {4} /r {5} /r {6} /r {7} /r {8}", 
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
                    Notes = string.Format("{0} /r {1} /r {2} /r {3} /r {4}", 
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
