import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartSevicesComponent } from './cart-sevices.component';

describe('CartSevicesComponent', () => {
  let component: CartSevicesComponent;
  let fixture: ComponentFixture<CartSevicesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartSevicesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CartSevicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
