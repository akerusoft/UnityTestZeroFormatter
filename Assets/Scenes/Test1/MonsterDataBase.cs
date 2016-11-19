using ZeroFormatter;

[Union(typeof(MonsterDataV1), typeof(MonsterDataV2))]
public abstract class MonsterDataBase
{
    public enum VersionType
    {
        MonsterDataV1,
        MonsterDataV2,
    }

    [UnionKey]
    public abstract VersionType Version
    {
        get;
    }
}
