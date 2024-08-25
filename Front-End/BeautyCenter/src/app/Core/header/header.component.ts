import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { LocalStorageService } from '../../services/local-storage.service';
import { Subscription } from 'rxjs';
import * as JWTDecode from 'jwt-decode'

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor(private localStorageService: LocalStorageService, public router:Router) {}

  storageSub: Subscription|null = null;
  userName:string|null=null
  id:string|null=null
  t:string=''

  ngOnInit(): void {
    this.t = this.localStorageService.getItem("token")
    if(this.t != null){
      let r:{name:string, id:string} = JWTDecode.jwtDecode(this.t)
      this.userName = r.name
      this.id = r.id
    }
    else{
      this.userName=null
    }
    this.storageSub = this.localStorageService.watchStorage().subscribe((key) => {
      if (key) {
        // Perform actions when a specific key changes in local storage
        this.t = this.localStorageService.getItem("token")
        if(this.t != null){
          let r:{name:string, id:string} = JWTDecode.jwtDecode(this.t)
          this.userName = r.name
          this.id = r.id
        }
        else{
          this.userName=null
          this.id=null
        }
      }
    });
  }

  ngOnDestroy(): void {
    if (this.storageSub) {
      this.storageSub.unsubscribe();
    }
  }
  
  logout() {
    this.localStorageService.removeItem('token');
    this.router.navigateByUrl("/Home")
  }
}