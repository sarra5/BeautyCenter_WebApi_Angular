import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../../../services/serviceM.service';
import { Subscription } from 'rxjs';
import { HeroComponent } from '../hero/hero.component';
import { AboutUsComponent } from '../about-us/about-us.component';
import { CategoriesComponent } from '../categories/categories.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeroComponent, AboutUsComponent, CategoriesComponent, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  constructor(public serv:ServiceService){}

  sub:Subscription|null = null
  categories:string[] = []

  ngOnInit(){
    this.sub = this.serv.getAllCategories().subscribe({
      next:(data) => {
        this.categories = data
      }
    })
  }

  trackByFn(index: number, item: string): number {
    return index; // Use index as the unique identifier
  }

  ngOnDestroy(){
    this.sub?.unsubscribe()
  }
}
