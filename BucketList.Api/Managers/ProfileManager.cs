using BucketList.Api.Http;
using BucketList.Api.Interfaces;
using BucketList.Entities.Models;
using BucketList.Site.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            StringContent content = new StringContent(bodyData);
            var response = await Client.Instance.GetClient.PostAsync("api/profile/CreateListItem"
                                                                     + "?userId=" + userId,
                                                                     content);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BucketListItem>(result);
        }

        public async void DeleteListItem(Guid userId, Guid itemId)
        {
            await Client.Instance.GetClient.DeleteAsync("api/profile/DeleteListItem?" +
                                                        "userId=" + userId +
                                                        "itemId=" + itemId);
        }

        public async void Follow(Guid userId, Guid userToFollowId)
        {
            await Client.Instance.GetClient.GetAsync("api/profile/Follow?" +
                                                     "userId=" + userId +
                                                     "userToFollowId=" + userToFollowId);
        }

        public async void SaveUser(User user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user));
            await Client.Instance.GetClient.PostAsync("api/profile/SaveUser", content);
        }

        public async void SetItemStatus(Guid userId, Guid itemId, int status)
        {
            await Client.Instance.GetClient.PutAsync("api/profile/SetItemStatus?" +
                                                    "userId=" + userId +
                                                    "itemId=" + itemId +
                                                    "status=" + status, null);
        }
    }
}
