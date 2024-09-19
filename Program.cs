using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Runtime;
using System.Globalization;

namespace Authentication1
{
    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();
        static async Task Main()
        {
            ISettings settings = new Settings();
            Employees? employees;

            string url = $"{settings.Protocol}://{settings.Hostname}/wfo/user-mgmt-api/v1/employees";
            string method = "GET";

            VerintAuthentication verint = new VerintAuthentication(DateTime.UtcNow, method, url, settings.APIKeyID, settings.APIKey);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", verint.AuthId + " " + verint.AuthHeader);

            
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                using HttpResponseMessage response = await client.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {

                    employees = await response.Content.ReadFromJsonAsync<Employees>(jsonOptions!);

                    if (employees is not null)
                    {
                        foreach (var employee in employees.data!)
                        {
                            DateTime startDate = employee.attributes.startTime;

                            Console.WriteLine("id=" + employee.id +
                                              " qmAnalyticsId=" + employee.attributes.qmAnalyticsId +
                                              " startDate=" + startDate.ToString("yyyy-MM-dd") +
                                              " Name=" + employee.attributes.person.firstName + " " +
                                              employee.attributes.person.lastName);

                        }
                    }

                }
                else
                {
                    Console.WriteLine("ERROR: Http response code: " + response.StatusCode);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            
        }
    }
}
