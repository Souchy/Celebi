using MongoDB.Bson;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// An item you can buy in the shop.
    /// It links to a model like:
    ///     - Creature, 
    ///     - Spell, 
    ///     - Skin  
    ///         - creature + spells
    ///         - spell 
    ///         - status
    ///         - board arena
    ///         - HUD skins (like the life globe mtx in poe)
    ///         - the rings around creatures on the board to differentiate teams
    ///         - kill skins (dofus finishers)
    ///         - profile skins (profile icon, profile background...)
    ///     
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
