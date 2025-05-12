using DS2ConsoleApp.Orm;
using DS2ConsoleApp.Orm.Dao;
using DS2ConsoleApp.Orm.Dto;

namespace DS2ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new Database();
            db.Connect();

            //Media video = new()
            //{
            //    Url = $"https://example.com/video_{Random.Shared.Next()}.mp4",
            //    Type = MediaType.Video,
            //    Data = File.ReadAllBytes("dog.jpg"),
            //    UploadDate = DateTime.UtcNow
            //};

            //MediaDao.InsertMedia(db, video);
            //Console.WriteLine(video);

            foreach (var u in UserDao.GetAllUsers(db))
                Console.WriteLine(u);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var traUsers = UserDao.GetUsersByName(db, "TRA0163");
            foreach (var u in traUsers)
                Console.WriteLine(u);

            var myUser = traUsers.First();
            var channels = ChannelDao.GetChannelsByUser(db, myUser.UserId);

            foreach (var c in channels)
            {
                Console.WriteLine(c);
                var playlists = PlaylistDao.GetPlaylistsByChannel(db, c.ChannelId);
                foreach (var pl in playlists)
                {
                    Console.WriteLine(pl);
                }
            }

            //Playlist playlist = new ()
            //{
            //    UserId = null,
            //    ChannelId = 13,
            //    Title = "MyChannel111",
            //    Visibility = Visibility.Public,
            //    CreationDate = DateTime.UtcNow,
            //};

            //PlaylistDao.InsertPlaylist(db, playlist);


            var parameters = new TransactionDao.FinishVideoUploadParameters()
            {
                ChannelId = 12, 
                Title = "My transaction video 1", 
                Description = "My description", 
                Visibility = Visibility.Public,
                IsMonetized = true,
                Duration = 420,
                ThumbnailMediaId = 35,
                VideoMediaId = 11,
                PlaylistIds = [14, 15, 16],
                CategoryIds = [1,2,3,4,5],
                Chapters = [
                    new () { Title = "Intro", StartTime = 0 },
                    new () { Title = "Main", StartTime = 20 },
                    new () { Title = "Summary", StartTime = 350 },
                    new () { Title = "Outro", StartTime = 400 },
                ]
            };

#if false
            var insertedVideoId = TransactionDao.FinishVideoUpload(db, parameters);
            Console.WriteLine($"Inserted Video Id: {insertedVideoId}");
#else
            var insertedVideoId = TransactionDao.SP_FinishVideoUpload(db, parameters);
            Console.WriteLine($"Inserted Video Id: {insertedVideoId}");
#endif


            db.Close();
        }
    }
}
