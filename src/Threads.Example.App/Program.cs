﻿using Threads.Api;

namespace Threads.Example.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Threads.Net API Example");

            IThreadsApi api = new ThreadsApi(new HttpClient());

            var username = "tidusjar";

            var userId = await api.GetUserIdFromUserNameAsync(username);
            Console.WriteLine(userId);
            var userProfile = await api.GetUserProfileAsync(username, userId);

            Console.WriteLine(userProfile?.UserName);

            var threads = await api.GetThreadsAsync(username, userId);

            var replies = await api.GetUserRepliesAsync(username, userId);

            var token = await api.GetTokenAsync("tidusjar", "pass");

        }
    }
}