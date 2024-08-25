import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  private storageSub = new BehaviorSubject<string>('');

  watchStorage(): BehaviorSubject<string> {
    return this.storageSub;
  }

  setItem(key: string, value: any): void {
    localStorage.setItem(key, value);
    this.storageSub.next(key);
  }

  getItem(key: string): any {
    const item = localStorage.getItem(key);
    return item ? item : null;
  }

  removeItem(key: string): void {
    localStorage.removeItem(key);
    this.storageSub.next(key);
  }
}
