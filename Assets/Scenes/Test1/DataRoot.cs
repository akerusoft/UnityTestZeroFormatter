using System;
using ZeroFormatter;
using ZeroFormatter.Segments;

[ZeroFormattable]
public class DataRoot
{
    public enum DataTypeVersion
    {
        MonsterDataV1,
        MonsterDataV2,
    }

    [Index(0)]
    public virtual DataTypeVersion DataType { get; set; }

    [Index(1)]
    public virtual byte[] Data { get; set; }

    public bool SetMonsterData(MonsterDataBase data)
    {
        bool ret = false;

        if(data is MonsterDataV1)
        {
            this.DataType = DataTypeVersion.MonsterDataV1;
            this.Data = ZeroFormatterSerializer.Serialize((MonsterDataV1)data);
            ret = true;
        }
        else if(data is MonsterDataV2)
        {
            this.DataType = DataTypeVersion.MonsterDataV2;
            this.Data = ZeroFormatterSerializer.Serialize((MonsterDataV2)data);
            ret = true;
        }

        return ret;
    }

    public MonsterDataBase LoadMonsterData()
    {
        switch(DataType)
        {
            case DataTypeVersion.MonsterDataV1:
                return ZeroFormatterSerializer.Deserialize<MonsterDataV1>(Data);
            case DataTypeVersion.MonsterDataV2:
                return ZeroFormatterSerializer.Deserialize<MonsterDataV2>(Data);
            default:
                return null;
        }
    }
}

public class DataRootFormatter : ZeroFormatter.Formatters.Formatter<DataRoot>
{
    public override int? GetLength()
    {
        return null;
    }

    public override DataRoot Deserialize(ref byte[] bytes, int offset, DirtyTracker tracker, out int byteSize)
    {
        DataRoot dataRoot = new DataRoot();
        dataRoot.DataType = ZeroFormatter.Formatters.Formatter<DataRoot.DataTypeVersion>.Default.Deserialize(ref bytes, offset, tracker, out byteSize);
        dataRoot.Data = ZeroFormatter.Formatters.Formatter<byte[]>.Default.Deserialize(ref bytes, offset, tracker, out byteSize);
        return dataRoot;
    }

    public override int Serialize(ref byte[] bytes, int offset, DataRoot value)
    {
        int writeSize = ZeroFormatter.Formatters.Formatter<DataRoot.DataTypeVersion>.Default.Serialize(ref bytes, offset, value.DataType);
        writeSize += ZeroFormatter.Formatters.Formatter<byte[]>.Default.Serialize(ref bytes, offset, value.Data);
        return writeSize;
    }
}
