#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.Internal
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;
    using global::ZeroFormatter.Comparers;

    public static partial class ZeroFormatterInitializer
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            // Enums
            ZeroFormatter.Formatters.Formatter<global::DataRoot.DataTypeVersion>.Register(new ZeroFormatter.DynamicObjectSegments.DataTypeVersionFormatter());
            ZeroFormatter.Formatters.Formatter<global::DataRoot.DataTypeVersion?>.Register(new ZeroFormatter.DynamicObjectSegments.NullableDataTypeVersionFormatter());
            ZeroFormatter.Comparers.ZeroFormatterEqualityComparer<global::DataRoot.DataTypeVersion>.Register(new ZeroFormatter.DynamicObjectSegments.DataTypeVersionEqualityComparer());
            // Objects
            ZeroFormatter.Formatters.Formatter<global::DataRoot>.Register(new ZeroFormatter.DynamicObjectSegments.DataRootFormatter());
            ZeroFormatter.Formatters.Formatter<global::MonsterDataV1>.Register(new ZeroFormatter.DynamicObjectSegments.MonsterDataV1Formatter());
            ZeroFormatter.Formatters.Formatter<global::MonsterDataV2>.Register(new ZeroFormatter.DynamicObjectSegments.MonsterDataV2Formatter());
            // Structs
            // Generics
        }
    }
}
#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments
{
    using global::System;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;

    public class DataRootFormatter : Formatter<global::DataRoot>
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::DataRoot value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (1 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<global::DataRoot.DataTypeVersion>(ref bytes, startOffset, offset, 0, value.DataType);
                offset += ObjectSegmentHelper.SerializeFromFormatter<byte[]>(ref bytes, startOffset, offset, 1, value.Data);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 1);
            }
        }

        public override global::DataRoot Deserialize(ref byte[] bytes, int offset, DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new DataRootObjectSegment(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class DataRootObjectSegment : global::DataRoot, IZeroFormatterSegment
    {
        static readonly int[] __elementSizes = new int[]{ 4, 0 };

        readonly ArraySegment<byte> __originalBytes;
        readonly DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        readonly CacheSegment<byte[]> _Data;

        // 0
        public override global::DataRoot.DataTypeVersion DataType
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<global::DataRoot.DataTypeVersion>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<global::DataRoot.DataTypeVersion>(__originalBytes, 0, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 1
        public override byte[] Data
        {
            get
            {
                return _Data.Value;
            }
            set
            {
                _Data.Value = value;
            }
        }


        public DataRootObjectSegment(DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 1, __elementSizes);

            _Data = new CacheSegment<byte[]>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 1, __binaryLastIndex, __tracker));
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (1 + 1));

                offset += ObjectSegmentHelper.SerializeFixedLength<global::DataRoot.DataTypeVersion>(ref targetBytes, startOffset, offset, 0, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeCacheSegment<byte[]>(ref targetBytes, startOffset, offset, 1, _Data);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 1);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }

    public class MonsterDataV1Formatter : Formatter<global::MonsterDataV1>
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::MonsterDataV1 value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (4 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<string>(ref bytes, startOffset, offset, 0, value.Name);
                offset += ObjectSegmentHelper.SerializeFromFormatter<uint>(ref bytes, startOffset, offset, 1, value.HitPoint);
                offset += ObjectSegmentHelper.SerializeFromFormatter<float>(ref bytes, startOffset, offset, 2, value.HitRate);
                offset += ObjectSegmentHelper.SerializeFromFormatter<uint>(ref bytes, startOffset, offset, 3, value.Speed);
                offset += ObjectSegmentHelper.SerializeFromFormatter<uint>(ref bytes, startOffset, offset, 4, value.Luck);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 4);
            }
        }

        public override global::MonsterDataV1 Deserialize(ref byte[] bytes, int offset, DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new MonsterDataV1ObjectSegment(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class MonsterDataV1ObjectSegment : global::MonsterDataV1, IZeroFormatterSegment
    {
        static readonly int[] __elementSizes = new int[]{ 0, 4, 4, 4, 4 };

        readonly ArraySegment<byte> __originalBytes;
        readonly DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        readonly CacheSegment<string> _Name;

        // 0
        public override string Name
        {
            get
            {
                return _Name.Value;
            }
            set
            {
                _Name.Value = value;
            }
        }

        // 1
        public override uint HitPoint
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<uint>(__originalBytes, 1, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<uint>(__originalBytes, 1, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 2
        public override float HitRate
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<float>(__originalBytes, 2, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<float>(__originalBytes, 2, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 3
        public override uint Speed
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<uint>(__originalBytes, 3, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<uint>(__originalBytes, 3, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 4
        public override uint Luck
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<uint>(__originalBytes, 4, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<uint>(__originalBytes, 4, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }


        public MonsterDataV1ObjectSegment(DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 4, __elementSizes);

            _Name = new CacheSegment<string>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 0, __binaryLastIndex, __tracker));
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (4 + 1));

                offset += ObjectSegmentHelper.SerializeCacheSegment<string>(ref targetBytes, startOffset, offset, 0, _Name);
                offset += ObjectSegmentHelper.SerializeFixedLength<uint>(ref targetBytes, startOffset, offset, 1, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<float>(ref targetBytes, startOffset, offset, 2, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<uint>(ref targetBytes, startOffset, offset, 3, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<uint>(ref targetBytes, startOffset, offset, 4, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 4);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }

    public class MonsterDataV2Formatter : Formatter<global::MonsterDataV2>
    {
        public override int? GetLength()
        {
            return null;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::MonsterDataV2 value)
        {
            var segment = value as IZeroFormatterSegment;
            if (segment != null)
            {
                return segment.Serialize(ref bytes, offset);
            }
            else if (value == null)
            {
                BinaryUtil.WriteInt32(ref bytes, offset, -1);
                return 4;
            }
            else
            {
                var startOffset = offset;

                offset += (8 + 4 * (5 + 1));
                offset += ObjectSegmentHelper.SerializeFromFormatter<string>(ref bytes, startOffset, offset, 0, value.Name);
                offset += ObjectSegmentHelper.SerializeFromFormatter<uint>(ref bytes, startOffset, offset, 1, value.HitPoint);
                offset += ObjectSegmentHelper.SerializeFromFormatter<float>(ref bytes, startOffset, offset, 2, value.HitRate);
                offset += ObjectSegmentHelper.SerializeFromFormatter<uint>(ref bytes, startOffset, offset, 3, value.Speed);
                offset += ObjectSegmentHelper.SerializeFromFormatter<uint>(ref bytes, startOffset, offset, 4, value.Luck);
                offset += ObjectSegmentHelper.SerializeFromFormatter<uint>(ref bytes, startOffset, offset, 5, value.Defense);

                return ObjectSegmentHelper.WriteSize(ref bytes, startOffset, offset, 5);
            }
        }

        public override global::MonsterDataV2 Deserialize(ref byte[] bytes, int offset, DirtyTracker tracker, out int byteSize)
        {
            byteSize = BinaryUtil.ReadInt32(ref bytes, offset);
            if (byteSize == -1)
            {
                byteSize = 4;
                return null;
            }
            return new MonsterDataV2ObjectSegment(tracker, new ArraySegment<byte>(bytes, offset, byteSize));
        }
    }

    public class MonsterDataV2ObjectSegment : global::MonsterDataV2, IZeroFormatterSegment
    {
        static readonly int[] __elementSizes = new int[]{ 0, 4, 4, 4, 4, 4 };

        readonly ArraySegment<byte> __originalBytes;
        readonly DirtyTracker __tracker;
        readonly int __binaryLastIndex;
        readonly byte[] __extraFixedBytes;

        readonly CacheSegment<string> _Name;

        // 0
        public override string Name
        {
            get
            {
                return _Name.Value;
            }
            set
            {
                _Name.Value = value;
            }
        }

        // 1
        public override uint HitPoint
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<uint>(__originalBytes, 1, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<uint>(__originalBytes, 1, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 2
        public override float HitRate
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<float>(__originalBytes, 2, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<float>(__originalBytes, 2, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 3
        public override uint Speed
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<uint>(__originalBytes, 3, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<uint>(__originalBytes, 3, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 4
        public override uint Luck
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<uint>(__originalBytes, 4, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<uint>(__originalBytes, 4, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }

        // 5
        public override uint Defense
        {
            get
            {
                return ObjectSegmentHelper.GetFixedProperty<uint>(__originalBytes, 5, __binaryLastIndex, __extraFixedBytes, __tracker);
            }
            set
            {
                ObjectSegmentHelper.SetFixedProperty<uint>(__originalBytes, 5, __binaryLastIndex, __extraFixedBytes, value, __tracker);
            }
        }


        public MonsterDataV2ObjectSegment(DirtyTracker dirtyTracker, ArraySegment<byte> originalBytes)
        {
            var __array = originalBytes.Array;

            this.__originalBytes = originalBytes;
            this.__tracker = dirtyTracker = dirtyTracker.CreateChild();
            this.__binaryLastIndex = BinaryUtil.ReadInt32(ref __array, originalBytes.Offset + 4);

            this.__extraFixedBytes = ObjectSegmentHelper.CreateExtraFixedBytes(this.__binaryLastIndex, 5, __elementSizes);

            _Name = new CacheSegment<string>(__tracker, ObjectSegmentHelper.GetSegment(originalBytes, 0, __binaryLastIndex, __tracker));
        }

        public bool CanDirectCopy()
        {
            return !__tracker.IsDirty;
        }

        public ArraySegment<byte> GetBufferReference()
        {
            return __originalBytes;
        }

        public int Serialize(ref byte[] targetBytes, int offset)
        {
            if (__extraFixedBytes != null || __tracker.IsDirty)
            {
                var startOffset = offset;
                offset += (8 + 4 * (5 + 1));

                offset += ObjectSegmentHelper.SerializeCacheSegment<string>(ref targetBytes, startOffset, offset, 0, _Name);
                offset += ObjectSegmentHelper.SerializeFixedLength<uint>(ref targetBytes, startOffset, offset, 1, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<float>(ref targetBytes, startOffset, offset, 2, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<uint>(ref targetBytes, startOffset, offset, 3, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<uint>(ref targetBytes, startOffset, offset, 4, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);
                offset += ObjectSegmentHelper.SerializeFixedLength<uint>(ref targetBytes, startOffset, offset, 5, __binaryLastIndex, __originalBytes, __extraFixedBytes, __tracker);

                return ObjectSegmentHelper.WriteSize(ref targetBytes, startOffset, offset, 5);
            }
            else
            {
                return ObjectSegmentHelper.DirectCopyAll(__originalBytes, ref targetBytes, offset);
            }
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
namespace ZeroFormatter.DynamicObjectSegments
{
    using global::System;
    using global::System.Collections.Generic;
    using global::ZeroFormatter.Formatters;
    using global::ZeroFormatter.Internal;
    using global::ZeroFormatter.Segments;


    public class DataTypeVersionFormatter : Formatter<global::DataRoot.DataTypeVersion>
    {
        public override int? GetLength()
        {
            return 4;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::DataRoot.DataTypeVersion value)
        {
            return BinaryUtil.WriteInt32(ref bytes, offset, (Int32)value);
        }

        public override global::DataRoot.DataTypeVersion Deserialize(ref byte[] bytes, int offset, DirtyTracker tracker, out int byteSize)
        {
            byteSize = 4;
            return (global::DataRoot.DataTypeVersion)BinaryUtil.ReadInt32(ref bytes, offset);
        }
    }

    public class NullableDataTypeVersionFormatter : Formatter<global::DataRoot.DataTypeVersion?>
    {
        public override int? GetLength()
        {
            return 5;
        }

        public override int Serialize(ref byte[] bytes, int offset, global::DataRoot.DataTypeVersion? value)
        {
            BinaryUtil.WriteBoolean(ref bytes, offset, value.HasValue);
            if (value.HasValue)
            {
                BinaryUtil.WriteInt32(ref bytes, offset + 1, (Int32)value.Value);
            }
            else
            {
                BinaryUtil.EnsureCapacity(ref bytes, offset, offset + 5);
            }

            return 5;
        }

        public override global::DataRoot.DataTypeVersion? Deserialize(ref byte[] bytes, int offset, DirtyTracker tracker, out int byteSize)
        {
            byteSize = 5;
            var hasValue = BinaryUtil.ReadBoolean(ref bytes, offset);
            if (!hasValue) return null;

            return (global::DataRoot.DataTypeVersion)BinaryUtil.ReadInt32(ref bytes, offset + 1);
        }
    }

    public class DataTypeVersionEqualityComparer : IEqualityComparer<global::DataRoot.DataTypeVersion>
    {
        public bool Equals(global::DataRoot.DataTypeVersion x, global::DataRoot.DataTypeVersion y)
        {
            return (Int32)x == (Int32)y;
        }

        public int GetHashCode(global::DataRoot.DataTypeVersion x)
        {
            return (int)x;
        }
    }


}
#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
