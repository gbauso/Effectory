using Bogus;
using Effectory.Core.Model;
using Effectory.Shared.JsonConfiguration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Effectory.Test.Helpers
{
    public class DataHelper
    {
        private static readonly JsonSerializerSettings settings =
            new JsonSerializerSettings()
                .WithPrivateSetterContractResolver();

        private static readonly Faker faker = new Faker();

        public static Questionnaire GetFakeQuestionnaire()
        {
            var json = File.ReadAllText(Path.Join(Directory.GetCurrentDirectory(), "base_data.json"));

            return JsonConvert.DeserializeObject<Questionnaire>(json, settings);
        }

        public static IDictionary<string, string> GetKeyValuePairs()
        {
            return new Dictionary<string, string>()
            {
                { faker.Locale, faker.Lorem.Words(3).ToString() }
            };
        }
    }
}
