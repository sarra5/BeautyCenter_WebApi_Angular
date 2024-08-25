import { Component, OnInit } from '@angular/core';
import { PackageService } from '../../../../services/package.service';
import { Package } from '../../../../_model/package';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PackageUserService } from '../../../../services/package-user.service';
import { PackageUser } from '../../../../_model/package-user';
import { PackageUserr2 } from '../../../../_model/package-userr2';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { LocalStorageService } from '../../../../services/local-storage.service';
import * as JWTDecode from 'jwt-decode'


@Component({
  selector: 'app-package-list',
  standalone: true,
  imports: [RouterLink,FormsModule,],
  templateUrl: './package-list.component.html',
  styleUrl: './package-list.component.css'
})
export class PackageListComponent implements OnInit{
  constructor(private localStorage:LocalStorageService, public packgservice:PackageService,public packageuser:PackageUserService,public router:Router){}
  packges:Package[]=[];
  t:string=""
  NewPackageUser:PackageUserr2=new PackageUserr2(0,0,"","","") //


  ngOnInit(){
    this.packgservice.GetAllPackage().subscribe({
      next:(data)=>this.packges=data,
    })
  }

  CheckLogin():boolean{
    this.t = this.localStorage.getItem("token")
    if(this.t != null){
      let r:{name:string, id:string} = JWTDecode.jwtDecode(this.t)
      this.NewPackageUser.userName=r.name;
      this.NewPackageUser.userId=+r.id;
      return true;
    }
    else{
      this.router.navigateByUrl("/login");
      return false;
    }

  }

  setPackage(id:number,name:string){
    this.NewPackageUser.packageId=id;
    this.NewPackageUser.packageName=name;
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
          this.NewPackageUser.date = date;
          Swal.fire("Your reservation added to cart", date);
        this.packageuser.AddPackageUser(this.NewPackageUser).subscribe(d=>{
        })
        }else{
          Swal.fire("Please Enter a date");
        }
    }else{
      
    }
    
  }
}