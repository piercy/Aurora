using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Settings;
using Aurora.Utils;
using Newtonsoft.Json;
using Plugin_PushBullet.Models;
using WebSocketSharp;

namespace Plugin_PushBullet.PushBullet
{
    static class DataStream
    {

        public static Dictionary<string, List<int>> ActiveNotificationsList = new Dictionary<string, List<int>>();

        static List<string> _excludedTitleList = new List<string>();

        private static WebSocket ws = null;

        private static PushBulletSettings _settings;
        static DataStream()
        {
            _excludedTitleList.Add("WhatsApp Web");
        }

        public static void StartListening(PushBulletSettings settings)
        {
            _settings = settings;

            if (_settings != null && !string.IsNullOrEmpty(_settings.PushbulletAccessToken))
            {
                if (ws == null)
                    ws = new WebSocket("wss://stream.pushbullet.com/websocket/" + _settings.PushbulletAccessToken);

                if (ws != null && !ws.IsAlive)
                {


                    ws.OnMessage += Ws_OnMessage;

                    ws.Connect();
                }
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


                var notificationType = GetMobileApplicationType(incomingObject.Push.Package_Name);

                if (!string.IsNullOrEmpty(notificationType))
                {
                    if (!ActiveNotificationsList.ContainsKey(notificationType))
                        ActiveNotificationsList.Add(notificationType, new List<int>());

                    ActiveNotificationsList[notificationType] =
                        HandleApplication(ActiveNotificationsList[notificationType], incomingObject, notificationType);
                }
            }


        }

        private static string GetMobileApplicationType(string packageName)
        {

           var notificationType =  _settings.NotificationTargets.SingleOrDefault(x => x.PackageNames.Contains(packageName));

            return notificationType?.Name;
        }

        private static List<int> HandleApplication(List<int> currentList, PushBulletBase incomingObject, string notificationType)
        {
            if (incomingObject.Push.Type == "dismissal")
            {
                Console.WriteLine("Removing: {0} : {1}", notificationType, incomingObject.Push.notification_id);
                if (currentList.Contains(incomingObject.Push.notification_id))
                    currentList.Remove(incomingObject.Push.notification_id);
                
            }
            else
            {
                
                if (!_excludedTitleList.Contains(incomingObject.Push.Title))
                {
                    Console.WriteLine("Adding: {0} : {1}", notificationType, incomingObject.Push.notification_id);

                    if (!currentList.Contains(incomingObject.Push.notification_id))
                        currentList.Add(incomingObject.Push.notification_id);
                }
            }


            return currentList;
        }

    }

}
