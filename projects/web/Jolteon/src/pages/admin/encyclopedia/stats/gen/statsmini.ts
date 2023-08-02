import { bindable, inject, observable } from "aurelia";
import { watch } from '@aurelia/runtime-html';
import { CharacteristicCategory, CharacteristicType, IStat, IStats, MathEquation, StatValueType, Stats } from "../../../../../jolteon/services/api/data-contracts";
import { StatsModelController } from "../../../../../jolteon/services/api/StatsModelController";
import { Characteristics, Constants, Enums } from "../../../../../jolteon/constants";

@inject(StatsModelController)
export class Statsmini {

    public Enums: Enums = Enums;
    // public stats type (StatusInstanceStats? CreatureStats? ... -> what properties are allowed?)

    @bindable
    public idsuffix = "";
    @bindable
    public characsallowed: (any | CharacteristicType)[] = Characteristics.allSectioned;
    @bindable
    public hasgrowth: boolean = false;
    @bindable
    public hasadddelete: boolean = true;
    @bindable
    public showall: boolean = false;
    @bindable
    public callbacksavestat = () => { };

    @bindable @observable
    public stats: Stats;
    @bindable
    public statsuid: string;


    constructor(private readonly statsController: StatsModelController) {

    }

    /**
     * hook
     */
    binding() {
        if (this.statsuid && !this.stats) {
            // console.log("load stats " + this.statsuid)
            this.statsController.getStats(this.statsuid).then(
                res => {
                    // console.log("statsmini got stats: ")
                    // console.log(res.data)
                    this.stats = res.data;
                },
                rej => console.log("no stats error")
            )
        }
        // console.log("statsmini allowed:")
        // console.log(this.characsallowed);
        // console.log("stats mini stats: ");
        // console.log(this.stats);
        // console.log("ref: ")
        // console.log(this.statselector)
    }

    public hasSections() {
        if (!this.characsallowed) return false;
        let hasSections = Array.isArray(this.characsallowed[0]);
        // console.log("hasSections: " + hasSections);
        // console.log(this.characsallowed);
        return hasSections;
    }

    // @watch("stats.dic")
    public get getDicValues() {
        // console.log("sdf: " + JSON.stringify(sdf))
        return Object.values(this.stats.base.dic);
    }

    public getFilteredDicKeys(characsAllowedSection: (CharacteristicType[] | CharacteristicType[][])) {
        let keys = Object.keys(this.stats?.base.dic).filter(k => k != "$type" && k != "entityUid");
        if (Array.isArray(characsAllowedSection[0])) {
            if (this.showall) {
                let sections = characsAllowedSection.map(s => s.map(c => (c as CharacteristicType).id));
                return sections;
            }
            return keys.filter(k => characsAllowedSection.some(s => s.some(c => (c as CharacteristicType).id == k)))
        } else {
            if (this.showall) {
                return characsAllowedSection.map(c => c.id);
            }
            return keys.filter(k => characsAllowedSection.some(c => (c as CharacteristicType).id == k));
        }
    }
    public get getDicKeys() {
        // console.log("getDicKeys: " + this.showall + ", " + this.stats + ", " + this.statsuid); // + ", " + JSON.stringify(this.characsallowed))
        // console.log(this.stats);
        // if (this.showall) {
        //     return this.characsallowed;
        // } else {
            if (!this.stats) {
                // console.log("return null")
                return null;
            }
            return this.getFilteredDicKeys(this.characsallowed);
        // }
    }

    public getBase(statid: string) {
        return this.stats.base.dic[statid];
    }
    public getGrowth(statId) {
        return this.stats.growth.dic[statId];
    }
    
    public getSectionName(section: CharacteristicType[]) {
        let first = section[0];
        return CharacteristicCategory[first.category];
    }
    public sectionHasValues(section: CharacteristicType[]) {
        // console.log("section has values?")
        // console.log(section)
        return section?.some(c => this.stats?.base?.dic?.hasOwnProperty(c.id) || this.stats?.growth?.dic?.hasOwnProperty(c.id))
    }

    public onChangeStatValue() {
        // console.log("Statsmini onChangeStatValue: save effect");
        this.save();
    }
    public clickRemoveStat(statid: string) {
        delete this.stats.base.dic[statid];
        // console.log("Statsmini clickRemoveStat: save effect");
        this.save();
    }

    public onAddStat(property) {
        // console.log("Statsmini.onAddStat: " + JSON.stringify(property))
        this.statsController.postStat({ characID: property.id }).then(
            res => {
                this.stats.base.dic[res.data.statId] = res.data;

                if (this.hasgrowth) {
                    let equation: MathEquation = {
                        functions: [
                            {
                                xFromIncluded: Constants.MAX_INT,
                                xToExcluded: Constants.MIN_INT,
                                slopes: [0]
                            }
                        ]
                    }
                    this.stats.growth.dic[res.data.statId] = equation; //res.data;
                }
                console.log("Statsmini bubble up callback: ")
                console.log(this.stats)
                this.save();
            }
        )
    }

    public save() {
        // console.log("statsmini save")
        this.callbacksavestat();
        if (this.statsuid) {
            // console.log("spark save stats")
            this.statsController.putStats(this.statsuid, this.stats).then(
                res => this.stats = res.data
            )
        }
    }

}
