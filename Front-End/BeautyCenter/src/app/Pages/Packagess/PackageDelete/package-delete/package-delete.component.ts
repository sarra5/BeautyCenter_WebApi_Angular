import { Component, OnInit } from '@angular/core';
import { PackageService } from '../../../../services/package.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-package-delete',
  standalone: true,
  imports: [],
  templateUrl: './package-delete.component.html',
  styleUrl: './package-delete.component.css'
})
export class PackageDeleteComponent implements OnInit{
  constructor(public packageservice:PackageService, public activatedroutes:ActivatedRoute,public router:Router){}

  ngOnInit(): void {
  this.activatedroutes.params.subscribe(p=>{
    this.packageservice.DeletePackageById(p['id']).subscribe(d=>{
      console.log(d);
      this.router.navigateByUrl("/Package")
    })
  })
}
}
