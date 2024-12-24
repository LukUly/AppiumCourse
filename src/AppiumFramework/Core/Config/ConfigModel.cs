namespace AppiumFramework.Core.Config
{
    public class ConfigModel
    {
        public virtual string Package { get; set; }
        public virtual string Uri { get; set; }
        public virtual int WaitingTimeSeconds { get; set; }
        public virtual string LogPath { get; set; }
        public virtual string ScreenshotFolder { get; set; }
        public virtual string LogName { get; set; }
        public virtual string LogFormat { get; set; }
        public virtual Capability[] Capabilities { get; set; }
    }

    public class Capability
    {
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }
}
