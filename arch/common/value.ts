
export class Value<T> {
    public val: T; 
}

export class ValueMinMax<T> extends Value<T> {
    public val2: T;
    public get min() {
        return this.val
    }
    public get max() {
        return this.val2
    }
}

