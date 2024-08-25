import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, HttpClientModule, RouterModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  form: FormGroup;

  constructor(
    private http: HttpClient,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.form = this.formBuilder.group({
      id:Number,
      Name: '',
      email: '',
      password: '',
      confirmPassword: '',
      bankAccount: ''
    });
  }

  ngOnInit() {
    // Initialization logic if needed
  }

  submit() {
    const user = this.form.getRawValue();

    if (!user.email.includes('@')) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Invalid email!',
      });
      return;
    } else if (user.password.length < 8) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Password must be at least 8 characters long!',
      });
      return;
    } else if (user.password !== user.confirmPassword) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Passwords do not match!',
      });
      return;
    }

    this.http.post('http://localhost:5240/api/User', user)
      .subscribe({
        next: (response) => {
          Swal.fire({
            icon: 'success',
            title: 'Registration Successful',
            text: 'You have registered successfully!',
          });
          this.router.navigate(['/login']);
        },
        error: (error) => {
          Swal.fire({
            icon: 'error',
            title: 'Registration Failed',
            text: error.error.message || 'Registration failed. Please try again later.',
          });
        }
      });
  }
}
