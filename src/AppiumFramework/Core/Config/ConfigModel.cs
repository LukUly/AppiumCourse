namespace AppiumFramework.Core.Config
{
    public class ConfigModel
    {
        public virtual string PlatformName { get; set; }
        public virtual string Package { get; set; }
        public virtual string Uri { get; set; }
        public virtual int WaitingTimeSeconds { get; set; }
        public virtual string LogPath { get; set; }
    }
}
