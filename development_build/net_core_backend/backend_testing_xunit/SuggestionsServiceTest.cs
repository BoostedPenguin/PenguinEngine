﻿using Microsoft.AspNetCore.Http;
using net_core_backend.Context;
using net_core_backend.Models;
using net_core_backend.Services;
using net_core_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backend_testing_xunit
{
    public class SuggestionsServiceTest : DatabaseSeeder
    {
        private readonly ISuggestionService service;

        public SuggestionsServiceTest(ISuggestionService service, IHttpContextAccessor http, IContextFactory factory) : base(http, factory)
        {
            this.service = service;
            this.http = http;
        }


        [Fact]
        public async Task MainAlgorithm()
        {
            CreateIdentity(Users[0].Auth);
            List<UserKeywords> words = new List<UserKeywords>()
            {
                new UserKeywords()
                {
                    UserId = Users[0].Id,
                    Keyword = "НЕГ Гьоте",
                    KeywordAddress = new KeywordAddress()
                    {
                        Country = "Bulgaria",
                        City = "Burgas",
                    }
                },
                new UserKeywords()
                {
                    UserId = Users[0].Id,
                    Keyword = "Съдебна палата",
                    KeywordAddress = new KeywordAddress()
                    {
                        Country = "Bulgaria",
                        City = "Sofia",
                    }
                },
                new UserKeywords()
                {
                    UserId = Users[0].Id,
                    Keyword = "Музей",
                    KeywordAddress = new KeywordAddress()
                    {
                        Country = "Netherlands",
                        City = "Eindhoven",
                    }
                },
                new UserKeywords()
                {
                    UserId = Users[0].Id,
                    Keyword = "philips museum",
                    KeywordAddress = new KeywordAddress()
                    {
                        Country = "Netherlands",
                        City = "Eindhoven",
                    }
                },
                new UserKeywords()
                {
                    UserId = Users[0].Id,
                    Keyword = "Музей",
                    KeywordAddress = new KeywordAddress()
                    {
                        Country = "Netherlands",
                        City = "Eindhoven",
                    }
                },
                new UserKeywords()
                {
                    UserId = Users[0].Id,
                    Keyword = "anne frank house",
                    KeywordAddress = new KeywordAddress()
                    {
                        Country = "Netherlands",
                        City = "Amsterdam",
                    }
                },
            };

            using(var a = factory.CreateDbContext())
            {
                await a.AddRangeAsync(words);
                await a.SaveChangesAsync();
            }


            //20-30ms on using already logged suggestions
            //5000ms on using new suggestions

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var suggestions = await service.Main();
            sw.Stop();

            Assert.Equal(8, suggestions.Length);
        }
    }
}