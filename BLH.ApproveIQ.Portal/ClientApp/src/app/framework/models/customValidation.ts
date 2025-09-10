import { AbstractControl, ValidatorFn } from '@angular/forms';

export default class CustomValidation {
  static match(controlName: string, checkControlName: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const control = controls.get(controlName);
      const checkControl = controls.get(checkControlName);

      if (checkControl?.errors && !checkControl.errors['matching']) {
        return null;
      }

      if (control?.value !== checkControl?.value) {
        controls.get(checkControlName)?.setErrors({ matching: true });
        return { matching: true };
      } else {
        return null;
      }
    };
  }

  static requiredIfCheckHasValue(controlName: string, checkControlName: string, checkControlNameValue: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const control = controls.get(controlName);
      const checkControl = controls.get(checkControlName);

      if (checkControl?.value && checkControl?.value == checkControlNameValue && !(control?.value))
        control?.setErrors({ required: true });
      else
        control?.setErrors(null);

      return null;
    };
  }
}
