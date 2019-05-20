using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Store.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoreContext(
                serviceProvider.GetRequiredService<DbContextOptions<StoreContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                     new Movie
                     {
                         Title = "Blade Runner 2049",
                         ReleaseDate = DateTime.Parse("2017-10-3"),
                         Genre = "Sci-Fi",
                         Price = 60M,
                         Description = "Thirty years after the events of the first film, a new blade runner, LAPD Officer K (Ryan Gosling), unearths a long-buried secret that has the potential to plunge what's left of society into chaos. K's discovery leads him on a quest to find Rick Deckard (Harrison Ford), a former LAPD blade runner who has been missing for 30 years.",
                         ImageName = "Blade Runner 2049.jpg"
                     },

                     new Movie
                     {
                         Title = "Ghostbusters",
                         ReleaseDate = DateTime.Parse("1984-3-13"),
                         Genre = "Comedy",
                         Price = 50M,
                         Description = "Peter Venkman, Ray Stantz and Egon Spengler work at Columbia University. where they delve into the paranormal and fiddle with many unethical experiments on their students. As they are kicked out of the University, they really understand the paranormal and go into business for themselves. Under the new snazzy business name of 'Ghostbusters', and living in the old firehouse building they work out of, they are called to rid New York City of paranormal phenomenon at everyone's whim. - for a price. They make national press as the media reports the Ghostbusters are the cause of it all. ",
                         ImageName = "Ghostbusters.jpg"
                     },

                     new Movie
                     {
                         Title = "La La Land",
                         ReleaseDate = DateTime.Parse("2017-1-12"),
                         Genre = "Drama",
                         Price = 75M,
                         Description = "Aspiring actress serves lattes to movie stars in between auditions and jazz musician Sebastian scrapes by playing cocktail-party gigs in dingy bars. But as success mounts, they are faced with decisions that fray the fragile fabric of their love affair, and the dreams they worked so hard to maintain in each other threaten to rip them apart.",
                         ImageName = "La La Land.jpg"
                     },

                     new Movie
                     {
                         Title = "Vanilla Sky",
                         ReleaseDate = DateTime.Parse("2002-1-24"),
                         Genre = "Mystery",
                         Price = 80M,
                         Description = "Incarcerated and charged with murder, David Aames Jr. is telling the story of how he got to where he is to McCabe, the police psychologist. That story includes: being the 51% shareholder of a major publishing firm, which he inherited from his long deceased parents; the firm's board, appointed by David Aames Sr., being the 49% shareholders who would probably like to see him gone as they see him as being too irresponsible and immature to run the company; his best bro friendship with author, Brian Shelby; his \"friends with benefits\" relationship with Julie Gianni, who saw their relationship in a slightly different light; his budding romance with Sofia Serrano, who Brian brought to David's party as his own date and who Brian saw as his own possible life mate; and being in an accident which disfigured his face and killed the person who caused the accident. But as the story proceeds, David isn't sure what is real and what is a dream/nightmare as many facets of the story are incompatible to ...",
                         ImageName = "Vanilla Sky.jpg"
                     },

                     new Movie
                     {
                         Title = "Tron",
                         ReleaseDate = DateTime.Parse("1982-9-07"),
                         Genre = "Adventure",
                         Price = 60M,
                         Description = "Hacker/arcade owner Kevin Flynn is digitally broken down into a data stream by a villainous software pirate known as Master Control and reconstituted into the internal, 3-D graphical world of computers. It is there, in the ultimate blazingly colorful, geometrically intense landscapes of cyberspace, that Flynn joins forces with Tron to outmaneuver the Master Control Program that holds them captive in the equivalent of a gigantic, infinitely challenging computer game.",
                         ImageName = "Tron.jpg"
                     },

                     new Movie
                     {
                         Title = "The Amazing Spider-Man",
                         ReleaseDate = DateTime.Parse("2012-7-5"),
                         Genre = "Action",
                         Price = 65M,
                         Description = "Peter Parker (Garfield) is an outcast high schooler who was abandoned by his parents as a boy, leaving him to be raised by his Uncle Ben (Sheen) and Aunt May (Field). Like most teenagers, Peter is trying to figure out who he is and how he got to be the person he is today. Peter is also finding his way with his first high school crush, Gwen Stacy (Stone), and together, they struggle with love, commitment, and secrets. As Peter discovers a mysterious briefcase that belonged to his father, he begins a quest to understand his parents' disappearance - leading him directly to Oscorp and the lab of Dr. Curt Connors (Ifans), his father's former partner.",
                         ImageName = "The Amazing Spider-Man.jpg"
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
