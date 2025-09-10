export type valueOf<T> = T[keyof T];

export function nameOf<T, V extends T[keyof T]>(f: (x: T) => V): valueOf<{ [K in keyof T]: T[K] extends V ? K : never }>;
export function nameOf(f: (x: any) => any): keyof any {
  let p = new Proxy({}, {
    get: (_target, key) => key
  })
  return f(p);
}