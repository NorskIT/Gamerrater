﻿using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GamerRater.Model;
using Newtonsoft.Json;

namespace GamerRater.Application.DataAccess
{
    class Games
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<GameRoot> GetGame(GameRoot game)
        {
            using (_httpClient)
            {
                var httpResponse = await _httpClient.GetAsync(new Uri(BaseUri.Games + game.Id));
                //var jsonCourses = await result.Content.ReadAsStringAsync();
                var jsonCourses = await httpResponse.Content.ReadAsStringAsync();
                var resultGame = JsonConvert.DeserializeObject<GameRoot>(jsonCourses);
                if (resultGame.Id != 0)
                    return resultGame;
                return null;
            }
            
        }
    }
}
