import { Component, OnInit } from '@angular/core';

import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { MatDialogRef } from '@angular/material/dialog';


@Component({

  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css'],

})
export class LoginDialogComponent implements OnInit {

  user: string = '';
  password: string = '';
  loggedIn = false;
  constructor(
     private authService: AuthService, private router: Router,
    private dialogRef: MatDialogRef<LoginDialogComponent>) {}
  
  ngOnInit(): void {
    console.log("LoginDialogComponent initialized");
   this.loggedIn = this.authService.isLoggedIn();
  }
  signOut(): void {
    this.authService.logout();
    this.loggedIn = false;
  }
  onLogin() {
    
    this.authService.login(this.user, this.password)
      .subscribe({       
        next: (res) => {
          // store token / user data
          localStorage.setItem('token', res.token);
          localStorage.setItem('userName', this.user);
          this.dialogRef.close(); // closes login popup
          this.loggedIn = true;
          // navigate → login component is destroyed
          this.router.navigate(['/home']);
        },
        error: err => alert('Login failed')
        
      });
  }
}


