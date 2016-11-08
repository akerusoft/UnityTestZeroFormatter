using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using ZeroFormatter;

public class GameController : MonoBehaviour
{
    void Start()
    {
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

        Debug.Log("------------------------------------");

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

        Debug.Log("------------------------------------");

        {
            DataRoot dataRoot = new DataRoot();

            dataRoot.SetMonsterData(dataV1);

            {
                byte[] bytes = ZeroFormatterSerializer.Serialize(dataRoot);
                DataRoot loadData = ZeroFormatterSerializer.Deserialize<DataRoot>(bytes);

                MonsterDataBase dataBase = loadData.LoadMonsterData();

                if (dataBase is MonsterDataV1)
                {
                    MonsterDataV1 data = dataBase as MonsterDataV1;
                    Debug.Log("Name:" + data.Name);
                    Debug.Log("HitPoint:" + data.HitPoint);
                    Debug.Log("HitRate:" + data.HitRate);
                    Debug.Log("Speed:" + data.Speed);
                    Debug.Log("Luck:" + data.Luck);
                }
                else
                {
                    Debug.LogError("Failed load data version1.");
                }
            }
        }

        Debug.Log("------------------------------------");

        {
            DataRoot dataRoot = new DataRoot();

            dataRoot.SetMonsterData(dataV2);

            {
                byte[] bytes = ZeroFormatterSerializer.Serialize(dataRoot);
                DataRoot loadData = ZeroFormatterSerializer.Deserialize<DataRoot>(bytes);

                MonsterDataBase dataBase = loadData.LoadMonsterData();

                if (dataBase is MonsterDataV2)
                {
                    MonsterDataV2 data = dataBase as MonsterDataV2;
                    Debug.Log("Name:" + data.Name);
                    Debug.Log("HitPoint:" + data.HitPoint);
                    Debug.Log("HitRate:" + data.HitRate);
                    Debug.Log("Speed:" + data.Speed);
                    Debug.Log("Luck:" + data.Luck);
                    Debug.Log("Defense:" + data.Defense);
                }
                else
                {
                    Debug.LogError("Failed load data version2.");
                }
            }
        }
    }
}
