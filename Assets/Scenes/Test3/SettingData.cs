using ZeroFormatter;

namespace Test3
{
    public enum FileVersion
    {
        Version1,
        Version2,
    }

    public enum LanguageType
    {
        Japanese,
        English,
    }

    [Union(typeof(SettingDataVer1), typeof(SettingDataVer2))]
    public abstract class SettingData
    {
        [UnionKey]
        public abstract FileVersion Version { get; }
    }

    [ZeroFormattable]
    public class SettingDataVer1 : SettingData
    {
        public override FileVersion Version
        {
            get { return FileVersion.Version1; }
        }

        [Index(0)]
        public virtual float MusicVolume { get; set; }

        [Index(1)]
        public virtual float EffectsVolume { get; set; }
    }

    [ZeroFormattable]
    public class SettingDataVer2 : SettingData
    {
        public override FileVersion Version
        {
            get { return FileVersion.Version2; }
        }

        [Index(0)]
        public virtual LanguageType Language { get; set; }
    }
}
