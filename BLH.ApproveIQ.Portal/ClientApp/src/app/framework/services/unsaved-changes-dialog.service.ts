import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UnsavedChangesDialogComponent } from 'src/app/components/shared/unsaved-changes-dialog/unsaved-changes-dialog.component';

@Injectable({
    providedIn: 'root'
})
export class UnsavedChangesDialogService {
    constructor(private modalService: NgbModal) { }

    public async showUnsavedDialog(): Promise<boolean> {
        return this.modalService.open(UnsavedChangesDialogComponent).result.then(res => res).catch(err => false);
    }
}
