"use strict";(self.webpackChunkJolteon=self.webpackChunkJolteon||[]).push([[913],{1831:(e,t,s)=>{s.d(t,{Z:()=>r});var l=s(8081),i=s.n(l),n=s(3645),a=s.n(n)()(i());a.push([e.id,"creature {\n  display: block;\n}\ncreature h3 {\n  border-bottom: 1px solid #58585885;\n}\ncreature .titleSpacer {\n  margin-top: 20px;\n}\n",""]);const r=a},1092:(e,t,s)=>{s.d(t,{Z:()=>r});var l=s(8081),i=s.n(l),n=s(3645),a=s.n(n)()(i());a.push([e.id,".list {\n  display: flex;\n  flex-flow: row wrap;\n}\n.list spell,\n.list creature,\n.list status {\n  cursor: pointer;\n  display: flex;\n  position: relative;\n  height: 70px;\n  flex-basis: 150px;\n  max-width: 150px;\n  margin: 8px;\n  box-shadow: 3px 3px 4px var(--shadow);\n  word-wrap: break-word;\n}\n.list spell h4,\n.list creature h4,\n.list status h4 {\n  color: var(--accent1);\n}\n.list spell .title,\n.list creature .title,\n.list status .title {\n  height: 100%;\n  width: 100%;\n  border: 1px solid var(--accent1);\n  border-radius: 5px;\n}\n.list spell .titleSearch,\n.list creature .titleSearch,\n.list status .titleSearch {\n  border-top-left-radius: 0px;\n  border-bottom-left-radius: 0px;\n}\n.list spell .close,\n.list creature .close,\n.list status .close {\n  padding: 0px;\n  height: 50%;\n  border: 1px solid var(--accent1);\n  border-radius: 5px;\n  border-top-left-radius: 0px;\n  border-bottom-right-radius: 0px;\n  position: absolute;\n  right: 0px;\n  width: 32px;\n}\n.list spell .title:hover + .close,\n.list creature .title:hover + .close,\n.list status .title:hover + .close {\n  display: none;\n}\n.list spell .add,\n.list creature .add,\n.list status .add {\n  line-height: 65px;\n  padding: 0px 8px;\n  color: var(--front1);\n  background-color: var(--accent1);\n  border: 1px solid var(--accent1);\n  border-radius: 5px;\n  border-right-width: 0px;\n  border-top-right-radius: 0px;\n  border-bottom-right-radius: 0px;\n}\n",""]);const r=a},4545:(e,t,s)=>{s.d(t,{Z:()=>r});var l=s(8081),i=s.n(l),n=s(3645),a=s.n(n)()(i());a.push([e.id,".modal-dialog {\n  width: 90%;\n  max-width: 90%;\n}\n.modal-dialog .modal-content {\n  background-color: var(--bg1);\n}\n",""]);const r=a},535:(e,t,s)=>{s.d(t,{Z:()=>r});var l=s(8081),i=s.n(l),n=s(3645),a=s.n(n)()(i());a.push([e.id,"",""]);const r=a},9643:(e,t,s)=>{var l=s(3379),i=s.n(l),n=s(7795),a=s.n(n),r=s(569),d=s.n(r),o=s(3565),c=s.n(o),p=s(9216),h=s.n(p),u=s(4589),m=s.n(u),b=s(1092),g={};g.styleTagTransform=m(),g.setAttributes=c(),g.insert=d().bind(null,"head"),g.domAPI=a(),g.insertStyleElement=h(),i()(b.Z,g),b.Z&&b.Z.locals&&b.Z.locals},8523:(e,t,s)=>{s.d(t,{M:()=>i});var l=s(1804);class i extends l.e{getAll(e={}){return this.request(Object.assign({path:"/api/models/creature/all",method:"GET",format:"json"},e))}getFiltered(e,t={}){return this.request(Object.assign({path:"/api/models/creature/filtered",method:"GET",body:e,type:l.z.Json,format:"json"},t))}getByString(e,t={}){return this.request(Object.assign({path:`/api/models/creature/byString/${e}`,method:"GET",format:"json"},t))}getCreature(e,t={}){return this.request(Object.assign({path:`/api/models/creature/${e}`,method:"GET",format:"json"},t))}putCreature(e,t,s={}){return this.request(Object.assign({path:`/api/models/creature/${e}`,method:"PUT",body:t,type:l.z.Json,format:"json"},s))}deleteCreature(e,t={}){return this.request(Object.assign({path:`/api/models/creature/${e}`,method:"DELETE",format:"json"},t))}postNew(e={}){return this.request(Object.assign({path:"/api/models/creature/new",method:"POST",format:"json"},e))}putSpells(e,t,s={}){return this.request(Object.assign({path:`/api/models/creature/${e}/spells`,method:"PUT",body:t,type:l.z.Json,format:"json"},s))}}},4913:(e,t,s)=>{s.r(t),s.d(t,{Creature:()=>V});var l={};s.r(l),s.d(l,{default:()=>M,dependencies:()=>Z,name:()=>I,register:()=>R,template:()=>P});var i={};s.r(i),s.d(i,{CreatureStats:()=>G});var n={};s.r(n),s.d(n,{default:()=>z,dependencies:()=>F,name:()=>D,register:()=>_,template:()=>L});var a=s(655),r=s(1542),d=s(3379),o=s.n(d),c=s(7795),p=s.n(c),h=s(569),u=s.n(h),m=s(3565),b=s.n(m),g=s(9216),v=s.n(g),f=s(4589),x=s.n(f),y=s(1831),S={};S.styleTagTransform=x(),S.setAttributes=b(),S.insert=u().bind(null,"head"),S.domAPI=p(),S.insertStyleElement=v(),o()(y.Z,S),y.Z&&y.Z.locals&&y.Z.locals;var k=s(1478),w=s(2736),C=s(5142),j=s(535),U={};U.styleTagTransform=x(),U.setAttributes=b(),U.insert=u().bind(null,"head"),U.domAPI=p(),U.insertStyleElement=v(),o()(j.Z,U),j.Z&&j.Z.locals&&j.Z.locals;var O=s(1662),E=s(3362),A=s(4249),T=s(4045);const I="creaturestats",P='\n\n\n\n\n\n<div style="display: flex; width: 100%;" if.bind="stats">\n\n  \x3c!-- column --\x3e\n  <div style="width: 33%;">\n    <h5>Resource</h5>\n    <tablesimple characs.bind="Characteristics.resourcesCreatureModel" stats.bind="stats"></tablesimple>\n    <h5>Other</h5>\n    <tablesimple characs.bind="Characteristics.others" stats.bind="stats"></tablesimple>\n  </div>\n  \n  \x3c!-- column --\x3e\n  <div style="width: 33%;">\n    <h5>Affinity</h5>\n    <tablesimple characs.bind="Characteristics.affinities" stats.bind="stats"></tablesimple>\n    <h5>State</h5>\n    <tablebool characs.bind="Characteristics.states" stats.bind="stats"></tablebool>\n  </div>\n\n  <div style="width: 33%;">\n    <h5>Resistance</h5>\n    <tablesimple characs.bind="Characteristics.resistances" stats.bind="stats"></tablesimple>\n  </div>\n\n</div>\n',M=P,Z=[O,E,A,T];let q;function R(e){q||(q=r.b_N.define({name:I,template:P,dependencies:Z})),e.register(q)}var $=s(9344),J=s(2074),N=s(5746);s(1932);let G=class{constructor(e,t){this.ea=e,this.statsController=t,this.Characteristics=N.Mt,this.stats=null,e.subscribe("stat:base:change",(e=>{this.postUpdate(this.stats,e)}))}async binding(){let e=await this.statsController.getStats(this.statsuid);this.stats=e.data}async postUpdate(e,t){const s=parseInt(t.value.toString(),10);let l;l=isNaN(s)?this.statsController.putBool(e.entityUid,t):this.statsController.putSimple(e.entityUid,t);try{(await l).data.matchedCount>0?this.ea.publish("operation:saved"):this.ea.publish("operation:failed")}catch(e){this.ea.publish("operation:failed")}}};(0,a.gn)([r.ExJ,(0,a.w6)("design:type",String)],G.prototype,"statsuid",void 0),G=(0,a.gn)([(0,r.MoW)(l),(0,$.f3)($.Rp,J.s),(0,a.w6)("design:paramtypes",[Object,J.s])],G);var B=s(9990);const D="creature",L='\n\n\n\n\n\n\x3c!-- <div if.bind="model"> --\x3e\n\n  <template if.bind="isvignette">\n    <button class="title btn-outline" click.trigger="clickCreature()">\n      <stringcomponent uid.bind="model.nameId" editable.bind="!isvignette"></stringcomponent>\n      \x3c!-- view-model.ref="name"  --\x3e\n      \x3c!-- ${name.entity.value} --\x3e\n      \x3c!-- <au-slot></au-slot> --\x3e\n    </button>\n  \n    \x3c!-- delete & confirm --\x3e\n    <button class="btn close" data-bs-toggle="modal" data-bs-target="#deleteCreature-${model.entityUid}">x</button>\n    <modal id="deleteCreature-${model.entityUid}" header="Delete Creature?" close.bind=false footer.bind=true callbackok.bind="() => clickRemove()">\n      Confirm delete ${name.entity.value}?\n    </modal>\n  </template>\n  \n  \n  <template else>\n    <h3>Creature #${model.modelUid}</h3>\n    <div class="d-flex">\n      <stringcomponent uid.bind="model.nameId" editable=true></stringcomponent>\n      <stringcomponent uid.bind="model.descriptionId" editable=true large=true class="large"></stringcomponent>\n      \x3c!-- view-model.ref="name"\n           view-model.ref="desc" --\x3e\n    </div>\n  \n  \n    <h3 class="titleSpacer">Skins</h3>\n    \x3c!-- <spelllist mode="creature" spellids.bind="model.spellIds" creatureid.bind="model.modelUid" \n        callbackadd.bind="s => onAddSpell(s)" callbackremove.bind="s => onRemoveSpell(s)"\n    ></spelllist> --\x3e\n\n    <h3 class="titleSpacer">Spells</h3>\n    <spelllist mode="creature" spellids.bind="model.spellIds" creatureid.bind="model.modelUid" \n        callbackadd.bind="s => onAddSpell(s)" callbackremove.bind="s => onRemoveSpell(s)"\n    ></spelllist>\n  \n    <h3 class="titleSpacer">Passives</h3>\n    <statuslist mode="creature" passiveids.bind="model.statusPassiveIds"\n      callbackadd.bind="s => onAddPassive(s)" callbackremove.bind="s => onRemovePassive(s)"\n    ></statuslist>\n  \n    <h3 class="titleSpacer">Stats</h3>\n      <creaturestats statsuid.bind="model.statsId"></creaturestats>  \x3c!-- growthuid.bind="model.growthStats" --\x3e\n\n    \n    \x3c!-- TODO un stats mini pour les affinities, un pour les resources, etc? \n      p-e aussi un paramètre statsmini pour qu\'il affiche tous les champs de characsAllowed, même s\'il n\'existent pas dans le dictionnaire \n      serait pratique pour les SpellCost p.ex.\n    --\x3e\n    \x3c!-- Mini\n    <statsmini statsuid.bind="model.statsId" characsallowed.bind="Characteristics.creaturesSectioned" hasadddelete.bind="true" hasgrowth.bind="true" showall.bind="true"></statsmini> --\x3e\n  </template>\n  \n\n\x3c!-- </div> --\x3e\n',z=L,F=[k,w,C,i,B];let W;function _(e){W||(W=r.b_N.define({name:D,template:L,dependencies:F})),e.register(W)}var K=s(9561),Y=s(8523),H=s(1804);class Q extends H.e{getSkinAll(e={}){return this.request(Object.assign({path:"/api/models/creature/skin/all",method:"GET",format:"json"},e))}getSkin(e,t={}){return this.request(Object.assign({path:`/api/models/creature/skin/${e}`,method:"GET",format:"json"},t))}putSkin(e,t,s={}){return this.request(Object.assign({path:`/api/models/creature/skin/${e}`,method:"PUT",body:t,type:H.z.Json,format:"json"},s))}deleteSkin(e,t={}){return this.request(Object.assign({path:`/api/models/creature/skin/${e}`,method:"DELETE",format:"json"},t))}getSkin2(e,t,s={}){return this.request(Object.assign({path:`/api/models/creature/skin/${e}`,method:"GET",query:t,format:"json"},s))}postSkin(e={}){return this.request(Object.assign({path:"/api/models/creature/skin",method:"POST",format:"json"},e))}}let V=class{constructor(e,t,s,l){this.ea=e,this.router=t,this.creatureController=s,this.skinController=l,this.Enums=N.Yb,this.Characteristics=N.Mt,this.isvignette=!1}binding(){}async loading(e,t,s){this.uid=e.uid;try{let e=await this.creatureController.getCreature(this.uid);this.model=e.data,console.log("nav to new creature"),this.ea.publish("navcrumb:spell",null),this.ea.publish("navcrumb:status",null),this.ea.publish("navcrumb:creature",{modeluid:this.model.modelUid,nameuid:this.model.nameId})}catch(e){this.router.load("editor")}}clickCreature(){this.router.load("/editor/creature/"+this.model.modelUid)}clickRemove(){this.creatureController.deleteCreature(this.model.modelUid)}async clickNewSkin(){let e=await this.skinController.postSkin();this.model.skinIds.push(e.data.entityUid),this.creatureController.putCreature(this.model.modelUid,this.model)}onAddSpell(e){console.log("creature onAddSpell: "+e.modelUid),this.model.spellIds.push(e.entityUid),this.creatureController.putSpells(this.model.modelUid,this.model.spellIds)}onRemoveSpell(e){let t=this.model.spellIds.indexOf(e.entityUid);-1!=t&&(this.model.spellIds.splice(t,1),this.creatureController.putSpells(this.model.modelUid,this.model.spellIds))}onAddStatus(e){console.log("creature onAddStatus: "+e.modelUid),this.model.statusPassiveIds.push(e.entityUid),this.creatureController.putCreature(this.model.modelUid,this.model)}onRemoveStatus(e){let t=this.model.statusPassiveIds.indexOf(e.entityUid);-1!=t&&(this.model.statusPassiveIds.splice(t,1),this.creatureController.putCreature(this.model.modelUid,this.model))}};(0,a.gn)([r.ExJ,(0,a.w6)("design:type",Object)],V.prototype,"model",void 0),(0,a.gn)([r.ExJ,(0,a.w6)("design:type",Boolean)],V.prototype,"isvignette",void 0),V=(0,a.gn)([(0,r.MoW)(n),(0,$.f3)($.Rp,K.v5,Y.M,Q),(0,a.w6)("design:paramtypes",[Object,Object,Y.M,Q])],V)},2736:(e,t,s)=>{s.r(t),s.d(t,{SpellList:()=>M});var l={};s.r(l),s.d(l,{default:()=>C,dependencies:()=>j,name:()=>k,register:()=>O,template:()=>w});var i=s(655),n=s(1542),a=s(3379),r=s.n(a),d=s(7795),o=s.n(d),c=s(569),p=s.n(c),h=s(3565),u=s.n(h),m=s(9216),b=s.n(m),g=s(4589),v=s.n(g),f=s(4545),x={};x.styleTagTransform=v(),x.setAttributes=u(),x.insert=p().bind(null,"head"),x.domAPI=o(),x.insertStyleElement=b(),r()(f.Z,x),f.Z&&f.Z.locals&&f.Z.locals;var y=s(1708),S=s(1478);s(9643);const k="spelllist",w='\n\n\n\n\x3c!-- creature spells --\x3e\n<div if.bind="mode == \'creature\'">\n\n  \x3c!-- Button trigger modal --\x3e\n  <button type="button" class="btn listMenu" data-bs-toggle="modal" data-bs-target="#spellSearchModal">Add Spell</button>\n  <button class="btn listMenu" click.trigger="clickCreate()">New Spell</button>\n\n  \x3c!-- Modal --\x3e\n  <modal id="spellSearchModal" header="Spell select" close.bind=false footer.bind=false>\n    <spelllist mode="search" callbackadd.bind="s => clickAddToCreature(s)"></spelllist>\n  </modal>\n\n  \x3c!-- creature spell list --\x3e\n  <div class="list">\n    <spell repeat.for="spell of spells" view-model.ref="refs[$index]" mode.bind="mode" model.bind="spell" isvignette.bind="true" callbackremove.bind="s => clickRemove(s)"></spell>\n  </div>\n\n</div>\n\n\n\x3c!-- root or search --\x3e\n<div if.bind="mode == \'root\' || mode == \'search\'">\n\n  <div>\n    <button class="btn listMenu" click.trigger="refresh()">Refresh</button>\n    <button class="btn listMenu" click.trigger="clickCreate()">New Spell</button>\n    <input type="string" value.bind="filter" change.trigger="onSearch()" placeholder="search..." />\n  </div>\n\n  \x3c!-- root --\x3e\n  <div if.bind="mode == \'root\'" class="list">\n    <spell repeat.for="spell of filteredSpells" view-model.ref="refs[$index]" mode.bind="mode" model.bind="spell" isvignette.bind="true" callbackremove.bind="s => clickRemove(s)"></spell>\n  </div>\n  \x3c!-- search (add to creature) --\x3e\n  <div if.bind="mode == \'search\'" class="list">\n    <spell repeat.for="spell of filteredSpells" view-model.ref="refs[$index]" mode.bind="mode" model.bind="spell" isvignette.bind="true" data-bs-dismiss="modal" callbackadd.bind="callbackadd"></spell>\n  </div>\n\n</div>\n',C=w,j=[y,S];let U;function O(e){U||(U=n.b_N.define({name:k,template:w,dependencies:j})),e.register(U)}var E=s(5599),A=s(9344),T=s(6892),I=s(9561),P=s(8523);s(1932);let M=class{constructor(e,t,s,l){this.ea=e,this.router=t,this.spellController=s,this.creatureController=l,this.mode="root",this.spellids=[],this.callbackremove=e=>{},this.callbackadd=e=>{},this.spells=[],this.filteredSpells=[],this.selectedSpells=[],this.filter="",this.refs=[],this.numPerPage=50,this.page=0}async binding(){await this.refresh()}async refresh(){if(!this.mode)return;let e;"root"==this.mode&&this.ea.publish("navcrumb:spell",null),this.page,this.numPerPage,this.numPerPage,e="creature"==this.mode?this.spellController.getList({list:this.spellids}):this.spellController.getAll();try{let t=await e;this.spells=t.data,this.filteredSpells=[...this.spells]}catch(e){E.FN.create({title:"Spells",message:"Failed to fetch spells from server",status:E.vM.DANGER,timeout:2e3})}}async clickCreate(){let e=await this.spellController.postNew();this.spells.push(e.data),this.filteredSpells.push(e.data),this.callbackadd(e.data)}onSearch(){if(!this.filter)return void(this.filteredSpells=[...this.spells]);let e=this.filter.toLowerCase();this.spellController.getByString(e).then((e=>{this.filteredSpells=e.data}))}async clickAddToCreature(e){console.log("spell list click add (creature) "+e.modelUid),this.spellids.push(e.entityUid),this.spells.push(e),"creature"==this.mode&&this.callbackadd(e)}async clickRemove(e){let t=this.spells.findIndex((t=>t.modelUid==e.modelUid));-1!=t&&("creature"==this.mode&&(this.callbackremove(e),this.spells.splice(t,1),this.filteredSpells.splice(t,1)),"root"==this.mode&&this.spellController.deleteSpell(e.modelUid).then((e=>{this.spells.splice(t,1),this.filteredSpells.splice(t,1)})))}};(0,i.gn)([n.ExJ,(0,i.w6)("design:type",String)],M.prototype,"mode",void 0),(0,i.gn)([n.ExJ,(0,i.w6)("design:type",Array)],M.prototype,"spellids",void 0),(0,i.gn)([n.ExJ,(0,i.w6)("design:type",String)],M.prototype,"creatureid",void 0),(0,i.gn)([n.ExJ,(0,i.w6)("design:type",Object)],M.prototype,"callbackremove",void 0),(0,i.gn)([n.ExJ,(0,i.w6)("design:type",Object)],M.prototype,"callbackadd",void 0),M=(0,i.gn)([(0,n.MoW)(l),(0,A.f3)(A.Rp,I.v5,T.K,P.M),(0,i.fM)(1,I.v5),(0,i.w6)("design:paramtypes",[Object,Object,T.K,P.M])],M)}}]);