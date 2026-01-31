import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { DataService } from '../services/data.service';

@Injectable({ providedIn: 'root' })
export class ReferenceDataResolver implements Resolve<any> {
  constructor(private dataService: DataService) {}

  resolve(): Observable<any> {
    return this.dataService.getAllByGet<any>("/TaskType")
  }
}
