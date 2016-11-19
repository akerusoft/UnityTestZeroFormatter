using ZeroFormatter;

[Union(typeof(MonsterDataV1), typeof(MonsterDataV2))]
public abstract class MonsterDataBase
{
    [UnionKey]
    public abstract DataRoot.DataTypeVersion DataType
    {
        get;
    }
}
