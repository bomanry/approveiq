import { Injectable } from '@angular/core';

export interface District {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  public selectedDistrict: District | null = null;

  constructor() { }

  setSelectedDistrict(district: District): void {
    this.selectedDistrict = district;
  }

  getSelectedDistrict(): District | null {
    return this.selectedDistrict;
  }
}