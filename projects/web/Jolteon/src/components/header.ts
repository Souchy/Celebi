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
        let theme = this.themeLight;
        this.themeLight = theme;
        this.ea.subscribe("navcrumb:creature", (c: any) => this.activecreature = c);
        this.ea.subscribe("navcrumb:spell", (s: any) => this.activespell = s);
    }

    public changeTheme() {
        this.themeLight = !this.themeLight;
    }

    public get themeLight(): boolean {
        if (localStorage.getItem("jolteon:themeLight") === null) this.themeLight = false;
        let theme = localStorage.getItem("jolteon:themeLight");
        return theme == "true";
    }
    public set themeLight(dark: boolean) {
        localStorage.setItem("jolteon:themeLight", dark.toString())
        if (this.themeLight)
            document.documentElement.className = "theme-light"
        else
            document.documentElement.className = "theme-dark"
    }


    public goBack() {
        history.back();
    }
    public goForward() {
        history.forward();
    }

}
