using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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

        private HttpClient client;
        public HttpClient GetClient { get { return client;  } }

        public Client()
        {
            InitClient();
        }

        private void InitClient()
        {
            string baseUrl = "http://acebucketlist.azurewebsites.net/";
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
           
            };
        }
    }
}