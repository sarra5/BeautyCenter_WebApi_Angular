import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from '../../../services/local-storage.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css'
})
export class HeroComponent {
  constructor(public router:Router, private localStorageService:LocalStorageService){}

  isLogged:boolean|null = null;
  storageSub: Subscription|null = null;

  ngOnInit(){
    if(this.localStorageService.getItem("token") != null){
      this.isLogged=true
    }
    else{
      this.isLogged=false
    }

    this.storageSub = this.localStorageService.watchStorage().subscribe((key) => {
      if (key) {
        // Perform actions when a specific key changes in local storage
        if(this.localStorageService.getItem(key) != null){
          this.isLogged=true
        }
        else{
          this.isLogged=false
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
  }

  goTo(){
    this.router.navigateByUrl('/login')
  }
}
