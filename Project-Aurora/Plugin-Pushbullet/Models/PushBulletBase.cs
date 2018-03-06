using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin_PushBullet.Models
{
    class PushBulletBase
    {
        public string Type { get; set; }

        public PushBulletPush Push { get; set; }
    }

    class PushBulletPush
    {
        public int notification_id;
        public string Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string Application_Name { get; set; }
        public string Package_Name { get; set; }
        
    }
}
