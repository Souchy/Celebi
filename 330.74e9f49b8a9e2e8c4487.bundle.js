"use strict";(self.webpackChunkJolteon=self.webpackChunkJolteon||[]).push([[330,25],{4863:(t,e,s)=>{s.d(e,{Z:()=>l});var a=s(8081),n=s.n(a),i=s(3645),r=s.n(i)()(n());r.push([t.id,"editor > .navbar {\n  margin-left: -15px;\n  margin-top: -20px;\n  width: calc(100% + 30px);\n}\n",""]);const l=r},6841:(t,e,s)=>{s.d(e,{Z:()=>l});var a=s(8081),n=s.n(a),i=s(3645),r=s.n(i)()(n());r.push([t.id,".listMenu {\n  margin: 8px;\n}\n",""]);const l=r},8867:(t,e,s)=>{s.d(e,{t:()=>n});var a=s(1804);class n extends a.e{getAll(t={}){return this.request(Object.assign({path:"/api/models/status/all",method:"GET",format:"json"},t))}getFiltered(t,e={}){return this.request(Object.assign({path:"/api/models/status/filtered",method:"GET",body:t,type:a.z.Json,format:"json"},e))}getByString(t,e={}){return this.request(Object.assign({path:`/api/models/status/byString/${t}`,method:"GET",format:"json"},e))}getStatus(t,e={}){return this.request(Object.assign({path:`/api/models/status/${t}`,method:"GET",format:"json"},e))}putStatus(t,e,s={}){return this.request(Object.assign({path:`/api/models/status/${t}`,method:"PUT",body:e,type:a.z.Json,format:"json"},s))}deleteStatus(t,e={}){return this.request(Object.assign({path:`/api/models/status/${t}`,method:"DELETE",format:"json"},e))}postNew(t={}){return this.request(Object.assign({path:"/api/models/status/new",method:"POST",format:"json"},t))}putAddEffect(t,e,s={}){return this.request(Object.assign({path:`/api/models/status/${t}/addEffect/${e}`,method:"PUT",format:"json"},s))}putRemoveEffect(t,e={}){return this.request(Object.assign({path:`/api/models/status/removeEffect/${t}`,method:"PUT",format:"json"},e))}}},7330:(t,e,s)=>{s.r(e),s.d(e,{Editor:()=>et});var a={};s.r(a),s.d(a,{default:()=>U,dependencies:()=>P,name:()=>M,register:()=>R,template:()=>O});var n={};s.r(n),s.d(n,{CreatureList:()=>N});var i={};s.r(i),s.d(i,{default:()=>_,dependencies:()=>F,name:()=>B,register:()=>W,template:()=>G});var r={};s.r(r),s.d(r,{Statuslist:()=>H});var l={};s.r(l),s.d(l,{default:()=>V,dependencies:()=>X,name:()=>K,register:()=>tt,template:()=>Q});var o=s(655),d=s(1542),c=s(3379),u=s.n(c),h=s(7795),m=s.n(h),b=s(569),g=s.n(b),p=s(3565),f=s.n(p),v=s(9216),x=s.n(v),y=s(4589),k=s.n(y),S=s(4863),w={};w.styleTagTransform=k(),w.setAttributes=f(),w.insert=g().bind(null,"head"),w.domAPI=m(),w.insertStyleElement=x(),u()(S.Z,w),S.Z&&S.Z.locals&&S.Z.locals;var C=s(6841),j={};j.styleTagTransform=k(),j.setAttributes=f(),j.insert=g().bind(null,"head"),j.domAPI=m(),j.insertStyleElement=x(),u()(C.Z,j),C.Z&&C.Z.locals&&C.Z.locals;var E=s(4913);s(9643);const M="creaturelist",O='\n\n\n\n\n<button class="btn listMenu" click.trigger="refresh()">Refresh</button>\n<button class="btn listMenu" click.trigger="clickCreate()">New creature</button>\n\n<input type="string" value.bind="filter" change.trigger="onSearch()" placeholder="search..." />\n\n\x3c!-- todo bind creature --\x3e\n<div class="list">\n  <creature repeat.for="crea of filteredCreatures" model.bind="crea" isvignette.bind="true"></creature>\n</div>\n',U=O,P=[E];let A;function R(t){A||(A=d.b_N.define({name:M,template:O,dependencies:P})),t.register(A)}var T=s(9344),$=s(8523),J=s(9561);s(1932);let N=class{constructor(t,e,s){this.ea=t,this.router=e,this.creatureController=s,this.creatures=[],this.filteredCreatures=[],this.selectedCreatures=[],this.filter="",this.numPerPage=50,this.page=0,this.refresh()}refresh(){this.ea.publish("navcrumb:creature",null),this.page,this.numPerPage,this.numPerPage,this.creatureController.getAll().then((t=>{this.creatures=t.data,this.filteredCreatures=[...this.creatures]}))}clickCreate(){this.creatureController.postNew().then((t=>{this.creatures.push(t.data),this.filteredCreatures.push(t.data)}))}onSearch(){if(!this.filter)return void(this.filteredCreatures=[...this.creatures]);let t=this.filter.toLowerCase();this.creatureController.getByString(t).then((t=>{this.filteredCreatures=t.data}))}};N=(0,o.gn)([(0,d.MoW)(a),(0,T.f3)(T.Rp,J.v5,$.M),(0,o.fM)(1,J.v5),(0,o.w6)("design:paramtypes",[Object,Object,$.M])],N);var I=s(2736),Z=s(8025),q=s(1478);const B="statuslist",G='\n\n\n\n\x3c!-- creature status passives --\x3e\n<div if.bind="mode == \'creature\'">\n\n  \x3c!-- Button trigger modal --\x3e\n  <button type="button" class="btn listMenu" data-bs-toggle="modal" data-bs-target="#statusSearchModal">Add Status</button>\n  <button class="btn listMenu" click.trigger="clickCreate()">New Status</button>\n\n  \x3c!-- Modal --\x3e\n  <modal id="statusSearchModal" header="Status select" close.bind=false footer.bind=false>\n    <statuslist mode="search" callbackadd.bind="s => clickAddToCreature(s)"></statuslist>\n  </modal>\n\n  \x3c!-- creature status list --\x3e\n  <div class="list">\n    <status repeat.for="status of statuses" view-model.ref="refs[$index]" mode.bind="mode" model.bind="status" isvignette.bind="true" callbackremove.bind="s => clickRemove(s)"></status>\n  </div>\n\n</div>\n\n\n\x3c!-- root or search --\x3e\n<div if.bind="mode == \'root\' || mode == \'search\'">\n\n  <div>\n    <button class="btn listMenu" click.trigger="refresh()">Refresh</button>\n    <button class="btn listMenu" click.trigger="clickCreate()">New Status</button>\n    <input type="string" value.bind="filter" change.trigger="onSearch()" placeholder="search..." />\n  </div>\n\n  \x3c!-- root --\x3e\n  <div if.bind="mode == \'root\'" class="list">\n    <status repeat.for="status of filteredStatuses" model.bind="status" mode.bind="mode" isvignette.bind="true" callbackremove.bind="s => clickRemove(s)"></status>\n  </div>\n  \x3c!-- search (add to creature) --\x3e\n  <div if.bind="mode == \'search\'" class="list">\n    <status repeat.for="status of filteredStatuses" model.bind="status" mode.bind="mode" isvignette.bind="true" data-bs-dismiss="modal" callbackadd.bind="callbackadd"></status>\n  </div>\n\n</div>\n',_=G,F=[Z,q];let L;function W(t){L||(L=d.b_N.define({name:B,template:G,dependencies:F})),t.register(L)}var D=s(5599),z=s(8867);let H=class{constructor(t,e,s){this.ea=t,this.router=e,this.statusController=s,this.mode="root",this.statusids=[],this.callbackremove=t=>{},this.callbackadd=t=>{},this.statuses=[],this.filteredStatuses=[],this.filter="",this.numPerPage=50,this.page=0}async binding(){console.log("status list binding"),await this.refresh()}async refresh(){if(!this.mode)return;let t;console.log("statuslist refresh: mode="+this.mode+", ids="+this.statusids),"root"==this.mode&&this.ea.publish("navcrumb:status",null),this.page,this.numPerPage,this.numPerPage,t="creature"==this.mode?this.statusController.getFiltered({_id:{in:this.statusids}}):this.statusController.getAll();try{let e=await t;this.statuses=e.data,this.filteredStatuses=[...this.statuses],console.log(this.filteredStatuses)}catch(t){D.FN.create({title:"Statuses",message:"Failed to fetch status from server",status:D.vM.DANGER,timeout:2e3})}}async clickCreate(){let t=await this.statusController.postNew();console.log("status list click create : "+JSON.stringify(t.data)),this.statuses.push(t.data),this.filteredStatuses.push(t.data),this.callbackadd(t.data)}onSearch(){if(!this.filter)return void(this.filteredStatuses=[...this.statuses]);let t=this.filter.toLowerCase();this.statusController.getByString(t).then((t=>{this.filteredStatuses=t.data}))}async clickAddToCreature(t){console.log("status list click add (creature) "+t.modelUid),this.statusids.push(t.entityUid),this.statuses.push(t),"creature"==this.mode&&this.callbackadd(t)}async clickRemove(t){let e=this.statuses.findIndex((e=>e.entityUid==t.entityUid));-1!=e&&("creature"==this.mode&&(this.callbackremove(t),this.statuses.splice(e,1),this.filteredStatuses.splice(e,1)),"root"==this.mode&&this.statusController.deleteStatus(t.entityUid).then((t=>{this.statuses.splice(e,1),this.filteredStatuses.splice(e,1)})))}};(0,o.gn)([d.ExJ,(0,o.w6)("design:type",String)],H.prototype,"mode",void 0),(0,o.gn)([d.ExJ,(0,o.w6)("design:type",Array)],H.prototype,"statusids",void 0),(0,o.gn)([d.ExJ,(0,o.w6)("design:type",Object)],H.prototype,"callbackremove",void 0),(0,o.gn)([d.ExJ,(0,o.w6)("design:type",Object)],H.prototype,"callbackadd",void 0),H=(0,o.gn)([(0,d.MoW)(i),(0,T.f3)(T.Rp,J.v5,z.t),(0,o.fM)(1,J.v5),(0,o.w6)("design:paramtypes",[Object,Object,z.t])],H);const K="editor",Q='\n\n\n\n\x3c!-- <h3>Editor</h3> --\x3e\n\n<nav class="navbar navbar-expand-lg">\n  <ul class="navbar-nav mr-auto">\n    <li class="nav-item">\n      <a click.trigger="mode = \'creature\'">Creature</a>\n    </li>\n    <li class="nav-item">\n      <a click.trigger="mode = \'spell\'">Spell</a>\n    </li>\n    <li class="nav-item">\n      <a click.trigger="mode = \'status\'">Status</a>\n    </li>\n  </ul>\n</nav>\n\n\n<creaturelist show.bind="mode == \'creature\'"></creaturelist>\n<spelllist show.bind="mode == \'spell\'"></spelllist>\n\n\x3c!-- <statuslist show.bind="mode == \'status\'"></statuslist> --\x3e\n<statuslist ></statuslist>\n\n\n\n\x3c!-- some statuses can be re-used, ex: \n    - eclat de surt \n    - infecté\n    - portail elio\n    - mot stimu/galva\n    - \n\n    these will use the schemas AddStatus + a StatusModel\n--\x3e\n\n\x3c!-- some statuses would be nice & easy to custom create in an AddStatus effect, using its children, ex:\n    - buff stat (shield, fourberie, manigance, etc)\n    - ret pa (which is buff stat too: ralentissement, sablier, horloge...)\n    - damage status (ignite, poison insi, injection, malé vaudoo, fleche tyra, )\n\n\n    these will use the schemas CreateStatus + status properties to define a new status every time (with status identifier being the spellmodel id)\n        means you still can\'t stack 2 manigance even if it comes from 2 srams, because they have the same spellmodel id\n--\x3e\n\n\x3c!-- then you could also have a button to convert a CreateStatus effect into an AddStatus + StatusModel --\x3e\n',V=Q,X=[n,I,r];let Y;function tt(t){Y||(Y=d.b_N.define({name:K,template:Q,dependencies:X})),t.register(Y)}let et=class{constructor(){this.mode="creature"}};et=(0,o.gn)([(0,d.MoW)(l)],et)},8025:(t,e,s)=>{s.r(e),s.d(e,{Status:()=>S});var a={};s.r(a),s.d(a,{default:()=>b,dependencies:()=>g,name:()=>h,register:()=>f,template:()=>m});var n=s(655),i=s(1542),r=s(5142),l=s(9522),o=s(7915),d=s(8142),c=s(1478),u=s(9218);const h="status",m='\n\n\n\n\n\n\n\n\x3c!-- vignette root || creature --\x3e\n<template if.bind="isvignette && mode != \'search\'">\n  \x3c!-- <a href="/editor/status/{model.modelUid}"> --\x3e\n  <button class="btn title btn-outline" click.trigger="clickStatus()">\n    <stringcomponent view-model.ref="name" uid.bind="model.nameId" editable.bind="!isvignette"></stringcomponent>\n  </button>\n  \x3c!-- <button class="btn close" click.trigger="clickRemove()">x</button> --\x3e\n\n  \x3c!-- remove passive from creature --\x3e\n  <template if.bind="mode == \'creature\'">\n    <button class="btn close" data-bs-toggle="modal" data-bs-target="#removeStatus-${model.entityUid}">x</button>\n    <modal id="removeStatus-${model.entityUid}" header="Remove Status?" close.bind=false footer.bind=true callbackok.bind="() => clickRemove()">\n      Confirm remove ${name.entity.value} from creature?\n    </modal>\n  </template>\n\n  \x3c!-- delete status entirely --\x3e\n  <template if.bind="mode == \'root\'">\n    <button class="btn close" data-bs-toggle="modal" data-bs-target="#deleteStatus-${model.entityUid}">x</button>\n    <modal id="deleteStatus-${model.entityUid}" header="Delete Status?" close.bind=false footer.bind=true callbackok.bind="() => clickRemove()">\n      Confirm delete ${name.entity.value} entirely?\n    </modal>\n  </template>\n</template>\n\n\n\x3c!-- vignette search --\x3e\n<template if.bind="isvignette && mode == \'search\'">\n  <div class="add">+</div>\n  <button class="title titleSearch btn-outline" click.trigger="clickStatus()">\n    <stringcomponent view-model.ref="name" uid.bind="model.nameId" editable.bind="!isvignette"></stringcomponent>\n  </button>\n</template>\n\n\x3c!-- full status ui, not vignette --\x3e\n<template if.bind="!isvignette">\n  <div>\n    <h2>Status #${model.modelUid}</h2>\n    <div class="d-flex">\n      <stringcomponent uid.bind="model.nameId" editable=true></stringcomponent>\n      <stringcomponent uid.bind="model.descriptionId" editable=true large=true class="large"></stringcomponent>\n    </div>\n  </div>\n\n  \x3c!-- status properties --\x3e\n  <div>\n    <h4>Stats</h4>\n    \x3c!-- showall=true --\x3e\n    <statsmini statsuid.bind="model.statsId" \n        callbacksavestat.bind="() => onChange()" \n        characsallowed.bind="Characteristics.statusModels"\n        hasadddelete.bind="true" hasgrowth.bind="true"\n    ></statsmini>\n  </div>\n\n  \x3c!-- children section --\x3e\n  <effectlist effectids.bind="model.effectIds" modaluid.bind="model.entityUid" callbacksave.bind="() => onSave()"></effectlist>\n\n</template>\n',b=m,g=[r,l,o,d,c,u];let p;function f(t){p||(p=i.b_N.define({name:h,template:m,dependencies:g})),t.register(p)}var v=s(9344),x=s(9561),y=s(8867),k=s(5746);s(1932);let S=class{constructor(t,e,s){this.ea=t,this.router=e,this.statusController=s,this.Characteristics=k.Mt,this.mode="root",this.isvignette=!1,this.callbackremove=t=>{},this.callbackadd=t=>{}}binding(){}async loading(t,e,s){let a=t.uid;try{let t=await this.statusController.getStatus(a);this.model=t.data,console.log("nav to new status"),this.ea.publish("navcrumb:spell",null),this.ea.publish("navcrumb:creature",null),this.ea.publish("navcrumb:status",{modeluid:this.model.modelUid,nameuid:this.model.nameId})}catch(t){this.router.load("editor")}}clickStatus(){console.log("click status: "+this.mode),"root"!=this.mode&&"creature"!=this.mode||(console.log("click status "+this.model.entityUid),this.router.load("/editor/status/"+this.model.entityUid)),"search"==this.mode&&this.callbackadd(this.model)}clickRemove(){console.log("status click remove"),this.callbackremove(this.model)}onSave(){this.statusController.putStatus(this.model.entityUid,this.model).then((t=>this.model=t.data)).then((t=>this.ea.publish("operation:saved")))}};(0,n.gn)([i.ExJ,(0,n.w6)("design:type",String)],S.prototype,"mode",void 0),(0,n.gn)([i.ExJ,(0,n.w6)("design:type",Boolean)],S.prototype,"isvignette",void 0),(0,n.gn)([i.ExJ,(0,n.w6)("design:type",Object)],S.prototype,"callbackremove",void 0),(0,n.gn)([i.ExJ,(0,n.w6)("design:type",Object)],S.prototype,"callbackadd",void 0),(0,n.gn)([i.ExJ,(0,n.w6)("design:type",Object)],S.prototype,"model",void 0),S=(0,n.gn)([(0,i.MoW)(a),(0,v.f3)(v.Rp,x.v5,y.t),(0,n.w6)("design:paramtypes",[Object,Object,y.t])],S)}}]);