using BucketList.Mobile.Properties;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace BucketList.Api.Http
{
    public class Client
    {

        private static Client _instance;
        public static Client Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Client();
                }
                return _instance;
            }
        }
        public HttpClient GetClient { get; private set; }

        public Client()
        {
            InitClient();
        }

        private void InitClient()
        {
            string baseUrl = Resources.BASE_URL;
            GetClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
    }
}