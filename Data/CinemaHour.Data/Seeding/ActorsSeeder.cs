namespace CinemaHour.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CinemaHour.Data.Models;
    using CinemaHour.Data.Models.Enum;

    public class ActorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Actors.Any())
            {
                return;
            }

            var actors = new List<(string ImageUrl, string FirstName, string LastName, string Info, DateTime BirthDate, string Gender)>
            {
                ("https://upload.wikimedia.org/wikipedia/commons/8/80/Kevin_Hart_2014_%28cropped_2%29.jpg", "Kevin", "Hart", "Kevin Darnell Hart (born July 6, 1979) is an American stand-up comedian, actor, and producer. Born and raised in Philadelphia, Pennsylvania, Hart began his career by winning several amateur comedy competitions at clubs throughout New England, culminating in his first real break in 2001 when he was cast by Judd Apatow for a recurring role on the TV series Undeclared. The series lasted only one season, but he soon landed other roles in films such as Paper Soldiers (2002), Scary Movie 3 (2003), Soul Plane (2004), In the Mix (2005), and Little Fockers (2010).", DateTime.Parse("1979-06-07"), "Male"), // Added movie
                ("https://upload.wikimedia.org/wikipedia/commons/f/f1/Dwayne_Johnson_2%2C_2013.jpg", "Dwayne", "Johnson", "Dwayne Douglas Johnson (born May 2, 1972), also known by his ring name The Rock, is an American-Canadian actor, producer, businessman, and former professional wrestler and football player. He was a professional wrestler for the World Wrestling Federation (WWF, now WWE) for eight years prior to pursuing an acting career. His films have grossed over $3.5 billion in North America and over $10.5 billion worldwide, making him one of the highest-grossing box-office stars of all time.", DateTime.Parse("1972-05-02"), "Male"), // Added movie
                ("https://static.standartnews.com/storage/thumbnails/inner_article/5415/7380/9680/%D1%82%D1%85%D0%B0%D0%BD%D0%BA%D1%81.jpg", "Tom", "Hanks", "Thomas Jeffrey Hanks (born July 9, 1956) is an American actor and filmmaker. Known for both his comedic and dramatic roles, Hanks is one of the most popular and recognizable film stars worldwide, and is widely regarded as an American cultural icon. Hanks' films have grossed more than $4.9 billion in North America and more than $9.96 billion worldwide, making him the fifth-highest-grossing actor in North America.", DateTime.Parse("1956-07-09"), "Male"), // Added movie
                ("https://upload.wikimedia.org/wikipedia/commons/3/3b/Al_Pacino_in_2014.jpg", "Al", "Pacino", "Alfredo James Pacino (/pəˈtʃiːnoʊ/; Italian: [paˈtʃiːno]; born April 25, 1940) is an American actor and filmmaker. In a career spanning over five decades, he has received many awards and nominations, including an Academy Award, two Tony Awards and two Primetime Emmy Awards. He is one of the few performers to have received the Triple Crown of Acting. He has also been honored with the AFI Life Achievement Award, the Cecil B. DeMille Award, and the National Medal of Arts.", DateTime.Parse("1940-04-25"), "Male"), // Added movie
                ("https://upload.wikimedia.org/wikipedia/commons/c/ca/Denzel_Washington_cropped.jpg", "Denzel", "Washington", "Denzel Hayes Washington Jr. (born December 28, 1954) is an American actor, director, and producer. He has received two Golden Globe awards, one Tony Award, and two Academy Awards: Best Supporting Actor for the historical war drama film Glory (1989) and Best Actor for his role as corrupt detective Alonzo Harris in the crime thriller Training Day (2001). He is widely regarded as one of the greatest actors of his generation, and is considered an American cultural icon.", DateTime.Parse("1954-12-28"), "Male"), // Added movie
                ("https://cdn.britannica.com/05/156805-050-4B632781/Leonardo-DiCaprio-2010.jpg", "Leonardo", "DiCaprio", "Leonardo Wilhelm DiCaprio (/dɪˈkæprioʊ/, Italian: [diˈkaːprjo]; born November 11, 1974) is an American actor, producer, and environmentalist. He has often played unconventional parts, particularly in biopics and period films. As of 2019, his films have earned US$7.2 billion worldwide, and he has placed eight times in annual rankings of the world's highest-paid actors. His accolades include an Academy Award and three Golden Globe Awards.", DateTime.Parse("1974-11-11"), "Male"), // Added movie
                ("https://m.media-amazon.com/images/M/MV5BMTM0ODU5Nzk2OV5BMl5BanBnXkFtZTcwMzI2ODgyNQ@@._V1_.jpg", "Johnny", "Depp", "John Christopher Depp II (born June 9, 1963) is an American actor, producer, and musician. He has been nominated for 10 Golden Globe Awards, winning one for Best Actor for his performance of the title role in Sweeney Todd: The Demon Barber of Fleet Street (2008), and has been nominated for three Academy Awards for Best Actor, among other accolades. He is regarded as one of the world's biggest film stars.", DateTime.Parse("1963-06-09"), "Male"), // Added movie
                ("https://upload.wikimedia.org/wikipedia/commons/6/60/Scarlett_Johansson_by_Gage_Skidmore_2_%28cropped%29.jpg", "Scarlet", "Johansson", "Scarlett Ingrid Johansson (/dʒoʊˈhænsən/; born November 22, 1984) is an American actress and singer. The world's highest-paid actress since 2018, she has made multiple appearances in the Forbes Celebrity 100. Her films have grossed over $14.3 billion worldwide, making Johansson the third-highest-grossing box office star of all time. She is the recipient of numerous accolades, including a Tony Award and a BAFTA Award, as well as nominations for two Academy Awards and five Golden Globe Awards.", DateTime.Parse("1984-11-22"), "Female"), // Added movie
            };

            foreach (var actor in actors)
            {
                dbContext.Actors.Add(new Actor
                {
                    ImageUrl = actor.ImageUrl,
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    BirthDate = actor.BirthDate,
                    Info = actor.Info,
                    Gender = Enum.Parse<Gender>(actor.Gender),
                });
            }
        }
    }
}
