import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { ToastrNotifier } from '../services/toastrNotifier.service';

@Injectable()
export class ValidationInterceptor implements HttpInterceptor {
    private requests: HttpRequest<any>[] = [];

    constructor(private loadingService: LoadingService,
        private toastrNotifier: ToastrNotifier,
        private router: Router) { }

    removeRequest(req: HttpRequest<any>) {
        const i = this.requests.indexOf(req);
        if (i >= 0) {
            this.requests.splice(i, 1);

        }
        this.loadingService.isLoading.next(this.requests.length > 0);
    }

    intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.requests.push(httpRequest);
        this.loadingService.isLoading.next(true);

        return new Observable(observer => {
            {
                const subscription = next.handle(httpRequest)
                    .subscribe(
                        (event: HttpEvent<any>) => {
                            if (event instanceof HttpResponse || event instanceof HttpErrorResponse) {
                                this.removeRequest(httpRequest);
                                observer.next(event);
                                if (httpRequest.method == "POST" && event instanceof HttpResponse)
                                    this.toastrNotifier.success("Success!");
                            }
                        },
                        (error: HttpErrorResponse) => {
                            let errorMsg = '';
                            if (error.message) {
                              errorMsg = error.message;
                            }
                            if (error.error?.errors) {
                                let errorArr: string[] = [];
                                Object.values(error.error.errors).forEach((element: any) => {
                                    if (typeof (element) === 'string')
                                        errorArr.push(element);
                                    else if (typeof (element) === 'object' && element.message)
                                        errorArr.push(element.message);
                                    else if (typeof (element) === 'object' && element.length > 0)
                                        errorArr.push(element[0]);
                                });
                                errorMsg = errorArr.join('<br>');
                            } else {
                                errorMsg = error.error?.detail ?? error.error?.title ?? "No Access.";
                            }

                            this.toastrNotifier.error(errorMsg);
                            this.removeRequest(httpRequest);
                            observer.error(error);
                        },
                        () => {
                            this.removeRequest(httpRequest);
                            observer.complete();
                        });
                return () => {
                    this.removeRequest(httpRequest);
                    subscription.unsubscribe();
                };
            }
        });
    }
}
