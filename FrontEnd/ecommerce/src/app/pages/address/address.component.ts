import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Address } from 'src/app/models/address.model';
import { UserDetailService } from 'src/app/services/user-detail.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css']
})
export class AddressComponent {

  address: Address = {
    userName: '',
    fullName: '',
    email: '',
    phoneNo: '',
    addressLine1: '',
    addressLine2: '',
    city: '',
    state: '',
    postalCode: '',
    country: '',
    alterNamePhoneNo: '',
    createdAt: ''
  };

  @Output() addressSaved = new EventEmitter<any>();

  addressForm: FormGroup;
  userName: string | null = '';
  constructor(private fb: FormBuilder, private userDetailService: UserDetailService, private router: Router) {
    this.addressForm = this.fb.group({
      fullName: ['', Validators.required],
      mobile: ['', [Validators.required, Validators.pattern('[0-9]{10}')]],
      addressLine1: ['', Validators.required],
      addressLine2: [''],
      city: ['', Validators.required],
      state: ['', Validators.required],
      pincode: ['', [Validators.required, Validators.pattern('[0-9]{6}')]]
    });
  }

  saveAddress() {
    if (this.addressForm.valid) {
      this.userName = localStorage.getItem('userName');
      this.address.userName = this.userName!;
      this.address.fullName = this.addressForm.value.fullName;
      this.address.phoneNo = this.addressForm.value.mobile;
      this.address.email = this.addressForm.value.email;
      this.address.addressLine1 = this.addressForm.value.addressLine1;
      this.address.addressLine2 = this.addressForm.value.addressLine2;
      this.address.city = this.addressForm.value.city;
      this.address.state = this.addressForm.value.state;
      this.address.postalCode = this.addressForm.value.pincode;
      this.address.country = 'India';
      this.address.alterNamePhoneNo = '';
      this.address.createdAt = new Date().toISOString();

      this.addressSaved.emit(this.address);
      
    } else {
      this.addressForm.markAllAsTouched();
    }
  }
  // saveAddress() {
    
  //   this.userName = localStorage.getItem('userName');
  //   this.address.userName = this.userName!;
  //   this.address.fullName = this.addressForm.value.fullName;
  //   this.address.phoneNo = this.addressForm.value.mobile;
  //   this.address.email = this.addressForm.value.email;
  //   this.address.addressLine1 = this.addressForm.value.addressLine1;
  //   this.address.addressLine2 = this.addressForm.value.addressLine2;
  //   this.address.city = this.addressForm.value.city;
  //   this.address.state = this.addressForm.value.state;
  //   this.address.postalCode = this.addressForm.value.pincode;
  //   this.address.country = 'India';
  //   this.address.alterNamePhoneNo = '';
  //   this.address.createdAt = new Date().toISOString();

  //   if (this.userName == null) {
  //     alert('Please login to save address');
  //     return;
  //   }

  //   if (this.addressForm.valid){
  //     this.userDetailService.saveAddress(this.address).subscribe({
  //     next: (res) => {
  //       console.log('Address saved successfully', res);
  //       alert('Address saved successfully');
  //       this.router.navigate(['/checkout']);
  //     },
  //     error: (err) => {
  //       console.error('Error saving address', err);
  //     }
  //   });
  //   }
  //   else{
  //     this.addressForm.markAllAsTouched();     
  //   }
    
  // }
}
