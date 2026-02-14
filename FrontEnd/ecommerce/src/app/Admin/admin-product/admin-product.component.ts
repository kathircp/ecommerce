import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatFormField, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { IndexpageService } from 'src/app/services/indexpage.service';
import { Subscription } from 'rxjs';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckbox } from '@angular/material/checkbox';

@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatButtonModule,
    CommonModule,
    MatRadioModule,
    MatCheckbox
  ], 
  standalone:true
})
export class AdminProductComponent {

  productForm!: FormGroup;
  selectedFile!: File;
  imagePreview: string | ArrayBuffer | null = null;
  fileName: string = '';
  categories: any[] = [];
  productSubscription: Subscription | undefined; 
  pageIndex : any[] = [];
  isLoading: boolean = false;
  uploadError: string = '';
  checked = false;
  indeterminate = false;
  labelPosition: 'before' | 'after' = 'after';
  disabled = false;
  categoryName: string = '';

  constructor(private fb: FormBuilder, private http: HttpClient, private indexpageService: IndexpageService) {}

  ngOnInit() {
    this.loadCategories(); 
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      price: [0, Validators.required],
      stock: [0, Validators.required],
      categoryName: [],
      color: [''],
      discount: [],
      blouse: [false],
      newArrival: [false],
      image : [null],
      updatedBy: ['Admin']
    });
    
  }

  onFileSelected(event: any) {
    this.uploadError = '';
    const file = event.target.files[0];
    
    if (!file) return;

    // Validate file type
    const allowedTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/webp'];
    if (!allowedTypes.includes(file.type)) {
      this.uploadError = 'Please select a valid image file (JPEG, PNG, GIF, or WebP)';
      return;
    }

    // Validate file size (max 5MB)
    const maxSize = 5 * 1024 * 1024;
    if (file.size > maxSize) {
      this.uploadError = 'File size must be less than 5MB';
      return;
    }

    this.selectedFile = file;
    this.fileName = file.name;

    console.log('Selected file:', this.selectedFile);
    // Create preview
    const reader = new FileReader();
    reader.onload = (e) => {
      this.imagePreview = e.target?.result || null;
    };
    reader.readAsDataURL(file);
  }

  removeImage() {
    this.selectedFile = null!;
    this.imagePreview = null;
    this.fileName = '';
    this.uploadError = '';
  }

  onSubmit() {
    if (this.productForm.invalid) return;

    const formData = new FormData();

    formData.append('Name', this.productForm.value.name);
    formData.append('Description', this.productForm.value.description);
    formData.append('Price', this.productForm.value.price);
    formData.append('Stock', this.productForm.value.stock);
    formData.append('CategoryName', this.categoryName);
    formData.append('Color', this.productForm.value.color);
    formData.append('Discount', this.productForm.value.discount);
    formData.append('Blouse', this.productForm.value.blouse);
    formData.append('NewArrival', this.productForm.value.newArrival);
    formData.append('CreatedAt', new Date().toISOString());
    formData.append('UpdatedAt', new Date().toISOString());
    formData.append('UpdatedBy', this.productForm.value.updatedBy);

    if (this.selectedFile) {
      formData.append('Image', this.selectedFile);
    }

    console.log('Submitting product:', this.productForm.value);
    this.isLoading = true;
    this.uploadError = '';

    this.http.post('http://localhost:5179/api/ecommerce/Products', formData)
      .subscribe({
        next: (res) => {
          console.log('Saved Successfully', res);
          this.productForm.reset();
          this.removeImage();
          this.isLoading = false;
          alert('Product saved successfully');
        },
        error: (error) => {
          console.error('Error saving product', error);
          this.uploadError = error.error?.message || 'Failed to save product. Please try again.';
          this.isLoading = false;
        }
      });
  }
  loadCategories() {
    this.productSubscription= this.indexpageService.getPageIndex()
    .subscribe((_pages)=> {
      this.pageIndex = _pages;
      this.categories = [...new Set(_pages
        .map(item => item.categoryName)
        .filter(name => name != null)
      )];

    });  
  }
  getCategoryName(categoryId: any): any {
    const categories = this.pageIndex.filter(x => x.categoryId == categoryId);
    const category = categories[0];
    
    return category ? category.categoryName : 'Unknown';
  }
  filterByCategory(categoryName: string) {
    this.categoryName = categoryName;
  }
}
