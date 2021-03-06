using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TreyFunctionsAPI
{
    public static class AddContact
    {
        [FunctionName("AddContact")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "AddContact")] HttpRequest req, ILogger log)
        {
            try
            {
                string body = await new StreamReader(req.Body).ReadToEndAsync();
                var Contact = JsonConvert.DeserializeObject<Contacts>(body as string);

                var ContactDatabase = new Database();

                ContactDatabase.Contacts.Add(Contact);

                await ContactDatabase.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception)
            {

                return new BadRequestResult();
            }


        }
    }
}
