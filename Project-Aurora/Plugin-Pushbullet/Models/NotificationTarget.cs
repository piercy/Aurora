using System.Collections.Generic;

namespace Plugin_PushBullet.Models
{
    public class NotificationTarget
    {

        public string Name { get; set; }

        public List<string> PackageNames { get; set; }

        public List<string> Exclusions { get; set; }


        public NotificationTarget(string name, List<string> packageNames, List<string> exclusions)
        {
            Name = name;
            PackageNames = packageNames;
            Exclusions = exclusions;
        }
    }
}