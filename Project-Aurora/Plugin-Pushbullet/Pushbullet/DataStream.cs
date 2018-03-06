using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Utils;
using Newtonsoft.Json;
using Plugin_Pushbullet.Models;
using WebSocketSharp;

namespace Plugin_Pushbullet.Pushbullet
{
    static class DataStream
    {

        private const string AccessToken = "o.V04laqnLIsdGMwXzWxkPfMZLw6ue5HyK";

        public static List<int> Calls = new List<int>();
        private static WebSocket ws = null;
        public static void StartListening()
        {
            ws = new WebSocket("wss://stream.pushbullet.com/websocket/" + AccessToken);
            
                ws.OnMessage += Ws_OnMessage;   

                ws.Connect();
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

                if (incomingObject.Push.Package_Name == "com.google.android.dialer")
                {
                    if (incomingObject.Push.Body?.ToLower() == "incoming call")
                    {

                        if (!Calls.Contains(incomingObject.Push.notification_id))
                            Calls.Add(incomingObject.Push.notification_id);

                    } else if (incomingObject.Push.Type == "dismissal")
                    {
                        if (Calls.Contains(incomingObject.Push.notification_id))
                            Calls.Remove(incomingObject.Push.notification_id);
                    }
                }
            }

            
        }
    }
}
