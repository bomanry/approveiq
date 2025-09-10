import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpRequestService } from "../framework/services/http-request.service";
import { ValidateUserRequest } from "../data/data/b-l-h/requests/validate-user-request";
import { ValidateUserResponse } from "../data/data/b-l-h/responses/validate-user-response";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpRequest: HttpRequestService) { }

  validateUser(email: string): Observable<ValidateUserResponse> {
    const request: ValidateUserRequest = { email };
    return this.httpRequest.post<ValidateUserResponse>('users/validate', request);
  }
}