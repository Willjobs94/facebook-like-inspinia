using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using FacebookLikeInspinia.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FacebookLikeInspinia.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FacebookLikeInspinia.Models.FacebookLikeInspiniaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FacebookLikeInspiniaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var passwordHash = new PasswordHasher();
            var hashedPassword = passwordHash.HashPassword("MyPassword23@#");
            var initUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    FirstName = "Mark",
                    LastName = "Levy",
                    Email = "marklevy@email.com",
                    UserName = "marklevy@email.com",
                    PasswordHash = hashedPassword,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    About =
                        "Mark Levy is the founder of Levy Innovation, a marketing strategy firm. David Meerman Scott has called him “a positioning guru extraordinaire,” and Debbie Weil referred to him as “a horse whisperer for writers and business thinkers.” He has written for The New York Times, and has written or co-created five books. His latest is a revised, expanded, and re-subtitled edition of his bestseller, Accidental Genius: Using Writing to Generate Your Best Ideas, Insight, and Content. Mark also creates magic tricks and shows. His work has been performed Off-Broadway, in Las Vegas, and on all the major television networks. Visit him and read his blog at levyinnovation.com.”“Mark Levy is the founder of Levy Innovation, a marketing strategy firm. David Meerman Scott has called him “a positioning guru extraordinaire,” and Debbie Weil referred to him as “a horse whisperer for writers and business thinkers.” He has written for The New York Times, and has written or co-created five books. His latest is a revised, expanded, and re-subtitled edition of his bestseller, Accidental Genius: Using Writing to Generate Your Best Ideas, Insight, and Content. Mark also creates magic tricks and shows. His work has been performed Off-Broadway, in Las Vegas, and on all the major television networks. Visit him and read his blog at levyinnovation.com."
                },
                new ApplicationUser
                {
                    FirstName = "Ann",
                    LastName = "Handley ",
                    Email = "ann.handley@email.com",
                    UserName = "ann.handley@email.com",
                    PasswordHash = hashedPassword,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    About = "Ann Handley is a veteran of creating and managing digital content to build relationships for organizations and individuals. Ann is the author of the Wall Street Journal bestseller  Everybody Writes: Your Go-To Guide to Creating Ridiculously Good Content (September 2014, Wiley) and co-author of the best-selling book on content marketing, Content Rules: How to Create Killer Blogs, Podcasts, Videos, Ebooks, Webinars (and More) That Engage Customers and Ignite Your Business (2011, Wiley). She is the Chief Content Officer of MarketingProfs; a LinkedIn Influencer; a keynote speaker, mom, and writer."
                    },
                new ApplicationUser
                {
                    FirstName = "William",
                    LastName = "Bello ",
                    Email = "william.bello@email.com",
                    UserName = "william.bello@email.com",
                    PasswordHash = hashedPassword,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    About = "He is the very cool, very chiseled face of Ketto. He doesn't really need an introduction but we enjoy talking about him, so we're going to. He's the Co-Founder and a brilliant film and theatre actor. He's also super clever. And he has a heart of pure, sunshiny gold. Our only complaint is that he's too busy doing good to shoot the breeze with us."

                },
                new ApplicationUser
                {
                    FirstName = "Jose ",
                    LastName = "Armando",
                    Email = "jose.armando@emtail.com",
                    UserName = "jose.armando@emtail.com",
                    PasswordHash = hashedPassword,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    About = "He is the high-tech Co-Founder and CTO. He's the master of cool and a bundle of energy. Thankfully, he expends some of it at the gym before jogging to work. We love him because he does proxy workouts for us too. He also has a wicked sense of humour. So if you want a good laugh, his number is...er, oh, he's right here. Shhh...tell you later!"
                }
            };

           

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                initUsers.ForEach(u =>
                {
                    context.Users.AddOrUpdate(x => x.Email, u);
                    if (!context.Users.Any(x => x.Email == u.Email))
                    {
                        userManager.Create(u);
                    }
                });
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add($"{DateTime.Now}: Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                //Write to file
                //System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                //throw;

                // Showing it on screen
                throw new Exception(string.Join(",", outputLines.ToArray()));

            }


        }
    }
}

// Use the following statements to delete current database and create new one
/*
 * 1) Open nuget package console and then use this command
 *
 * sqllocaldb.exe stop
 *
 * 2) Then after we successfully stop the localDB service we have to delete it
 *
 * sqllocaldb.exe delete
 *
 * 3) If a you changed the current model (if not jump to step 4) do this: 
 *
 * Add-Migration InitialDb
 *
 * 4) Then apply InitialDb migration to database by using this command:
 *
 * Update-database
 */
