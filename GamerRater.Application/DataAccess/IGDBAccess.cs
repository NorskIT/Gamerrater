﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;
using GamerRater.Model;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

namespace GamerRater.Application.DataAccess
{
    internal class IgdbAccess : IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();
        //TODO: DO MORE ERROR CHECKS
        public IgdbAccess()
        {
            _httpClient.DefaultRequestHeaders.Add("user-key", "d1f0748dd028fe160ba161dfd05fe3b1");
        }

        public async Task<GameRoot[]> GetCoversToGamesAsync(GameRoot[] games)
        {
                var results = await _httpClient.PostAsync(new Uri(BaseUri.IGDBCovers), new HttpStringContent(
                    "fields *;" +
                    "where id  = (" + BuildGameCoverIdString(games) + ");",
                    UnicodeEncoding.Utf8,
                    "application/json"));
                var jsonGame = await results.Content.ReadAsStringAsync();
                var coversArr = JsonConvert.DeserializeObject<GameCover[]>(jsonGame);
                foreach (var cover in coversArr)
                {
                    cover.url = "https:" + cover.url;
                    foreach (GameRoot game in games)
                        if (cover.id == game.Cover)
                            game.GameCover = cover;
                }

                return games;
        }

        /// <summary>Gets the games asynchronous.</summary>
        /// <param name="gameNames">The game names.</param>
        /// <returns></returns>
        public async Task<GameRoot[]> GetGamesAsync(string gameNames)
        {
            try
            {
                var results = await _httpClient.PostAsync(new Uri(BaseUri.IGDBGames), new HttpStringContent(
                    "fields *;" +
                    "search \"" + gameNames + "\"*;" +
                    "where cover != 0;" +
                    "limit 50;",
                    UnicodeEncoding.Utf8,
                    "application/json"));
                var jsonGame = await results.Content.ReadAsStringAsync();
                if (!results.IsSuccessStatusCode) return null;
                var gamesArr = JsonConvert.DeserializeObject<GameRoot[]>(jsonGame);
                return gamesArr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //TODO: Egen helper klasse?
        //Builds a Cover ID string compatible with the api query. Etc : (123, 432, 12994, 392)
        private static string BuildGameCoverIdString(GameRoot[] games)
        {
            var ids = "";
            var firstIterate = true;
            foreach (var game in games)
            {
                if (firstIterate)
                    ids += game.Cover;
                else
                    ids += ", " + game.Cover;
                firstIterate = false;

            }
            return ids;
        }

        //Builds a Platform ID string compatible with the api query. Etc : (123, 432, 12994, 392)
        private static string BuildPlatformIdString(GameRoot game)
        {
            var ids = "";
            var firstIterate = true;
            foreach (var id in game.PlatformsIds)
            {
                if (firstIterate)
                    ids += id;
                else
                    ids += ", " + id;
                firstIterate = false;

            }
            return ids;
        }
        
        public async Task<Platform[]> GetPlatformsAsync(GameRoot game)
        {
            var results = await _httpClient.PostAsync(new Uri(BaseUri.IGDBPlatforms), new HttpStringContent(
                "fields *;" +
                "where id = (" + BuildPlatformIdString(game) + ");",
                UnicodeEncoding.Utf8,
                "application/json"));
            var jsonGame = await results.Content.ReadAsStringAsync();
            var platforms = JsonConvert.DeserializeObject<Platform[]>(jsonGame);
            return platforms;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
