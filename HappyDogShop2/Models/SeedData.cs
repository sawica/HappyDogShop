      
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MvcMovie.Data;
    using System;
    using System.Data.Entity;
    using System.Linq;
    
    namespace HappyDogShop2.Models
    {
        public static class SeedData
        {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new MyDbContext(serviceProvider.GetRequiredService<DbContextOption<MyDbContext>>()))
                {
                    // Look for any movies.
                    if (context.Products.Any())
                    {
                        return;   // DB has been seeded
                    }
    
                    context.MediaTypes.AddRange(
                        new MediaType
                        {
                            Title = "szelki SLED",
                            ImagePath =
                                "https://happy-zoo.pl/userdata/public/assets//szelki%20sled%20nonstop%20dogwear.jpg"
                        }
                        new MediaType
                        {
                            Title = "Miska Bone kolor indygo duża TarHong",
                            ImagePath =
                                "https://www.zooglobe.pl/pol_pm_Miska-Bone-kolor-indygo-duza-2l-TarHong-141845_2.jpg"
                        }
                        new MediaType
                        {
                            Title = "O´Canis sucha karma premium, bezglutenowa dla psa z indykiem, kurczakiem, łososiem i czerwonym burakiem",
                            ImagePath =
                                "https://www.ocanis.pl/karmy-pieczone.html#"
                        }

                    );
                    context.Products.AddRange(
                        new Product
                        {
                            Name = "Szelki do biegania w zaprzęgu SLED",
                            Description =
                                "Szelki typu SLED dla psa zostały wykonane z wysokiej jakości taśmy. Szelki są bardzo trwałe i posiadają wszyte elementy odblaskowe, dzięki czemu Twój pies będzie widoczny. Szelki typu SLED są bardzo wygodne, dzięki miękkiej wyściółce wewnątrz taśmy. Materiał użyty do uszycia tych szelek jest oddychający, dzięki czemu Twój pupil będzie czuł się w nich komfortowo. Podszycie miękką wyściółką gwarantuje, że szelki nie będą powodowały otarć. ",
                            Price = (float) 79.99,
                            CategoryId = 28,
                            MediaTypeId = 2,
                        }
                        new Product
                        {
                            Name = "Miska Bone kolor indygo duża TarHong",
                            Description = "Miska dla psa wykonana z trwałej, odpornej na pęknięcia melaminy. Bardzo elegancka i estetyczna, będzie stanowiła ładny element dekoracyjny Twojego domu. Miska jest lekka. Posiada gumowe nóżki ograniczające przesuwanie miski po podłodze w czasie jedzenia. Wymiary: średnica 22cm, głębokość 7,4cm, pojemność 2l, Kolor: indygo" ,
                            Price = (float) 68.26,
                            CategoryId = 31,
                            MediaTypeId = 3,
                        }
                        new Product
                            {
                                Name = "O´Canis sucha karma premium, bezglutenowa dla psa z indykiem, kurczakiem, łososiem i czerwonym burakiem",
                                Description = "Podstawową i najwygodniejsza karmą dla psów jest karma sucha. Ta która produkujemy jest pieczona bo pieczenie w piecu jest tradycyjną metodą stosowaną przez ludzi od czasów historycznych i najlepiej pozwala zatrzymać prawdziwy smak mięsa a w składzie naszej karmy mięsa jest naprawdę sporo. Karma O’Canis jest produkowana w naszej fabryce pod nadzorem technologa, wg „rodzinnej” receptury jaka powstała na potrzeby naszego Scottie’go i jego psiego stada niemal 20 lat temu." ,
                                Price = (float) 18.50,
                                CategoryId = 5,
                                MediaTypeId = 4,
                            }
    
                    );
                    context.Sales.AddRange(
                        new Sale
                        {
                            Name = "Wielka wyprzedaz sezonu",
                            ValueInPercent = 10,
                            StartDate = new DateTime(2022/01/26),
                            EndDate = new DateTime(2022/02/10),
                        }
                        
                    );
                    context.SaveChanges();
                }
            }
        }
    }