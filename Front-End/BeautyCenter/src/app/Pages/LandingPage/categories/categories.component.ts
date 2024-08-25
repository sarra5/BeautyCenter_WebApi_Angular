import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent {
  @Input() category:string = ''
  @Input() photo:string = ''
  @Input() paragh:string = ''
  @Input() link:string = ''
  
  goTo() {
    if(this.link=="Package")
    this.router.navigateByUrl(`/${this.link}`)
  else{
    this.router.navigateByUrl(`/Service/${this.link}`)

  }
  }
  
  constructor(public router:Router){}
}
