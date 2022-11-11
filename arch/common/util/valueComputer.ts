import { Creature } from './../entity';

// instanced once per effect/value
export class ValueComputerParameters {
    public computerId: number;
    public takeFromSource: boolean;
    public takeFromTarget: boolean;
    public takeFromZone: boolean;
    public zonePattern: number;
    public parameters: number[];
}
// instanced once per activation
export class ValueComputerRed {
    public spellid;
    public effectid;

    public sourceid;
    public targetId;
    public targetsInZone: [];
}

export enum ResourceValueType {
    current,
    max,
    initial,
    missing
}
export enum CharacteristicValueType {
    current,
    initial
}

function getCreature(id: number) {
    return new Creature();
}

// instanced once (singleton)
export const computers: ((pkg: ValueComputerParameters, red: ValueComputerRed) => number)[] = [];

// just a number
export const computerBase = (pkg: ValueComputerParameters, red: ValueComputerRed) => pkg.parameters[0];
computers.push(computerBase);
// a number based on the number of targets in the zone
export const computerMultZoneNumber = (pkg: ValueComputerParameters, red: ValueComputerRed) => pkg.parameters[0] * red.targetsInZone.length;
computers.push(computerBase);

// a number based on a resource of the target
export const computerMultResourceTarget = (pkg: ValueComputerParameters, red: ValueComputerRed) => {
    let resourceId = pkg.parameters[0];
    let resourceValueType = pkg.parameters[1] as ResourceValueType;
    let mult = pkg.parameters[2];
    let resourceValue = 0; // getCreature(pkg.targetId).getResource(resourceId).getValue(resourceValueType) // idealy this ia just an interface that we DI in here
    return resourceValue * mult;
};
computers.push(computerMultResourceTarget);
// a number based on the sum of a resource of the zone
export const computerMultResourceZone = (pkg: ValueComputerParameters, red: ValueComputerRed) => {
    let resourceId = pkg.parameters[0];
    let resourceValueType = pkg.parameters[1] as ResourceValueType;
    let mult = pkg.parameters[2];
    let sumPattern = pkg.parameters[3]
    let resourceValueSum = 0; // sum getCreature(foreach t of pkg.targetsInZone).getResource(resourceId).getValue(resourceValueType) 
    return resourceValueSum * mult;
};
computers.push(computerMultResourceTarget);

// a number based on a context resource of the target (ex: lost/gained/used ap, mp...)
export const computerMultContextResourceTarget = (pkg: ValueComputerParameters, red: ValueComputerRed) => {
    let resourceId = pkg.parameters[0];
    let resourceValueType = pkg.parameters[1] as ResourceValueType;
    let mult = pkg.parameters[2];
    let context = pkg.parameters[3];
    let contextResourceValue = 0; // getCreature(pkg.targetId).getContextResource(resourceId, context).getValue(resourceValueType)
    return contextResourceValue * mult;
};
computers.push(computerMultContextResourceTarget);
// a number based on the sum of a context resource of the zone
export const computerMultContextResourceZoneSum = (pkg: ValueComputerParameters, red: ValueComputerRed) => {
    let resourceId = pkg.parameters[0];
    let resourceValueType = pkg.parameters[1] as ResourceValueType;
    let mult = pkg.parameters[2];
    let context = pkg.parameters[3];
    let contextResourceValueSum = 0; // sum getCreature(foreach t of pkg.targetsInZone).getContextResource(resourceId, context).getValue(resourceValueType)
    return contextResourceValueSum * mult;
};
computers.push(computerMultContextResourceZoneSum);

// a number based on a characteristic of the target
export const computerMultCharacteristic = (pkg: ValueComputerParameters, red: ValueComputerRed) => {
    let characteristicId = pkg.parameters[0];
    let characteristicValueType = pkg.parameters[1] as CharacteristicValueType;
    let mult = pkg.parameters[2];
    let characteristicValue = 0; // getCreature(pkg.targetId).getCharacteristic(characteristicId).getValue(characteristicValueType) 
    return characteristicValue * mult;
};
computers.push(computerMultCharacteristic);

// si on veut les transformer en classes : 
export class ComputerMultCharacteristicClass extends ValueComputerParameters {
    public characteristicId = 0 
    public characteristicValueType = CharacteristicValueType.current 
    public mult = 1 
    public compute(red: ValueComputerRed) {
        let characteristicValue = 0; // getCreature(pkg.targetId).getCharacteristic(characteristicId).getValue(characteristicValueType) 
        return characteristicValue * this.mult;
    }
}


const zoneComputer = (pkg: ValueComputerParameters, red: ValueComputerRed) => {
    switch (pkg.zonePattern) {
        case 1:
            // sum
            break;
        case 2:
            // multiply
            break;
        case 3:
            // take highest
            break;
        case 4:
            // take lowest
            break;
        case 5:
            //take first
            break;
        case 6:
            // take last
            break;
    }
}


