using System.Text.Encodings.Web;
using System.Text.Json;

namespace GospodaWiki.Helper
{
    public class SerializeHelper
    {
        public SerializeHelper()
        {
            
        }
        public static object SerializeObject(object obj)
        {
            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            return json;
        }
    }
}
