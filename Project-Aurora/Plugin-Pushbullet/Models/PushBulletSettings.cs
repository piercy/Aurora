using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Plugin_PushBullet.Models
{
    public class PushBulletSettings
    {
        public string PushbulletAccessToken { get; set; }

        public List<NotificationTarget> NotificationTargets  { get; set; }

        public PushBulletSettings(bool setupDefaults = false)
        {
            if (setupDefaults)
            {
                NotificationTargets = new List<NotificationTarget>();

                // Facebook defaults
                var fbPackageNames = new List<string> {"com.facebook.katana", "com.facebook.orca"};


                var fbNotificationTarget = new NotificationTarget("Facebook", fbPackageNames, null);
                NotificationTargets.Add(fbNotificationTarget);


                // snapchat defaults
                var scPackageNames = new List<string> {"com.snapchat.android"};

                var scNotificationTarget = new NotificationTarget("Snapchat", scPackageNames, null);
                NotificationTargets.Add(scNotificationTarget);

                // whatsapp defaults
                var waPackageNames = new List<string> {"com.whatsapp"};
                var waExclusions = new List<string> {"WhatsApp Web"};

                var waNotificationTarget = new NotificationTarget("Whatsapp", waPackageNames, waExclusions);
                NotificationTargets.Add(waNotificationTarget);

                // Phone Call defaults
                var pcPackageNames = new List<string> {"com.google.android.dialer"};

                var pcNotificationTarget = new NotificationTarget("PhoneCall", pcPackageNames, null);
                NotificationTargets.Add(pcNotificationTarget);

                // Email defaults
                var emPackageNames = new List<string> {"com.google.android.apps.inbox"};

                var emNotificationTarget = new NotificationTarget("Email", emPackageNames, null);
                NotificationTargets.Add(emNotificationTarget);

            }
        }
    }
}