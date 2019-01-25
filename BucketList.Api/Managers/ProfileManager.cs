using BucketList.Api.Http;
using BucketList.Api.Interfaces;
using BucketList.Entities.Models;
using BucketList.Site.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BucketList.Api.Managers
{
    public class ProfileManager : IProfileManager
    {
        private static ProfileManager _instance;
        public static ProfileManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProfileManager();
                }
                return _instance;
            }
        }

        public async Task<BucketListItem> CreateListItem(string body, Guid userId)
        {
            BodyObject bodyObject = new BodyObject()
            {
                Body = body
            };
            string bodyData = JsonConvert.SerializeObject(bodyObject);
            StringContent content = new StringContent(bodyData, Encoding.UTF8, "application/json");
            var response = await Client.Instance.GetClient.PostAsync("api/profile/CreateListItem"
                                                                     + "?userId=" + userId,
                                                                     content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BucketListItem>(result);
        }

        public async Task<HttpStatusCode> DeleteListItem(Guid userId, Guid itemId)
        {
            var result = await Client.Instance.GetClient.DeleteAsync("api/profile/DeleteListItem?" +
                                                        "userId=" + userId + "&" + 
                                                        "itemId=" + itemId);
            return result.StatusCode;
        }

        public async Task<HttpStatusCode> Follow(Guid userId, Guid userToFollowId)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/profile/Follow?" +
                                                     "userId=" + userId + "&" + 
                                                     "userToFollowId=" + userToFollowId);
            return response.StatusCode;
        }

        public async void SaveUser(User user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user));
            await Client.Instance.GetClient.PostAsync("api/profile/SaveUser", content);
        }

        public async Task<HttpStatusCode> SetItemStatus(Guid itemId, int status)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/profile/SetItemStatus?" +
                                                    "itemId=" + itemId +  "&" + 
                                                    "status=" + status);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> SaveItem(BucketListItem item)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var response = await Client.Instance.GetClient.PostAsync("api/profile/SaveItem", content);
            return response.StatusCode;
        }
    }
}