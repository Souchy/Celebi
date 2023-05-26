import { bindable } from "aurelia";

export class ModalButtons {
    public ok;
    public cancel;
}

export class Modal {
    @bindable
    public header: string = "";
    @bindable
    public draggable: boolean = false;
    @bindable
    public close: boolean = true;
    @bindable
    public footer: boolean = true;
    @bindable
    public buttons: ModalButtons;
    @bindable
    public callbackok = () => {}

    public clickOk() {
        this.callbackok();
    }

}
