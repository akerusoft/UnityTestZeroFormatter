using ZeroFormatter;

[ZeroFormattable]
public class MonsterDataV1 : MonsterDataBase
{
    public override DataRoot.DataTypeVersion DataType
    {
        get { return DataRoot.DataTypeVersion.MonsterDataV1; }
    }

    [Index(0)]
    public virtual string Name { get; set; }

    [Index(1)]
    public virtual uint HitPoint { get; set; }

    [Index(2)]
    public virtual float HitRate { get; set; }

    [Index(3)]
    public virtual uint Speed { get; set; }

    [Index(4)]
    public virtual uint Luck { get; set; }
}
