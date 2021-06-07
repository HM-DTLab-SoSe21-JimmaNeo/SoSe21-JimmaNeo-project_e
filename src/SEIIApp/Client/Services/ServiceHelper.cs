using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SEIIApp.Client.Services
{
    public static class ServiceHelper
    {

        public static async Task<T> DeserializeResponseContent<T>(this HttpResponseMessage message)
        {
            var responseBytes = await message.Content.ReadAsByteArrayAsync();
            var resultDto = JsonSerializer.Deserialize<T>(responseBytes, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return resultDto;
        }


    }
}