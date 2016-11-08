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
        int dataType = ZeroFormatter.Internal.BinaryUtil.ReadInt32(ref bytes, offset);
        byteSize = 4;

        DataRoot dataRoot = new DataRoot();

        dataRoot.DataType = (DataRoot.DataTypeVersion)dataType;

        int length = ZeroFormatter.Internal.BinaryUtil.ReadInt32(ref bytes, offset);

        if(length==-1)
        {
            byteSize += 4;
            return dataRoot;
        }
        else
        {
            byteSize += length + 4;
        }

        byte[] data = new byte[length];
        Buffer.BlockCopy(bytes, offset + 8, data, 0, length);
        dataRoot.Data = data;

        return dataRoot;
    }

    public override int Serialize(ref byte[] bytes, int offset, DataRoot value)
    {
        int writeSize = ZeroFormatter.Internal.BinaryUtil.WriteInt32(ref bytes, offset, (Int32)value.DataType);

        byte[] data = value.Data;

        if(data==null)
        {
            ZeroFormatter.Internal.BinaryUtil.WriteInt32(ref bytes, offset, -1);
            writeSize += 4;
            return writeSize;
        }

        writeSize += data.Length;
        ZeroFormatter.Internal.BinaryUtil.EnsureCapacity(ref bytes, offset, writeSize + 4);
        ZeroFormatter.Internal.BinaryUtil.WriteInt32Unsafe(ref bytes, offset, data.Length);
        Buffer.BlockCopy(data, 0, bytes, offset + 4, data.Length);
        return writeSize + 4;
    }
}
