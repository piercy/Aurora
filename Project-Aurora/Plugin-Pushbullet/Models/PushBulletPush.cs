namespace Plugin_PushBullet.Models
{
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