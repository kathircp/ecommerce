import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',  
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  error = '';
  returnUrl = '/';

  loginForm = this.fb.group({
    username: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.returnUrl =
      this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    const { username, password } = this.loginForm.value;

    if (this.authService.login(username!, password!)) {
      console.log('UserName', username)
      localStorage.setItem('userName', username!);
      this.router.navigateByUrl(this.returnUrl);
    } else {
      this.error = 'Invalid username or password';
    }
  }
}
