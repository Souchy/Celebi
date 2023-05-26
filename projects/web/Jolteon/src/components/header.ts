import { IEventAggregator, inject } from "aurelia";

@inject(IEventAggregator)
export class Header {


    public activecreature: { modeluid, nameuid } = null
    public activespell: { modeluid, nameuid } = null
    // public activecreature = {
    //     // modeluid: 5, //"editor/creature/5",
    //     // nameid: 2 //"Lancelot"
    // }
    // public activespell = {
    //     url: "editor/spell/19",
    //     name: "Gungnir"
    // }
    

    constructor(
        private readonly ea: IEventAggregator,
    ) {
        let theme = this.themeDark;
        this.themeDark = theme;
        this.ea.subscribe("navcrumb:creature", (c: any) => this.activecreature = c);
        this.ea.subscribe("navcrumb:spell", (s: any) => this.activespell = s);
    }

    public changeTheme() {
        this.themeDark = !this.themeDark;
    }

    public get themeDark(): boolean {
        if(localStorage.getItem("theme") === null) this.themeDark = true;
        let theme = localStorage.getItem("theme");
        return theme == "true";
    }
    public set themeDark(dark: boolean) {
        localStorage.setItem("theme", dark.toString())
        if (this.themeDark)
            document.documentElement.className = "theme-dark"
        else
            document.documentElement.className = "theme-light"
    }


    public goBack() {
        history.back();
    }
    public goForward() {
        history.forward();
    }

}
