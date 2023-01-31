using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace Umbreon.data.resources
{
    public class SpellModel : ISpellModel
    {
        public IID entityUid { get; set; }
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public Dictionary<ResourceType, int> costs { get; set; }
        public ISpellProperties properties { get; set; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public HashSet<IID> effectIds { get; set; }

        public SpellModel() { }
        //private SpellModel(IID id) => entityUid = id;
        public static ISpellModel Create() => new SpellModel() //Eevee.RegisterIID())
        {              
            //entityUid = Eevee.RegisterIID(),  
            //modelUid = Eevee.RegisterModel<ISpellModel>()   // type id
            entityUid = Eevee.RegisterIID<ISpellModel>(),

            nameId = Eevee.RegisterIID<string>(),         // string type id (we want continuity in i18n ids)
            descriptionId = Eevee.RegisterIID<string>(), 

            // Eevee.RegisterIID(),
            //Eevee.RegisterIID(this),
            //Eevee.RegisterIID(nameId),
            //Eevee.RegisterType(this)
            //Eevee.RegisterString()
        };


        public void Dispose()
        {
            //Eevee.DisposeIID(this);

            Eevee.DisposeIID<ISpellModel>(entityUid);
            Eevee.DisposeIID<string>(nameId);
            Eevee.DisposeIID<string>(descriptionId);
        }

    }
}
