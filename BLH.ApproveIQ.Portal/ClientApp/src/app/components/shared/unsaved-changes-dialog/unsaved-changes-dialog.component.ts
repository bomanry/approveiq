import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'unsaved-changes-dialog',
  templateUrl: './unsaved-changes-dialog.component.html',
  styleUrls: ['./unsaved-changes-dialog.component.scss']
})
export class UnsavedChangesDialogComponent {
  constructor(private activeModal: NgbActiveModal) { }

  public close(result: boolean) {
    this.activeModal.close(result);
  }
}
