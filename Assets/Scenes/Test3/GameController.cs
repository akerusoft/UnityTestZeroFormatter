using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

namespace Test3
{
    public class GameController : MonoBehaviour
    {
        void Awake()
        {
            SettingDataVer1 version1 = new SettingDataVer1()
            {
                MusicVolume = 1.2f,
                EffectsVolume = 3.4f,
            };

            Serialize(version1);

            SettingDataVer2 version2 = new SettingDataVer2()
            {
                Language = LanguageType.English,
            };

            Serialize(version2);
        }

        void Serialize(SettingData data)
        {
            byte[] bytes = ZeroFormatter.ZeroFormatterSerializer.Serialize<SettingData>(data);
            SettingData loadData = ZeroFormatter.ZeroFormatterSerializer.Deserialize<SettingData>(bytes);
            OutputToLog(loadData);
        }

        void OutputToLog(SettingData data)
        {
            switch(data.Version)
            {
                case FileVersion.Version1:
                    SettingDataVer1 version1 = (SettingDataVer1)data;
                    Debug.LogFormat("Data is version1. MusicVolume:{0}, EffectsVolume:{1}", version1.MusicVolume, version1.EffectsVolume);
                    break;
                case FileVersion.Version2:
                    SettingDataVer2 version2 = (SettingDataVer2)data;
                    Debug.LogFormat("Data is version2. Language:{0}", version2.Language);
                    break;
            }
        }
    }
}
