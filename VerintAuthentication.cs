using System.ComponentModel;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Authentication1;

internal class VerintAuthentication
{
    private readonly DateTime _now;
    private readonly Uri _uri;
    private readonly string _method;
    private readonly string _issuedAt;
    private readonly string _salt;
    private readonly string _apiKApiKeyId;
    private readonly string _apiKey;

    public VerintAuthentication(DateTime now, string method, string url, string apiKApiKeyId, string apiKey)
    {
        _now = now;
        _method = method;
        _uri = new Uri(url);
        _issuedAt = CalculateIssuedAt();
        _salt = CalculateSalt();
        _apiKApiKeyId = apiKApiKeyId;
        _apiKey = apiKey;
    }

    //String prefix
    private const string VerintAuthId = "Vrnt-1-HMAC-SHA256";

    private string CalculateIssuedAt()
    {
        // returns ISO8601 UTC e.g. 2024-09-23T09:25:46Z
        return _now.ToString("yyyy-MM-dd") + "T" + _now.ToString("HH:mm:ss") + "Z";
    }

    private string CalculateSalt()
    {
        const int RANDOMSIZE = 16;
        byte[] randomSaltBytes = new byte[RANDOMSIZE];
        
        //Fill byte array with random numbers
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomSaltBytes);
        }
        
        return Convert.ToBase64String(randomSaltBytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    
    private string CalculateStringSignature()
    {
        // Pad == to the right to align and convert from Bast64String
        string keyForHash = _apiKey.PadRight(_apiKey.Length + (4 - (_apiKey.Length % 4)), '=').Replace('-', '+').Replace('_', '/');

        Console.WriteLine(keyForHash);

        string stringToSign = CalculateStringToSign();

        return ComputeHmacSha256(keyForHash, stringToSign); ;
    }

    private string CalculateStringToSign()
    {
        return  _salt + "\n" +
                _method + "\n" +
                _uri.LocalPath + "\n" +
                _issuedAt + "\n\n";
    }

    private static string ComputeHmacSha256(string key, string message)
    {
        var keyBytes = Convert.FromBase64String(key);
        var messageBytes = Encoding.UTF8.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyBytes))
        {
            var hashmessage = hmacsha256.ComputeHash(messageBytes);

            return Convert.ToBase64String(hashmessage).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }
    }

    public string AuthId { get => VerintAuthId; }

    public string AuthHeader { get => CalculateAuthHeader(); }

    private string CalculateAuthHeader()
    {
        return "salt=" + _salt + "," +
               "iat=" + _issuedAt + "," + 
               "kid=" + _apiKApiKeyId + "," +
               "sig=" + CalculateStringSignature();
    }
}


