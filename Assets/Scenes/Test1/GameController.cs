using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using ZeroFormatter;

public class GameController : MonoBehaviour
{
    void Start()
    {
        Debug.Log("-----------------<1>-----------------");

        MonsterDataV1 dataV1 = new MonsterDataV1()
        {
            Name = "スライム",
            HitPoint = 10,
            HitRate = 80f,
            Speed = 2,
            Luck = 3,
        };

        {
            byte[] bytes = ZeroFormatterSerializer.Serialize(dataV1);
            MonsterDataV1 loadData = ZeroFormatterSerializer.Deserialize<MonsterDataV1>(bytes);

            Debug.Log("Name:" + loadData.Name);
            Debug.Log("HitPoint:" + loadData.HitPoint);
            Debug.Log("HitRate:" + loadData.HitRate);
            Debug.Log("Speed:" + loadData.Speed);
            Debug.Log("Luck:" + loadData.Luck);
        }

        Debug.Log("-----------------<2>-----------------");

        MonsterDataV2 dataV2 = new MonsterDataV2()
        {
            Name = "スライムベッキー",
            HitPoint = 100,
            HitRate = 95f,
            Speed = 200,
            Luck = 300,
            Defense = 400,
        };

        {
            byte[] bytes = ZeroFormatterSerializer.Serialize(dataV2);
            MonsterDataV2 loadData = ZeroFormatterSerializer.Deserialize<MonsterDataV2>(bytes);

            Debug.Log("Name:" + loadData.Name);
            Debug.Log("HitPoint:" + loadData.HitPoint);
            Debug.Log("HitRate:" + loadData.HitRate);
            Debug.Log("Speed:" + loadData.Speed);
            Debug.Log("Luck:" + loadData.Luck);
            Debug.Log("Defense:" + loadData.Defense);
        }

        Debug.Log("-----------------<3>-----------------");

        PackAndUnpack(dataV1);

        Debug.Log("-----------------<4>-----------------");

        PackAndUnpack(dataV2);
    }

    void PackAndUnpack(MonsterDataBase data)
    {
        byte[] bytes = ZeroFormatterSerializer.Serialize(data);
        MonsterDataBase loadData = ZeroFormatterSerializer.Deserialize<MonsterDataBase>(bytes);
        OutputToLog(loadData);
    }

    void OutputToLog(MonsterDataBase data)
    {
        switch(data.DataType)
        {
            case DataRoot.DataTypeVersion.MonsterDataV1:
                MonsterDataV1 version1 = data as MonsterDataV1;
                Debug.Log("Name:" + version1.Name);
                Debug.Log("HitPoint:" + version1.HitPoint);
                Debug.Log("HitRate:" + version1.HitRate);
                Debug.Log("Speed:" + version1.Speed);
                Debug.Log("Luck:" + version1.Luck);
                break;
            case DataRoot.DataTypeVersion.MonsterDataV2:
                MonsterDataV2 version2 = data as MonsterDataV2;
                Debug.Log("Name:" + version2.Name);
                Debug.Log("HitPoint:" + version2.HitPoint);
                Debug.Log("HitRate:" + version2.HitRate);
                Debug.Log("Speed:" + version2.Speed);
                Debug.Log("Luck:" + version2.Luck);
                Debug.Log("Defense:" + version2.Defense);
                break;
        }
    }
}
