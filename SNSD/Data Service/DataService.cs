namespace SNSD.Data_Service
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using SNSD.Helper;
    using SNSD.Model;

    public class DataService
    {
        //This method will be used to call list of members API and we will be parse it
        public async Task<List<Member>> SendListMembersRequest()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    //Accept application/json only
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.LIST_MEMBERS));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);
                    //parse string to list member
                    var modelResult = JsonConvert.DeserializeObject<List<Member>>(result);
                    return modelResult;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        //This method will be used to call detail API and we will be parse it
        public async Task<Member> SendDetailMemberRequest(string name)
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    //Accept application/json only
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(UrlHelper.LIST_MEMBERS + "/" + name);
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);
                    //parse string to a member
                    var modelResult = JsonConvert.DeserializeObject<Member>(result);
                    return modelResult;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}