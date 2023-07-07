using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace SurveyMonkey.WebApi.Extension
{
    public static class RequestHeader
    {
        public static string GetAuthorizationValues(this IHeaderDictionary headers,string jwt)
        {
            var token = headers.Authorization.ToString();
            int i = token.IndexOf(".");
            int j = token.IndexOf(".", i + 1);
            token = token.Substring(i + 1, j - i - 1);

            while (token.Length % 4 != 0)
            {
                token += "=";
            }
            
            var base64 = Convert.FromBase64String(token);
            var data = Encoding.UTF8.GetString(base64);
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            return dic[jwt];
        }

    }
}
