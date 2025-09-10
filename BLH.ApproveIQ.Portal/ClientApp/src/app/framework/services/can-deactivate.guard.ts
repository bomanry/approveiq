/*import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot } from '@angular/router';

export interface CanComponentDeactivate {
    canDeactivate: () => Promise<boolean>;
}

@Injectable({
    providedIn: 'root'
})
export class CanDeactivateGuard implements CanDeactivate<CanComponentDeactivate> {
    constructor() { }

    canDeactivate(component: CanComponentDeactivate, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState: RouterStateSnapshot) {
        return component.canDeactivate ? component.canDeactivate().then(canDeactivate => {
            return canDeactivate;
        }) : true;
    }
}
*/

import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CanDeactivateGuard implements CanDeactivate<any> {
  canDeactivate(
    component: { canDeactivate?: () => Promise<boolean> }
  ): Promise<boolean> {
    return component.canDeactivate
      ? component.canDeactivate().then(result => !!result)
      : Promise.resolve(true); // fallback: allow deactivation
  }
}
