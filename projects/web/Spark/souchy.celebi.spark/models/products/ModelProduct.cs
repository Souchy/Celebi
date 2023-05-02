using MongoDB.Bson;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// An item you can buy in the shop.
    /// It links to a model like a Spell, Creature, Skin...
    /// The modelId is then added the the owned models on the account.
    /// It costs currency.
    /// </summary>
    public class ModelProduct : Product
    {
        /// <summary>
        /// CreatureModel, SpellModel, CreatureSkin... maybe BoardSkins?
        /// </summary>
        public IID ModelID { get; set; }
    }
}
