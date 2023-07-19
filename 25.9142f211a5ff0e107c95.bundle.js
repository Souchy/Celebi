"use strict";(self.webpackChunkJolteon=self.webpackChunkJolteon||[]).push([[25],{8867:(t,e,s)=>{s.d(e,{t:()=>i});var n=s(1804);class i extends n.e{getAll(t={}){return this.request(Object.assign({path:"/api/models/status/all",method:"GET",format:"json"},t))}getFiltered(t,e={}){return this.request(Object.assign({path:"/api/models/status/filtered",method:"GET",body:t,type:n.z.Json,format:"json"},e))}getByString(t,e={}){return this.request(Object.assign({path:`/api/models/status/byString/${t}`,method:"GET",format:"json"},e))}getStatus(t,e={}){return this.request(Object.assign({path:`/api/models/status/${t}`,method:"GET",format:"json"},e))}putStatus(t,e,s={}){return this.request(Object.assign({path:`/api/models/status/${t}`,method:"PUT",body:e,type:n.z.Json,format:"json"},s))}deleteStatus(t,e={}){return this.request(Object.assign({path:`/api/models/status/${t}`,method:"DELETE",format:"json"},e))}postNew(t={}){return this.request(Object.assign({path:"/api/models/status/new",method:"POST",format:"json"},t))}putAddEffect(t,e,s={}){return this.request(Object.assign({path:`/api/models/status/${t}/addEffect/${e}`,method:"PUT",format:"json"},s))}putRemoveEffect(t,e={}){return this.request(Object.assign({path:`/api/models/status/removeEffect/${t}`,method:"PUT",format:"json"},e))}}},8025:(t,e,s)=>{s.r(e),s.d(e,{Status:()=>S});var n={};s.r(n),s.d(n,{default:()=>b,dependencies:()=>p,name:()=>u,register:()=>v,template:()=>h});var i=s(655),a=s(1542),o=s(5142),d=s(9522),l=s(7915),r=s(8142),c=s(1478),m=s(9218);const u="status",h='\n\n\n\n\n\n\n\n\x3c!-- vignette root || creature --\x3e\n<template if.bind="isvignette && mode != \'search\'">\n  \x3c!-- <a href="/editor/status/{model.modelUid}"> --\x3e\n  <button class="btn title btn-outline" click.trigger="clickStatus()">\n    <stringcomponent view-model.ref="name" uid.bind="model.nameId" editable.bind="!isvignette"></stringcomponent>\n  </button>\n  \x3c!-- <button class="btn close" click.trigger="clickRemove()">x</button> --\x3e\n\n  \x3c!-- remove passive from creature --\x3e\n  <template if.bind="mode == \'creature\'">\n    <button class="btn close" data-bs-toggle="modal" data-bs-target="#removeStatus-${model.entityUid}">x</button>\n    <modal id="removeStatus-${model.entityUid}" header="Remove Status?" close.bind=false footer.bind=true callbackok.bind="() => clickRemove()">\n      Confirm remove ${name.entity.value} from creature?\n    </modal>\n  </template>\n\n  \x3c!-- delete status entirely --\x3e\n  <template if.bind="mode == \'root\'">\n    <button class="btn close" data-bs-toggle="modal" data-bs-target="#deleteStatus-${model.entityUid}">x</button>\n    <modal id="deleteStatus-${model.entityUid}" header="Delete Status?" close.bind=false footer.bind=true callbackok.bind="() => clickRemove()">\n      Confirm delete ${name.entity.value} entirely?\n    </modal>\n  </template>\n</template>\n\n\n\x3c!-- vignette search --\x3e\n<template if.bind="isvignette && mode == \'search\'">\n  <div class="add">+</div>\n  <button class="title titleSearch btn-outline" click.trigger="clickStatus()">\n    <stringcomponent view-model.ref="name" uid.bind="model.nameId" editable.bind="!isvignette"></stringcomponent>\n  </button>\n</template>\n\n\x3c!-- full status ui, not vignette --\x3e\n<template if.bind="!isvignette">\n  <div>\n    <h2>Status #${model.modelUid}</h2>\n    <div class="d-flex">\n      <stringcomponent uid.bind="model.nameId" editable=true></stringcomponent>\n      <stringcomponent uid.bind="model.descriptionId" editable=true large=true class="large"></stringcomponent>\n    </div>\n  </div>\n\n  \x3c!-- status properties --\x3e\n  <div>\n    <h4>Stats</h4>\n    \x3c!-- showall=true --\x3e\n    <statsmini statsuid.bind="model.statsId" \n        callbacksavestat.bind="() => onChange()" \n        characsallowed.bind="Characteristics.statusModels"\n        hasadddelete.bind="true" hasgrowth.bind="true"\n    ></statsmini>\n  </div>\n\n  \x3c!-- children section --\x3e\n  <effectlist effectids.bind="model.effectIds" modaluid.bind="model.entityUid" callbacksave.bind="() => onSave()"></effectlist>\n\n</template>\n',b=h,p=[o,d,l,r,c,m];let g;function v(t){g||(g=a.b_N.define({name:u,template:h,dependencies:p})),t.register(g)}var f=s(9344),y=s(9561),x=s(8867),k=s(5746);s(1932);let S=class{constructor(t,e,s){this.ea=t,this.router=e,this.statusController=s,this.Characteristics=k.Mt,this.mode="root",this.isvignette=!1,this.callbackremove=t=>{},this.callbackadd=t=>{}}binding(){}async loading(t,e,s){let n=t.uid;try{let t=await this.statusController.getStatus(n);this.model=t.data,console.log("nav to new status"),this.ea.publish("navcrumb:spell",null),this.ea.publish("navcrumb:creature",null),this.ea.publish("navcrumb:status",{modeluid:this.model.modelUid,nameuid:this.model.nameId})}catch(t){this.router.load("editor")}}clickStatus(){console.log("click status: "+this.mode),"root"!=this.mode&&"creature"!=this.mode||(console.log("click status "+this.model.entityUid),this.router.load("/editor/status/"+this.model.entityUid)),"search"==this.mode&&this.callbackadd(this.model)}clickRemove(){console.log("status click remove"),this.callbackremove(this.model)}onSave(){this.statusController.putStatus(this.model.entityUid,this.model).then((t=>this.model=t.data)).then((t=>this.ea.publish("operation:saved")))}};(0,i.gn)([a.ExJ,(0,i.w6)("design:type",String)],S.prototype,"mode",void 0),(0,i.gn)([a.ExJ,(0,i.w6)("design:type",Boolean)],S.prototype,"isvignette",void 0),(0,i.gn)([a.ExJ,(0,i.w6)("design:type",Object)],S.prototype,"callbackremove",void 0),(0,i.gn)([a.ExJ,(0,i.w6)("design:type",Object)],S.prototype,"callbackadd",void 0),(0,i.gn)([a.ExJ,(0,i.w6)("design:type",Object)],S.prototype,"model",void 0),S=(0,i.gn)([(0,a.MoW)(n),(0,f.f3)(f.Rp,y.v5,x.t),(0,i.w6)("design:paramtypes",[Object,Object,x.t])],S)}}]);