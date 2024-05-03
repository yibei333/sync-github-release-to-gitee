using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SyncGithubReleaseToGitee.Extensions
{
    public static class JsonExtension
    {
        public static T Deserialize<T>(this string json) where T : class
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                using (var stream = new MemoryStream(jsonBytes))
                {
                    var deserialized = (T)serializer.ReadObject(stream);
                    return deserialized;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"deserialize failed:{ex.Message}", ex);
            }
        }

        public static string Serialize<T>(this T obj) where T : class
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, obj);
                    string json = Encoding.UTF8.GetString(stream.ToArray());
                    return json;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"serialize failed:{ex.Message}", ex);
            }
        }
    }
}
