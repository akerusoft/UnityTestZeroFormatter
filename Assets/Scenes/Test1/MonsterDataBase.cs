public abstract class MonsterDataBase
{
    DataRoot.DataTypeVersion _dataType;

    protected MonsterDataBase(DataRoot.DataTypeVersion dataType)
    {
        _dataType = dataType;
    }

    public DataRoot.DataTypeVersion DataType
    {
        get { return _dataType; }
    }
}
