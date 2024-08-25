import { Component, OnInit } from '@angular/core';
import { PackageService } from '../../../../services/package.service';
import { Router } from '@angular/router';
import { Package } from '../../../../_model/package';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-package-add',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './package-add.component.html',
  styleUrl: './package-add.component.css'
})
export class PackageAddComponent{
 constructor(public packageservice:PackageService,public router:Router){}

 newPackage:Package=new Package(0,"",0,[]);
 Save(){
  this.packageservice.AddPackage(this.newPackage).subscribe(d=>{
    this.router.navigateByUrl('/Package');console.log(d);      

  })
 }

}
