using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Utils;
using Newtonsoft.Json;
using Plugin_PushBullet.Models;
using WebSocketSharp;

namespace Plugin_PushBullet.PushBullet
{
    static class DataStream
    {

        private const string AccessToken = "o.V04laqnLIsdGMwXzWxkPfMZLw6ue5HyK";

        public static Dictionary<MobileApplicationType, List<int>> ActiveNotificationsList = new Dictionary<MobileApplicationType, List<int>>();

        static List<string> _excludedTitleList = new List<string>();

        private static WebSocket ws = null;

        static DataStream()
        {
            _excludedTitleList.Add("WhatsApp Web");
        }

        public static void StartListening()
        {
            if(ws == null)
                ws = new WebSocket("wss://stream.pushbullet.com/websocket/" + AccessToken);

            if (ws != null && !ws.IsAlive)
            {
             

                ws.OnMessage += Ws_OnMessage;

                ws.Connect();
            }
        }

        public static void StopListening()
        {
            ws.Close();
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("PushBullet says: " + e.Data);
            var incomingObject = JsonConvert.DeserializeObject<PushBulletBase>(e.Data);


            Console.WriteLine("JSON: " + incomingObject.Type);
            if (incomingObject.Push != null)
            {
                Console.WriteLine("Push Type:" + incomingObject.Push.Type);
                Console.WriteLine("Push Application_Name:" + incomingObject.Push.Application_Name);
                Console.WriteLine("Push Package_Name:" + incomingObject.Push.Package_Name);
                Console.WriteLine("Push Title:" + incomingObject.Push.Title);


                var mobileApplicationType = GetMobileApplicationType(incomingObject.Push.Package_Name);

                if(!ActiveNotificationsList.ContainsKey(mobileApplicationType))
                    ActiveNotificationsList.Add(mobileApplicationType, new List<int>());

                ActiveNotificationsList[mobileApplicationType] = HandleApplication(ActiveNotificationsList[mobileApplicationType], incomingObject, mobileApplicationType);
            }


        }

        private static MobileApplicationType GetMobileApplicationType(string packageName)
        {
            switch (packageName)
            {
                case "com.google.android.dialer":
                    return MobileApplicationType.Phone;
                case "com.google.android.apps.inbox":
                    return MobileApplicationType.Email;
                case "com.whatsapp":
                    return MobileApplicationType.Whatsapp;
            }
            return MobileApplicationType.None;
        }

        private static List<int> HandleApplication(List<int> currentList, PushBulletBase incomingObject, MobileApplicationType type)
        {
            if (incomingObject.Push.Type == "dismissal")
            {
                Console.WriteLine("Removing: {0} : {1}", type, incomingObject.Push.notification_id);
                if (currentList.Contains(incomingObject.Push.notification_id))
                    currentList.Remove(incomingObject.Push.notification_id);
                
            }
            else
            {
                
                if (!_excludedTitleList.Contains(incomingObject.Push.Title))
                {
                    Console.WriteLine("Adding: {0} : {1}", type, incomingObject.Push.notification_id);

                    if (!currentList.Contains(incomingObject.Push.notification_id))
                        currentList.Add(incomingObject.Push.notification_id);
                }
            }


            return currentList;
        }

    }

}
