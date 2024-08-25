import { Component, OnInit } from '@angular/core';
import { ServiceUser } from '../../../_model/service-user';
import { PackageUserService } from '../../../services/package-user.service';
import { ServiceUserService } from '../../../services/service-user.service';
import { Service } from '../../../_model/service';
import { ServiceService } from '../../../services/serviceM.service';
import { forkJoin } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { PriceCountService } from '../../../services/price-count.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-sevices',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart-sevices.component.html',
  styleUrls: ['./cart-sevices.component.css']
})
export class CartSevicesComponent implements OnInit {

  servUserM: ServiceUser[] = [];
  ServM: Service[] = [];
  userId!: number;
  priceCounter: number = 0;
  Pr:number=0;

  constructor(
    public serviceUse: ServiceUserService,
    public priceService: PriceCountService,
    public servServ: ServiceService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.userId = +params['userId'];

      this.serviceUse.getByUserId(this.userId).subscribe(data => {
        this.servUserM = data;

        console.log(this.servUserM);

        const requests = this.servUserM.map(element => {
          return this.servServ.getById(element.serviceId);
        });

        forkJoin(requests).subscribe((responses: Service[]) => {
          responses.forEach((response, index) => {
            this.servUserM[index].serviceInfo = response;
            this.priceCounter += +this.servUserM[index].serviceInfo.price; // Accumulate price
          });
          this.priceService.setPriceCounter(this.priceCounter); // Update the total price once
          console.log("price is", this.priceCounter);
        });
      });
    });
  }

  deletePackage(userId: number, serviceId: number): void {
    const foundUser = this.servUserM.find(item => item.userId === userId && item.serviceId === serviceId);
    
    if (foundUser) {
      // If foundUser is not undefined, proceed with deleting and reducing price
      const price: number = foundUser.serviceInfo?.price ?? 0;
      this.priceService.reducePrice(price);
      
      this.serviceUse.deleteById(userId, serviceId).subscribe(
        response => {
          console.log('Deleted successfully', response);
          // Remove the item from the array if deletion is successful
          this.servUserM = this.servUserM.filter(item => !(item.userId === userId && item.serviceId === serviceId));
          // Update the price counter
        },
        error => {
          console.error('Error deleting package', error);
        }
      );
    } else {
      console.error('ServiceUser not found for the specified userId and serviceId');
    }
  }
  

}
