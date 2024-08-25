import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../../../services/serviceM.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Service } from '../../../_model/service';
import { Subscription } from 'rxjs';
import { ServiceUserService } from '../../../services/service-user.service';
import { ServiceUser } from '../../../_model/service-user';
import { UserService2 } from '../../../_model/user-service2';
import Swal from 'sweetalert2';
import { LocalStorageService } from '../../../services/local-storage.service';
import * as JWTDecode from 'jwt-decode'

@Component({
  selector: 'app-services-list',
  standalone: true,
  imports: [],
  templateUrl: './services-list.component.html',
  styleUrl: './services-list.component.css'
})
export class ServicesListComponent implements OnInit{

constructor(public serviceOfServices:ServiceService, public activatedRoute:ActivatedRoute, public serviceOfUserService:ServiceUserService, public localStorage:LocalStorageService, public router:Router){}
serviceList:Service[]=[];
serviceSub: Subscription|null=null;
category:any="";
t:string="";
userService: UserService2= new UserService2(0,0,"");

ngOnInit(): void {
  this.serviceSub=this.activatedRoute.params.subscribe({
    next:(data)=>
      {
        this.serviceOfServices.getByCategory(data["category"]).subscribe({
          next:(serviceData)=>{
            this.serviceList=serviceData;
            console.log(this.serviceList);
          }
        });
      }
  })
}

addService(ServiceId:number){
  this.userService.ServiceId=ServiceId;
}

CheckLogin():boolean{
  this.t = this.localStorage.getItem("token")
  if(this.t != null){
    let r:{id:string} = JWTDecode.jwtDecode(this.t)
    this.userService.UserId =+ r.id; //+: for transform from string to number
    return true;
  }
  else{
    this.router.navigateByUrl("/login");
    return false;
  }
}

async ShowDate() {
  var check:boolean=this.CheckLogin();
  if(check==true){
      const { value: date } = await Swal.fire({
        title: "Select reservation date",
        input: "date",
        inputAttributes: {
          min: new Date().toISOString().split("T")[0]
        },
        didOpen: () => {
          const input = Swal.getInput();
          if (input) {
            const today = new Date().toISOString().split("T")[0];
            input.min = today;
          }
        }
      });
      if (date) {
        this.userService.date = date;
        Swal.fire("Your reservation added to cart", date);
        console.log(this.userService);
      this.serviceOfUserService.addServiceUser(this.userService).subscribe(d=>{
        console.log(d);
      })
      }else{
        Swal.fire("Please Enter a date");
      }
  }else{
    
  }
  
}





ngOnDestroy(){
  this.serviceSub?.unsubscribe();
}
}
