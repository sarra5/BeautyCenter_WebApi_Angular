import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PriceCountService {
  private priceCounterSubject = new BehaviorSubject<number>(0);
  priceCounter$ = this.priceCounterSubject.asObservable();

  // Method to update the price
  setPriceCounter(newPrice: number) {
    const currentPrice = this.priceCounterSubject.value;
    this.priceCounterSubject.next(currentPrice + newPrice);
  }
   reducePrice(PriceDeleted:number){
    const currentPrice = this.priceCounterSubject.value;
    console.log("PriceDeleted is",{PriceDeleted})
    const CP=currentPrice-PriceDeleted;
    console.log(CP)
    this.priceCounterSubject.next(CP);

   }
  // Method to get the current price value
  getPriceCounter() {
    return this.priceCounterSubject.value;
  }
}
