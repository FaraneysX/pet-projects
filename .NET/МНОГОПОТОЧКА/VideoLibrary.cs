using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    public class VideoLibrary
    {
        public Friend Peter { get; set; }
        public Friend Ivan { get; set; }
        public Friend Anton { get; set; }
        public Friend Dmitriy { get; set; }

        private readonly Thread thread;
        private static readonly Random random = new Random();

        public VideoLibrary()
        {
            Peter = new Friend()
            {
                Name = "Петр",
                Discs = new Queue<Disk>(new[]
                {
                    new Disk
                    {
                        Name = "The Shawshank Redemption: Special Edition",
                        Type = Disk.DiskFormat.CD,
                        Film = new Film
                        {
                            Name = "The Shawshank Redemption",
                            Genre = new Genre("Drama"),
                            Performer = new Performer("Tim Robbins")
                        }
                    },

                    new Disk
                    {
                        Name = "Inception: Director's Cut",
                        Type = Disk.DiskFormat.DVD,
                        Film = new Film
                        {
                            Name = "Inception",
                            Genre = new Genre("Sci-Fi"),
                            Performer = new Performer("Leonardo DiCaprio")
                        }
                    }
                })
            };

            Ivan = new Friend()
            {
                Name = "Иван",
                Discs = new Queue<Disk>(new[]
                {
                    new Disk
                    {
                        Name = "Pulp Fiction: Collector's Edition",
                        Type = Disk.DiskFormat.BlueRay,
                        Film = new Film
                        {
                            Name = "Pulp Fiction",
                            Genre = new Genre("Crime"),
                            Performer = new Performer("John Travolta")
                        }
                    },

                    new Disk
                    {
                        Name = "The Dark Knight Trilogy: Ultimate Box Set",
                        Type = Disk.DiskFormat.DVD,
                        Film = new Film
                        {
                            Name = "The Dark Knight Trilogy",
                            Genre = new Genre("Action"),
                            Performer = new Performer("Christian Bale")
                        }
                    }
                })
            };

            Anton = new Friend()
            {
                Name = "Антон",
                Discs = new Queue<Disk>(new[]
                {
                    new Disk
                    {
                        Name = "The Matrix Reloaded: Limited Edition",
                        Type = Disk.DiskFormat.CD,
                        Film = new Film
                        {
                            Name = "The Matrix Reloaded",
                            Genre = new Genre("Sci-Fi"),
                            Performer = new Performer("Keanu Reeves")
                        }
                    },

                    new Disk
                    {
                        Name = "Fight Club: Anniversary Edition",
                        Type = Disk.DiskFormat.BlueRay,
                        Film = new Film
                        {
                            Name = "Fight Club",
                            Genre = new Genre("Drama"),
                            Performer = new Performer("Brad Pitt")
                        }
                    },
                })
            };

            Dmitriy = new Friend()
            {
                Name = "Дмитрий",
                Discs = new Queue<Disk>(new[]
                {
                    new Disk
                    {
                        Name = "Goodfellas: Remastered",
                        Type = Disk.DiskFormat.DVD,
                        Film = new Film
                        {
                            Name = "Goodfellas",
                            Genre = new Genre("Crime"),
                            Performer = new Performer("Robert De Niro")
                        }
                    },

                    new Disk
                    {
                        Name = "Star Wars: Episode IV - A New Hope: Special Edition",
                        Type = Disk.DiskFormat.CD,
                        Film = new Film
                        {
                            Name = "Star Wars: Episode IV - A New Hope",
                            Genre = new Genre("Sci-Fi"),
                            Performer = new Performer("Mark Hamill")
                        }
                    }
                })
            };

            thread = new Thread(Factory);
        }

        public void Start()
        {
            thread.Start();
        }

        private void Factory()
        {
            while (true)
            {
                Friend sender = GetRandomFriend();

                // Если у отправителя есть диски.
                if (sender.Discs != null && sender.Discs.Count > 0)
                {
                    Friend friend = GetRandomFriend();

                    if (sender != friend)
                    {
                        sender.Recipient = friend;
                        
                        sender.SendingDisk(sender.Discs.Dequeue());
                        Thread.Sleep(2200);
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private Friend GetRandomFriend()
        {
            Friend[] friends = { Peter, Ivan, Anton, Dmitriy };

            return friends[random.Next(friends.Length)];
        }
    }
}
