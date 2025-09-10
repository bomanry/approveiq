import { Inject, Injectable, Injector } from "@angular/core";
import { ToastrService } from "ngx-toastr";

@Injectable({
    providedIn: 'root'
})
export class ToastrNotifier {

    //Build custom toastr popups here to override global settings
    constructor(@Inject(Injector) private injector: Injector) { }


    private get toastrService(): ToastrService {
        return this.injector.get(ToastrService);
    }

    public success(message: string): void {
        this.toastrService.success(message);
    }

    public error(message: string): void {
        this.toastrService.error(message);
    }
}