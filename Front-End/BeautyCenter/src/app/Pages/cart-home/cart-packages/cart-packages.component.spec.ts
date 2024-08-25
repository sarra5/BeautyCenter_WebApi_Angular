import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartPackagesComponent } from './cart-packages.component';

describe('CartPackagesComponent', () => {
  let component: CartPackagesComponent;
  let fixture: ComponentFixture<CartPackagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartPackagesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CartPackagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
