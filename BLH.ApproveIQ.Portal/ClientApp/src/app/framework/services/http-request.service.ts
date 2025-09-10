import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';

import { join } from '@fireflysemantics/join';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment';
import {BaseService} from "../../services/base.service";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class HttpRequestService{
  public baseUrl: string = environment.baseUrl;

  constructor(private httpRequest: HttpClient, private baseService: BaseService) { }

  get<Type>(url: string): Observable<Type> {
    const headers = new HttpHeaders()
      .set('X-Selected-District-Id', this.baseService.selectedDistrict?.id ?? "");
    return this.httpRequest.get<Type>(join(this.baseUrl, url), {headers});
  }

  post<Type>(url: string, body: object): Observable<Type> {
    const headers = new HttpHeaders()
      .set('X-Selected-District-Id', this.baseService.selectedDistrict?.id ?? "");
    return this.httpRequest.post<Type>(join(this.baseUrl, url), body, {headers});
  }

  delete<Type>(url: string): Observable<Type> {
    const headers = new HttpHeaders()
      .set('X-Selected-District-Id', this.baseService.selectedDistrict?.id ?? "");
    return this.httpRequest.delete<Type>(join(this.baseUrl, url), {headers});
  }

  getBlob(url: string): Observable<Blob> {
    const headers = new HttpHeaders()
      .set('X-Selected-District-Id', this.baseService.selectedDistrict?.id ?? "");
    return this.httpRequest.get(join(this.baseUrl, url), { responseType: 'blob', headers })
  }

  postBlob(url: string, body: any): Observable<Blob> {
    const headers = new HttpHeaders()
      .set('X-Selected-District-Id', this.baseService.selectedDistrict?.id ?? "")
    return this.httpRequest.post(join(this.baseUrl, url), body, { responseType: 'blob', headers });
  }

  postFile<Type>(url: string, file: File): Observable<Type> {
    const formData: FormData = new FormData();

    formData.append('file', file);

    const headers = new HttpHeaders()
      .set('X-Selected-District-Id', this.baseService.selectedDistrict?.id ?? "");

    return this.httpRequest.post<Type>(join(this.baseUrl, url), formData, {headers});
  }

  postFileWithPayload<Type>(url: string, file: File, payload: any): Observable<Type> {
    const formData: FormData = new FormData();

    formData.append('File', file);
    formData.append('Payload', JSON.stringify(payload));

    const headers = new HttpHeaders()
      .set('X-Selected-District-Id', this.baseService.selectedDistrict?.id ?? "");

    return this.httpRequest.post<Type>(join(this.baseUrl, url), formData, {headers});
  }
}
