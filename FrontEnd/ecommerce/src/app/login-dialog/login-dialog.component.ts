import { Component, OnInit } from '@angular/core';
import {ChangeDetectionStrategy} from '@angular/core';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css'],
   changeDetection: ChangeDetectionStrategy.OnPush
  
})
export class LoginDialogComponent implements OnInit {

  user: string = '';
  password: string = '';
  loginValid: boolean = true;
  year: number = new Date().getFullYear();
  constructor() { }

  ngOnInit(): void {
  }
  login(): void {
    this.loginValid = true;
  }

}
