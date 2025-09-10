import { KeyValuePair } from "../models/keyValuePair";

export function kvpFromEnum<T extends object>(obj: T): KeyValuePair[] {
    return Object.values(obj).filter(m => typeof (m) === 'string').map((m: string | T) => {
        let kvp = new KeyValuePair();
        kvp.label = m.toString();
        kvp.value = obj[m as keyof T] as string;
        return kvp;
    });
}

export function kvpFromArray<T extends object>(obj: T[],
    labelProperty: (arg0: T) => string,
    valueProperty: (arg0: T) => string): KeyValuePair[] {
    return obj.map((t: T) => {
        let kvp = new KeyValuePair();
        kvp.label = labelProperty(t);
        kvp.value = valueProperty(t);
        return kvp;
    });
}