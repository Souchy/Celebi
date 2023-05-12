using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util.serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.entity
{
    /// <summary>
    /// Permanent Model entity. Has a numerical ID additional to the regular entityUid
    /// </summary>
    public interface IEntityModel : IEntity
    {
        /// <summary>
        /// Numerical counter
        /// </summary>
        [BsonSerializer(typeof(IIDBsonSerializer))]
        public IID modelUid { get; set; }
    }
}
