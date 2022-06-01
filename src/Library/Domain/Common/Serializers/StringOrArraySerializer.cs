using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Common.Serializers
{
    public class StringOrArraySerializer : SerializerBase<IEnumerable>
    {
        public override IEnumerable Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context.Reader.CurrentBsonType == BsonType.Array)
            {
                //return context.Reader.ReadRawBsonArray();
            }
            if (context.Reader.CurrentBsonType == BsonType.String)
            {
                var value = context.Reader.ReadString();
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }
            }
            context.Reader.SkipValue();
            return null;
        }

        //public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        //{
        //    if (value == null)
        //    {
        //        context.Writer.WriteNull();
        //        return;
        //    }
        //    context.Writer.WriteInt32(Convert.ToInt32(value));
        //}
    }
}
