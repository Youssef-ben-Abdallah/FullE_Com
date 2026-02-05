import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-products',
  template: `
    <h2>Manage Products</h2>
    <form [formGroup]="form" class="form-grid" (ngSubmit)="save()">
      <input formControlName="name" placeholder="Name">
      <input formControlName="sku" placeholder="SKU">
      <input formControlName="unitPrice" placeholder="Price" type="number">
      <input formControlName="stockQty" placeholder="Stock" type="number">
      <input formControlName="subCategoryId" placeholder="SubCategory Id" type="number">
      <button type="submit">Save</button>
    </form>
    <table class="table">
      <tr><th>Name</th><th>SKU</th><th>Price</th></tr>
      <tr><td>Demo</td><td>SKU-1</td><td>100</td></tr>
    </table>
  `
})
export class AdminProductsComponent {
  form = this.fb.group({
    name: ['', Validators.required],
    sku: ['', Validators.required],
    unitPrice: [0, Validators.required],
    stockQty: [0, Validators.required],
    subCategoryId: [1, Validators.required]
  });

  constructor(private fb: FormBuilder) {}

  save(): void {
    if (this.form.valid) {
      console.log('Save product', this.form.value);
    }
  }
}
