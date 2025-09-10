import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import {Observable} from 'rxjs';
import {map, take} from "rxjs/operators";
import {RoleService} from "../../services/role.services";
import {Roles} from "../../data/data/identity/models/roles.enum";

@Injectable({
  providedIn: 'root'
})
export class IsAdminGuard implements CanActivate {
  constructor(private router: Router, private roleService: RoleService) { };

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      return this.roleService.getLoggedInRole().then(role => {
        return role.role == Roles[Roles.Administrator] || role.role === Roles[Roles.DistrictAdmin]
      });
  }
}
