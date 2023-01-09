
export class Header {
    
    constructor() {
        let theme = this.themeDark;
        this.themeDark = theme;
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

}
