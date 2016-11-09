using FlatBuffers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Diagnostics;
using System.Collections;
using System.IO;
using ZeroFormatter;

namespace Test2
{
    public class GameController : MonoBehaviour
    {
        const int COUNT = 1000;
        const string ZERO_FORMATTER_FILE_NAME = "ZeroFormatter.bin";
        const string FLAT_BUFFERS_FILE_NAME = "FlatBuffers.bin";

        const string MONSTER_NAME = "スライム";

        [SerializeField]
        Text _textFlatBuffers;

        [SerializeField]
        Text _textZeroFormatter;

        long _flatbuffresElapsedTime;
        long _zeroFormatterElapsedTime;

        IEnumerator Start()
        {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            yield return null;

            TestZeroFormatter();

            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            yield return null;

            TestFlatBuffers();

            _textFlatBuffers.text = string.Format("FlatBuffers:{0}[ms]", _flatbuffresElapsedTime);
            _textZeroFormatter.text = string.Format("ZeroFormatter:{0}[ms]", _zeroFormatterElapsedTime);
        }

        void TestFlatBuffers()
        {
            Stopwatch stopWatch = new Stopwatch();

            string name = null;
            uint hitPoint = 0;
            float hitRate = 0f;
            uint speed = 0;
            uint luck = 0;

            string path = Path.Combine(Application.persistentDataPath, FLAT_BUFFERS_FILE_NAME);
            //UnityEngine.Debug.Log("path:" + path);

            if (File.Exists(path))
                File.Delete(path);

            stopWatch.Start();

            for (int i = 0; i < COUNT; ++i)
            {
                // 読み込み
                if (File.Exists(path))
                {
                    ByteBuffer buffer = new ByteBuffer(File.ReadAllBytes(path));

                    MyFlatBuffers.Root root = MyFlatBuffers.Root.GetRootAsRoot(buffer);

                    MyFlatBuffers.Data dataType = root.DataType;

                    switch (dataType)
                    {
                        case MyFlatBuffers.Data.MonsterDataV1:
                            MyFlatBuffers.MonsterDataV1 monsterData = root.GetData(new MyFlatBuffers.MonsterDataV1());

                            name = monsterData.Name;
                            hitPoint = monsterData.Hp;
                            hitRate = monsterData.HitRate;
                            speed = monsterData.Speed;
                            luck = monsterData.Luck;
                            //UnityEngine.Debug.Log("[FlatBuffers] Data load. hitPoint:" + hitPoint);
                            break;
                    }
                }

                hitPoint++;
                hitRate += 1f;
                speed++;
                luck++;

                // 書き込み
                {
                    FlatBufferBuilder builder = new FlatBufferBuilder(1);

                    int offestData;
                    MyFlatBuffers.Data dataType;

                    Offset<MyFlatBuffers.MonsterDataV1> data = MyFlatBuffers.MonsterDataV1.CreateMonsterDataV1(builder, builder.CreateString(MONSTER_NAME), hitPoint, hitRate, speed, luck);
                    offestData = data.Value;
                    dataType = MyFlatBuffers.Data.MonsterDataV1;

                    MyFlatBuffers.Root.StartRoot(builder);
                    MyFlatBuffers.Root.AddDataType(builder, dataType);
                    MyFlatBuffers.Root.AddData(builder, offestData);
                    Offset<MyFlatBuffers.Root> endOffset = MyFlatBuffers.Root.EndRoot(builder);

                    MyFlatBuffers.Root.FinishRootBuffer(builder, endOffset);

                    //byte[] bytes = builder.SizedByteArray();
                    //File.WriteAllBytes(path, bytes);

                    // 公式の実装がこうなっていたので
                    using (MemoryStream ms = new MemoryStream(builder.DataBuffer.Data, builder.DataBuffer.Position, builder.Offset))
                    {
                        byte[] bytes = ms.ToArray();
                        File.WriteAllBytes(path, bytes);
                    }
                }

                //UnityEngine.Debug.Log("[FlatBuffers] hitPoint:" + hitPoint);

                hitPoint = 0;
            }

            stopWatch.Stop();
            _flatbuffresElapsedTime = stopWatch.ElapsedMilliseconds;
        }

        void TestZeroFormatter()
        {
            Stopwatch stopWatch = new Stopwatch();

            string name = null;
            uint hitPoint = 0;
            float hitRate = 0f;
            uint speed = 0;
            uint luck = 0;

            string path = Path.Combine(Application.persistentDataPath, ZERO_FORMATTER_FILE_NAME);
            //UnityEngine.Debug.Log("path:" + path);

            if (File.Exists(path))
                File.Delete(path);

            stopWatch.Start();

            for (int i = 0; i < COUNT; ++i)
            {
                // 読み込み
                if (File.Exists(path))
                {
                    DataRoot dataRoot = ZeroFormatterSerializer.Deserialize<DataRoot>(File.ReadAllBytes(path));
                    MonsterDataBase dataBase = dataRoot.LoadMonsterData();

                    switch(dataBase.DataType)
                    {
                        case DataRoot.DataTypeVersion.MonsterDataV1:
                        {
                            MonsterDataV1 data = dataBase as MonsterDataV1;
                            name = data.Name;
                            hitPoint = data.HitPoint;
                            hitRate = data.HitRate;
                            speed = data.Speed;
                            luck = data.Luck;
                            //UnityEngine.Debug.Log("[ZeroFormatter] Data load. hitPoint:" + hitPoint);
                            break;
                        }
                    }
                }

                hitPoint++;
                hitRate += 1f;
                speed++;
                luck++;

                // 書き込み
                {
                    MonsterDataV1 data = new MonsterDataV1()
                    {
                        Name = MONSTER_NAME,
                        HitPoint = hitPoint,
                        HitRate = hitRate,
                        Speed = speed,
                        Luck = luck,
                    };

                    DataRoot dataRoot = new DataRoot();
                    dataRoot.SetMonsterData(data);

                    File.WriteAllBytes(path, ZeroFormatterSerializer.Serialize(dataRoot));
                }

                //UnityEngine.Debug.Log("[ZeroFormatter] hitPoint:" + hitPoint);

                hitPoint = 0;
            }

            stopWatch.Stop();
            _zeroFormatterElapsedTime = stopWatch.ElapsedMilliseconds;
        }
    }
}
