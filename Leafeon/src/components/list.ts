import { bindable, IEventAggregator, INode, observable } from "aurelia";

export class List {
    // list name
    public name: string;

    // data
    @bindable @observable()
    public data: any[];

    // filter
    @observable
    public filterText: string = "";
    // filtered data
    public filtered: any[];

    // selected item
    public selected: any;
    // selected items
    public multiSelected: any[] = [];


    constructor(@IEventAggregator readonly ea: IEventAggregator, @INode private element: HTMLElement) {
        this.name = element.getAttribute("name");
        ea.subscribe("externalSelect:" + this.name, (item) => this.externalSelect(item))
    }

    /** ui function */
    public addItem() {
        this.ea.publish("addItem:" + this.name)
    }

    /** ui function */
    public selectItem(item) {
        // this.multiSelect(item);
        // console.log("list select: " + item.name)
        this.selected = item;
        this.ea.publish("selectItem:" + this.name, item)
    }

    /** ui function */
    public delete() {
        let arr = [this.selected].concat(this.multiSelected);
        this.selected = null;
        this.multiSelected.length = 0;
        this.ea.publish("delete:" + this.name, arr)
    }

    /** ui function */
    public refresh() {
        this.ea.publish("refresh:" + this.name);
    }

    /** event function */
    public externalSelect(item) {
        this.selectItem(item);
    }

    /** ui function */
    public multiSelect(item) {
        if(this.multiSelected.includes(item)) {
            this.multiSelected.splice(this.multiSelected.indexOf(item), 1);
        } else {
            this.multiSelected.push(item);
        }
    }

    /** ui function */
    // public multiUnselect(item) {
    //     this.multiSelected.splice(this.multiSelected.indexOf(item), 1);
    // }


    /** called on observable */
    public dataChanged() {
        this.filterTextChanged();
    }

    /** called on observable & ui change */
    public filterTextChanged() {
        if(this.filterText == "") {
            this.filtered = this.data;
        } else {
            this.filtered = this.data.filter(i => {
                if(i._id.toString().includes(this.filterText)) return true;
                if(i.name.toLowerCase().includes(this.filterText)) return true;
            })
        }
    }

}
