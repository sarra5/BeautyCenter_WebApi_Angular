import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PackageService } from '../../../../services/package.service';
import { Package } from '../../../../_model/package';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-package-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './package-edit.component.html',
  styleUrl: './package-edit.component.css'
})
export class PackageEditComponent implements OnInit{
  constructor(public router:Router,public packageservice:PackageService,public activatedroute:ActivatedRoute){}
  editedPackage:Package=new Package(0,"",0,[]);

  ngOnInit(): void {
    this.activatedroute.params.subscribe(p=>{
      this.packageservice.GetPackageByID(p['id']).subscribe(d=>{
        this.editedPackage=d;
      })
    })
  }

  Save(){
    this.packageservice.EditPackage(this.editedPackage).subscribe(d=>{
      console.log(d);
      this.router.navigateByUrl("/Package")
    })
  }

}
