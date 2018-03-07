using System;
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
}
