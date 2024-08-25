import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import Swal from 'sweetalert2';
import { LocalStorageService } from '../../services/local-storage.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, HttpClientModule, RouterModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form: FormGroup;

  constructor(private http: HttpClient, private formBuilder: FormBuilder, private router: Router, private localStorageService: LocalStorageService) {
    this.form = this.formBuilder.group({ 
      name: '', email: '', password: '', confirmPassword: '', bankAccount: ''
    });
  }

  login() {
    const user = this.form.getRawValue();
    // const loginUrl = `http://localhost:5240/api/Login?Email=${encodeURIComponent(user.email)}&pass=${encodeURIComponent(user.password)}`;
    const loginUrl = `https://localhost:7206/api/Login?Email=${encodeURIComponent(user.email)}&pass=${encodeURIComponent(user.password)}`;
    
    const emailRegex: RegExp = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

    if (!emailRegex.test(user.email)) {
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
    } 
    this.http.get(loginUrl,{responseType:'text'})
      .subscribe({
        next: (response) => {
          const token = response;
          if (token) {
            Swal.fire({
              icon: 'success',
              title: 'Welcome'!,
              text: 'You are logged in successfully!',
            });
            // localStorage.setItem('token', token); // Store token in localStorage
            this.localStorageService.setItem("token", token)

            this.router.navigateByUrl('/Home')
          } else {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Login failed. Please check your credentials.',
            });
          }
        },
        error: (error) => {
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: error.error.message || 'Login failed. Please try again later.',
          });
        }
      });
  }
}