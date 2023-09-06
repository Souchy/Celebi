"use strict";(self.webpackChunkJolteon=self.webpackChunkJolteon||[]).push([[708],{791:(e,t,n)=>{n.d(t,{Z:()=>l});var s=n(8081),a=n.n(s),i=n(3645),o=n.n(i)()(a());o.push([e.id,"",""]);const l=o},621:(e,t,n)=>{n.d(t,{Z:()=>l});var s=n(8081),a=n.n(s),i=n(3645),o=n.n(i)()(a());o.push([e.id,"creature {\n  display: block;\n}\ncreature h3 {\n  border-bottom: 1px solid #58585885;\n}\ncreature .titleSpacer {\n  margin-top: 20px;\n}\nspell .mainSpellProperties {\n  display: flex;\n}\nspell .mainSpellProperties > div {\n  margin-right: 15px;\n}\n",""]);const l=o},9825:(e,t,n)=>{n.d(t,{Z:()=>l});var s=n(8081),a=n.n(s),i=n(3645),o=n.n(i)()(a());o.push([e.id,"",""]);const l=o},7237:(e,t,n)=>{n.d(t,{Z:()=>l});var s=n(8081),a=n.n(s),i=n(3645),o=n.n(i)()(a());o.push([e.id,"",""]);const l=o},4495:(e,t,n)=>{n.d(t,{Z:()=>l});var s=n(8081),a=n.n(s),i=n(3645),o=n.n(i)()(a());o.push([e.id,"tablebool {\n  width: 100%;\n}\ntablebool input {\n  width: 110px;\n}\n",""]);const l=o},7575:(e,t,n)=>{n.d(t,{Z:()=>l});var s=n(8081),a=n.n(s),i=n(3645),o=n.n(i)()(a());o.push([e.id,"tablesimple {\n  width: 100%;\n}\ntablesimple input {\n  width: 110px;\n}\n",""]);const l=o},6892:(e,t,n)=>{n.d(t,{K:()=>a});var s=n(1804);class a extends s.e{getAll(e={}){return this.request(Object.assign({path:"/api/models/spell/all",method:"GET",format:"json"},e))}getList(e,t={}){return this.request(Object.assign({path:"/api/models/spell/list",method:"GET",query:e,format:"json"},t))}getFiltered(e,t={}){return this.request(Object.assign({path:"/api/models/spell/filtered",method:"GET",body:e,type:s.z.Json,format:"json"},t))}getByString(e,t={}){return this.request(Object.assign({path:`/api/models/spell/byString/${e}`,method:"GET",format:"json"},t))}getSpell(e,t={}){return this.request(Object.assign({path:`/api/models/spell/${e}`,method:"GET",format:"json"},t))}putSpell(e,t,n={}){return this.request(Object.assign({path:`/api/models/spell/${e}`,method:"PUT",body:t,type:s.z.Json,format:"json"},n))}deleteSpell(e,t={}){return this.request(Object.assign({path:`/api/models/spell/${e}`,method:"DELETE",format:"json"},t))}postNew(e={}){return this.request(Object.assign({path:"/api/models/spell/new",method:"POST",format:"json"},e))}putAddEffect(e,t,n={}){return this.request(Object.assign({path:`/api/models/spell/${e}/addEffect/${t}`,method:"PUT",format:"json"},n))}putRemoveEffect(e,t={}){return this.request(Object.assign({path:`/api/models/spell/removeEffect/${e}`,method:"PUT",format:"json"},t))}}},1708:(e,t,n)=>{n.r(t),n.d(t,{Spell:()=>he});var s={};n.r(s),n.d(s,{default:()=>N,dependencies:()=>J,name:()=>M,register:()=>U,template:()=>$});var a={};n.r(a),n.d(a,{SpellStats:()=>R});var i={};n.r(i),n.d(i,{default:()=>H,dependencies:()=>Q,name:()=>X,register:()=>ee,template:()=>Y});var o={};n.r(o),n.d(o,{Condition:()=>te});var l={};n.r(l),n.d(l,{default:()=>ae,dependencies:()=>ie,name:()=>ne,register:()=>le,template:()=>se});var d=n(655),r=n(1542),c=n(3379),p=n.n(c),h=n(7795),b=n.n(h),u=n(569),m=n.n(u),g=n(3565),v=n.n(g),f=n(9216),y=n.n(f),x=n(4589),w=n.n(x),k=n(621),S={};S.styleTagTransform=w(),S.setAttributes=v(),S.insert=m().bind(null,"head"),S.domAPI=b(),S.insertStyleElement=y(),p()(k.Z,S),k.Z&&k.Z.locals&&k.Z.locals;var E=n(1478),j=n(5142),T=n(7237),C={};C.styleTagTransform=w(),C.setAttributes=v(),C.insert=m().bind(null,"head"),C.domAPI=b(),C.insertStyleElement=y(),p()(T.Z,C),T.Z&&T.Z.locals&&T.Z.locals;var q=n(1662),I=n(3362),O=n(4249),Z=n(4045);const M="spellstats",$='\n\n\n\n\n\n<tablesimple characs.bind="Characteristics.spellModels" base.bind="base" hasheader.bind="false"></tablesimple>\n',N=$,J=[q,I,O,Z];let A;function U(e){A||(A=r.b_N.define({name:M,template:$,dependencies:J})),e.register(A)}var P=n(9344),G=n(2074),B=n(5746);n(1932);let R=class{constructor(e,t){this.ea=e,this.statsController=t,this.Characteristics=B.Mt,this.base=null,e.subscribe("stat:base:change",(e=>{this.postUpdate(this.base,e)}))}binding(){this.statsController.getStats(this.baseuid).then((e=>this.base=e.data))}async postUpdate(e,t){const n=parseInt(t.value.toString(),10);let s;s=isNaN(n)?this.statsController.putBool(e.entityUid,t):this.statsController.putSimple(e.entityUid,t);try{(await s).data.matchedCount>0?this.ea.publish("operation:saved"):this.ea.publish("operation:failed")}catch(e){this.ea.publish("operation:failed")}}};(0,d.gn)([r.ExJ,(0,d.w6)("design:type",String)],R.prototype,"baseuid",void 0),R=(0,d.gn)([(0,r.MoW)(s),(0,P.f3)(P.Rp,G.s),(0,d.w6)("design:paramtypes",[Object,G.s])],R);var _=n(7915),F=n(9522),z=n(8142),W=n(5041),D=n(9990),K=n(791),L={};L.styleTagTransform=w(),L.setAttributes=v(),L.insert=m().bind(null,"head"),L.domAPI=b(),L.insertStyleElement=y(),p()(K.Z,L),K.Z&&K.Z.locals&&K.Z.locals;const X="condition",Y='\n\x3c!-- Actor Type --\x3e\n<select>\n    <option></option>\n</select>\n\n\x3c!-- Comparator Type --\x3e\n<select>\n    <option></option>\n</select>\n\n\x3c!-- Group Type --\x3e\n<select>\n    <option></option>\n</select>\n\n\x3c!-- Children Type --\x3e\n<condition repeat.for="child of children" model.bind="child"></condition>\n',H=Y,Q=[];let V;function ee(e){V||(V=r.b_N.define({name:X,template:Y,dependencies:Q})),e.register(V)}let te=class{constructor(){}};te=(0,d.gn)([(0,r.MoW)(i),(0,d.w6)("design:paramtypes",[])],te);const ne="spell",se='\n\n\n\n\n\n\n\n\n\n\n\x3c!-- vignette root || creature --\x3e\n<template if.bind="isvignette && mode != \'search\'">\n  \x3c!-- <a href="/editor/spell/{model.modelUid}"> --\x3e\n  <button class="btn title btn-outline" click.trigger="clickSpell()">\n    <stringcomponent view-model.ref="name" uid.bind="model.nameId" editable.bind="!isvignette"></stringcomponent>\n  </button>\n  \x3c!-- <button class="btn close" click.trigger="clickRemove()">x</button> --\x3e\n\n  \x3c!-- delete & confirm --\x3e\n  <template if.bind="mode == \'creature\'">\n    <button class="btn close" data-bs-toggle="modal" data-bs-target="#removeSpell-${model.entityUid}">x</button>\n    <modal id="removeSpell-${model.entityUid}" header="Remove Spell?" close.bind=false footer.bind=true callbackok.bind="() => clickRemove()">\n      Confirm remove ${name.entity.value} from creature?\n    </modal>\n  </template>\n\n  <template if.bind="mode == \'root\'">\n    <button class="btn close" data-bs-toggle="modal" data-bs-target="#deleteSpell-${model.entityUid}">x</button>\n    <modal id="deleteSpell-${model.entityUid}" header="Delete Spell?" close.bind=false footer.bind=true callbackok.bind="() => clickRemove()">\n      Confirm delete ${name.entity.value} entirely?\n    </modal>\n  </template>\n</template>\n\n\x3c!-- vignette search --\x3e\n<template if.bind="isvignette && mode == \'search\'">\n  <div class="add">+</div>\n  <button class="title titleSearch btn-outline" click.trigger="clickSpell()">\n    <stringcomponent view-model.ref="name" uid.bind="model.nameId" editable.bind="!isvignette"></stringcomponent>\n  </button>\n</template>\n\n\x3c!-- full spell ui, not vignette --\x3e\n<template if.bind="!isvignette">\n  <h2>Spell #${model.modelUid}</h2>\n  <div class="d-flex">\n    <stringcomponent view-model.ref="name" uid.bind="model.nameId" editable=true></stringcomponent>\n    <stringcomponent view-model.ref="desc" uid.bind="model.descriptionId" editable=true large=true class="large"></stringcomponent>\n  </div>\n  <div>\n    <button class="btn" click.trigger="clickFileExplorer">File</button>\n    <input type="text" value.bind="model.icon" style="width: 200px;" change.trigger="save()" />\n  </div>\n  <label class="btn" for="inputId">file dialog</label>\n  <input id="inputId" type="file" style="position: fixed; top: -100em" files.bind="iconfile" change.trigger="f => clickFileIcon(f)">\n  \x3c!-- <div>\n    ${filesStr}\n  </div> --\x3e\n\n  \x3c!-- costs + stats --\x3e\n  <div class="mainSpellProperties">\n    <div>\n      <h4>Costs</h4>\n      <statsmini idsuffix.bind="\'costs\'" statsuid.bind="model.statsId" characsallowed.bind="Characteristics.resourcesSpellCosts" hasadddelete.bind="true" hasgrowth.bind="true"></statsmini>\n      \x3c!-- <table class="table-striped table-sm table-borderless table-responsive-sm">\n        <tbody>\n          <tr repeat.for="charac of Characteristics.resourcesSpellCosts">\n            <td>${charac.baseName}</td>\n            <td><input type="number" value.bind="model.costs[charac.id]" change.trigger="onChangeCost(charac.id)" placeholder="0" /></td>\n          </tr>\n        </tbody>\n      </table> --\x3e\n    </div>\n    \x3c!-- Stats --\x3e\n    <div>\n      <h4>Stats</h4>\n      \x3c!-- <spellstats baseuid.bind="model.statsId"></spellstats> --\x3e\n      <statsmini idsuffix.bind="\'stats\'" statsuid.bind="model.statsId" characsallowed.bind="Characteristics.spellModels" hasadddelete.bind="true" hasgrowth.bind="true"></statsmini>\n    </div>\n    \x3c!-- Range --\x3e\n    <div>\n      <h4>Range</h4>\n      <h6 class="clickable" data-bs-toggle="collapse" data-bs-target="#collapseZone-rangeMin">Zone Min</h6>\n      <zone zone.bind="model.rangeZoneMin" uid="rangeMin" callbacksave.bind="() => save()"></zone>\n\n      <h6 class="clickable" data-bs-toggle="collapse" data-bs-target="#collapseZone-rangeMax">Zone Max</h6>\n      <zone zone.bind="model.rangeZoneMax" uid="rangeMax" callbacksave.bind="() => save()"></zone>\n    </div>\n    \x3c!-- Conditions --\x3e\n    <div>\n      <h5>Conditions</h5>\n      <h6>Caster Condition</h6>\n      <condition model.bind="model.sourceCondition" callbacksave.bind="() => save()"></condition>\n      <h6>Target Filter</h6>\n      <condition model.bind="model.targetFilter" callbacksave.bind="() => save()"></condition>\n    </div>\n  </div>\n\n  \x3c!-- effects --\x3e\n  <effectlist if.bind="model.effectIds" effectids.bind="model.effectIds" modaluid.bind="model.entityUid" callbacksave.bind="() => save()"></effectlist>\n\n\n</template>\n',ae=se,ie=[E,j,a,_,F,z,W,D,o];let oe;function le(e){oe||(oe=r.b_N.define({name:ne,template:se,dependencies:ie})),e.register(oe)}var de=n(6892),re=n(9561),ce=n(1804);class pe extends ce.e{getSkinAll(e={}){return this.request(Object.assign({path:"/api/models/spell/skin/all",method:"GET",format:"json"},e))}getSkin(e,t={}){return this.request(Object.assign({path:`/api/models/spell/skin/${e}`,method:"GET",format:"json"},t))}putSkin(e,t,n={}){return this.request(Object.assign({path:`/api/models/spell/skin/${e}`,method:"PUT",body:t,type:ce.z.Json,format:"json"},n))}deleteSkin(e,t={}){return this.request(Object.assign({path:`/api/models/spell/skin/${e}`,method:"DELETE",format:"json"},t))}getSkin2(e,t,n={}){return this.request(Object.assign({path:`/api/models/spell/skin/${e}`,method:"GET",query:t,format:"json"},n))}postSkin(e={}){return this.request(Object.assign({path:"/api/models/spell/skin",method:"POST",format:"json"},e))}}let he=class{constructor(e,t,n,s){this.ea=e,this.router=t,this.spellController=n,this.skinController=s,this.Characteristics=B.Mt,this.isvignette=!1,this.mode="root",this.callbackremove=e=>{},this.callbackadd=e=>{}}binding(){}async loading(e,t,n){this.uid=e.uid;try{let e=await this.spellController.getSpell(this.uid);this.model=e.data,this.ea.publish("navcrumb:status",null),this.ea.publish("navcrumb:spell",{modeluid:this.model.modelUid,nameuid:this.model.nameId})}catch(e){this.router.load("editor")}}clickFileIcon(e){this.model.icon=this.iconfile[0].name,this.save()}onChangeCost(e){this.save()}clickSpell(){"root"!=this.mode&&"creature"!=this.mode||this.router.load("/editor/spell/"+this.model.modelUid),"search"==this.mode&&this.callbackadd(this.model)}async clickRemove(){this.callbackremove(this.model)}async clickNewSkin(){let e=await this.skinController.postSkin();this.model.skinIds.push(e.data.entityUid),this.save()}save(){this.spellController.putSpell(this.model.modelUid,this.model).then((e=>{this.model=e.data,this.ea.publish("operation:saved")}))}};(0,d.gn)([r.ExJ,(0,d.w6)("design:type",Object)],he.prototype,"model",void 0),(0,d.gn)([r.ExJ,(0,d.w6)("design:type",Boolean)],he.prototype,"isvignette",void 0),(0,d.gn)([r.ExJ,(0,d.w6)("design:type",String)],he.prototype,"mode",void 0),(0,d.gn)([r.ExJ,(0,d.w6)("design:type",Object)],he.prototype,"callbackremove",void 0),(0,d.gn)([r.ExJ,(0,d.w6)("design:type",Object)],he.prototype,"callbackadd",void 0),he=(0,d.gn)([(0,r.MoW)(l),(0,P.f3)(P.Rp,re.v5,de.K,pe),(0,d.w6)("design:paramtypes",[Object,Object,de.K,pe])],he)},8491:(e,t,n)=>{n.r(t),n.d(t,{Equation:()=>C});var s={};n.r(s),n.d(s,{default:()=>S,dependencies:()=>E,name:()=>w,register:()=>T,template:()=>k});var a=n(655),i=n(1542),o=n(3379),l=n.n(o),d=n(7795),r=n.n(d),c=n(569),p=n.n(c),h=n(3565),b=n.n(h),u=n(9216),m=n.n(u),g=n(4589),v=n.n(g),f=n(9825),y={};y.styleTagTransform=v(),y.setAttributes=b(),y.insert=p().bind(null,"head"),y.domAPI=r(),y.insertStyleElement=m(),l()(f.Z,y),f.Z&&f.Z.locals&&f.Z.locals;var x=n(1478);const w="equation",k='\n\n\n\x3c!-- the Y constant --\x3e\n<input type="number" value.bind="eq.functions[0].slopes[0]" />\n\n\x3c!-- edit button --\x3e\n<button class="btn btn-icon" data-bs-toggle="modal" data-bs-target="#equationModal-${characid}">\n  <i class="fa-regular fa-pen-to-square"></i>\n</button>\n\n\x3c!-- full equation editor --\x3e\n<modal id="equationModal-${characid}" header="Edit Growth Equation" close.bind=false footer.bind=false>\n  \x3c!-- add/delete function --\x3e\n  <div repeat.for="func of eq.functions">\n    \x3c!-- x range (turns range) --\x3e\n    <div>\n      <input type="number" value.bind="func.xFromIncluded" />\n      &lt= x &lt\n      <input type="number" value.bind="func.xToExcluded" />\n    </div>\n    \x3c!-- number of slopes (degree) --\x3e\n    <div>\n      degree: <input type="number" value.bind="func.slopes.length" change.trigger="event => changeSlopes(event, func)" />\n    </div>\n    \x3c!-- slopes --\x3e\n    <div>\n      <input repeat.for="slope of func.slopes" type="number" value.bind="slope" />\n    </div>\n  </div>\n</modal>\n\n',S=k,E=[x];let j;function T(e){j||(j=i.b_N.define({name:w,template:k,dependencies:E})),e.register(j)}n(1932);let C=class{constructor(){}changeSlopes(e,t){console.log("changeSlopes:"),console.log(e)}};(0,a.gn)([i.ExJ,(0,a.w6)("design:type",String)],C.prototype,"characid",void 0),(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Object)],C.prototype,"eq",void 0),C=(0,a.gn)([(0,i.MoW)(s),(0,a.w6)("design:paramtypes",[])],C)},3362:(e,t,n)=>{n.r(t),n.d(t,{Statbool:()=>u});var s={};n.r(s),n.d(s,{default:()=>d,dependencies:()=>r,name:()=>o,register:()=>p,template:()=>l});var a=n(655),i=n(1542);const o="statbool",l='\x3c!-- <containerless> --\x3e\n\n\x3c!-- <tr if.bind="false">\n  <td>\n    ${characName}\n  </td>\n  <td>\n    <input type="checkbox" checked.bind="basestat.value" change.trigger="onChangeBase" />\n  </td>\n</tr> --\x3e\n\n\x3c!-- else --\x3e\n<div style="margin: 5px;">\n  <input type="checkbox" class="btn-check" id="btn-check-outlined ${basestat.statId}" autocomplete="off" checked.bind="basestat.value" change.trigger="onChangeBase">\n  <label class="btn btn-outline shadow-none" for="btn-check-outlined ${basestat.statId}">${characName}</label><br>\n</div>\n',d=l,r=[];let c;function p(e){c||(c=i.b_N.define({name:o,template:l,dependencies:r})),e.register(c)}var h=n(9344),b=n(5746);n(1932);let u=class{constructor(e){this.ea=e}onChangeBase(){this.ea.publish("stat:base:change",this.basestat)}binding(){}get characName(){return b.Mt.getCharac(this.basestat.statId).baseName}};(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Object)],u.prototype,"basestat",void 0),u=(0,a.gn)([(0,i.MoW)(s),(0,h.f3)(h.Rp),i.r7y,(0,a.w6)("design:paramtypes",[Object])],u)},1662:(e,t,n)=>{n.r(t),n.d(t,{Statsimple:()=>m});var s={};n.r(s),n.d(s,{default:()=>r,dependencies:()=>c,name:()=>l,register:()=>h,template:()=>d});var a=n(655),i=n(1542),o=n(8491);const l="statsimple",d='\n<tr>\n  <td>\n    ${characName} ${basestats.entityUid}\n  </td>\n  <td>\n    <input type="number" value.bind="basestat.value" change.trigger="onChangeBase" />\n  </td>\n  <td if.bind="growthequation">\n    \x3c!-- <input type="number" value.bind="growthstat.value" change.trigger="onChangGrowth" /> --\x3e\n    <equation characid.bind="basestats.statId" eq.bind="growthequation"></equation>\n  </td>\n</tr>\n',r=d,c=[o];let p;function h(e){p||(p=i.b_N.define({name:l,template:d,dependencies:c})),e.register(p)}var b=n(9344),u=n(5746);n(1932);let m=class{constructor(e){this.ea=e}onChangeBase(){this.ea.publish("stat:base:change",this.basestat)}onChangGrowth(){this.ea.publish("stat:growth:change",this.growthequation)}binding(){}get characName(){return u.Mt.getCharac(this.basestat.statId).baseName}};(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Object)],m.prototype,"basestat",void 0),(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Object)],m.prototype,"growthequation",void 0),m=(0,a.gn)([(0,i.MoW)(s),(0,b.f3)(b.Rp),i.r7y,(0,a.w6)("design:paramtypes",[Object])],m)},4045:(e,t,n)=>{n.r(t),n.d(t,{Tablebool:()=>O});var s={};n.r(s),n.d(s,{default:()=>E,dependencies:()=>j,name:()=>k,register:()=>C,template:()=>S});var a=n(655),i=n(1542),o=n(3379),l=n.n(o),d=n(7795),r=n.n(d),c=n(569),p=n.n(c),h=n(3565),b=n.n(h),u=n(9216),m=n.n(u),g=n(4589),v=n.n(g),f=n(4495),y={};y.styleTagTransform=v(),y.setAttributes=b(),y.insert=p().bind(null,"head"),y.domAPI=r(),y.insertStyleElement=m(),l()(f.Z,y),f.Z&&f.Z.locals&&f.Z.locals;var x=n(3362),w=n(8491);const k="tablebool",S='\n\n\n<div style="display: flex; flex-wrap: wrap;">\n  <statbool repeat.for="charac of characs" basestat.bind="getBaseStat(charac.id)"></statbool>\n</div>\n\n\n<div>\n  <equation repeat.for="charac of characs" characid.bind="charac" eq.bind="getGrowthEquation(charac.id)"></equation>\n</div>\n',E=S,j=[x,w];let T;function C(e){T||(T=i.b_N.define({name:k,template:S,dependencies:j})),e.register(T)}var q=n(9344),I=n(5746);n(1932);let O=class{constructor(e){this.ea=e}bound(){}getBaseStat(e){var t,n,s;return(null===(s=null===(n=null===(t=this.stats)||void 0===t?void 0:t.base)||void 0===n?void 0:n.dic)||void 0===s?void 0:s.hasOwnProperty(e))?this.stats.base.dic[e]:{statId:e,value:!1}}getGrowthEquation(e){var t,n,s;return(null===(s=null===(n=null===(t=this.stats)||void 0===t?void 0:t.growth)||void 0===n?void 0:n.dic)||void 0===s?void 0:s.hasOwnProperty(e))?this.stats.growth.dic[e]:{functions:[{xFromIncluded:I.gT.MAX_INT,xToExcluded:I.gT.MIN_INT,slopes:[0]}]}}};(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Array)],O.prototype,"characs",void 0),(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Object)],O.prototype,"stats",void 0),O=(0,a.gn)([(0,i.MoW)(s),(0,q.f3)(q.Rp),(0,a.w6)("design:paramtypes",[Object])],O)},4249:(e,t,n)=>{n.r(t),n.d(t,{Tablesimple:()=>I});var s={};n.r(s),n.d(s,{default:()=>S,dependencies:()=>E,name:()=>w,register:()=>T,template:()=>k});var a=n(655),i=n(1542),o=n(3379),l=n.n(o),d=n(7795),r=n.n(d),c=n(569),p=n.n(c),h=n(3565),b=n.n(h),u=n(9216),m=n.n(u),g=n(4589),v=n.n(g),f=n(7575),y={};y.styleTagTransform=v(),y.setAttributes=b(),y.insert=p().bind(null,"head"),y.domAPI=r(),y.insertStyleElement=m(),l()(f.Z,y),f.Z&&f.Z.locals&&f.Z.locals;var x=n(1662);const w="tablesimple",k='\n\n<table class="table-striped table-sm table-borderless table-responsive-sm" if.bind="stats">\n  <thead if.bind="hasheader">\n    <tr>\n      <th></th>\n      <th>Base</th>\n      <th>Growth</th>\n       \x3c!-- if.bind="growth" --\x3e\n    </tr>\n  </thead>\n  <tbody>\n    <tr as-element="statsimple" repeat.for="charac of characs" basestat.bind="getBaseStat(charac.id)" growthequation.bind="getGrowthEquation(charac.id)"></tr>\n  </tbody>\n</table>\n',S=k,E=[x];let j;function T(e){j||(j=i.b_N.define({name:w,template:k,dependencies:E})),e.register(j)}var C=n(9344),q=n(5746);n(1932);let I=class{constructor(e){this.ea=e,this.hasheader=!0,this.stats=null}bound(){}getBaseStat(e){var t,n,s;return(null===(s=null===(n=null===(t=this.stats)||void 0===t?void 0:t.base)||void 0===n?void 0:n.dic)||void 0===s?void 0:s.hasOwnProperty(e))?this.stats.base.dic[e]:{statId:e,value:0}}getGrowthEquation(e){var t,n,s;return(null===(s=null===(n=null===(t=this.stats)||void 0===t?void 0:t.growth)||void 0===n?void 0:n.dic)||void 0===s?void 0:s.hasOwnProperty(e))?this.stats.growth.dic[e]:{functions:[{xFromIncluded:q.gT.MAX_INT,xToExcluded:q.gT.MIN_INT,slopes:[0]}]}}};(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Boolean)],I.prototype,"hasheader",void 0),(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Array)],I.prototype,"characs",void 0),(0,a.gn)([i.ExJ,(0,a.w6)("design:type",Object)],I.prototype,"stats",void 0),I=(0,a.gn)([(0,i.MoW)(s),(0,C.f3)(C.Rp),(0,a.w6)("design:paramtypes",[Object])],I)}}]);