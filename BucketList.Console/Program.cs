using BucketList.Api.Managers;
using System;

namespace BucketList.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var line = System.Console.ReadLine();
                if (line.Equals("Test"))
                {
                    new Program();
                }
            }
        }

        public Program()
        {
            TestSignIn();
        }

        private async void TestSignIn()
        {
            SignInManager manager = new SignInManager();
            var result = await manager.SignInUser("codyjg10@gmail.com", "Airplane10");
            System.Console.WriteLine("Test");
        }
    }
}
