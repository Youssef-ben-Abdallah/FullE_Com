import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-subcategories',
  template: `
    <h2>Manage Subcategories</h2>
    <form [formGroup]="form" class="form-grid" (ngSubmit)="save()">
      <input formControlName="name" placeholder="Name">
      <input formControlName="categoryId" placeholder="Category Id" type="number">
      <input formControlName="description" placeholder="Description">
      <button type="submit">Save</button>
    </form>
    <table class="table">
      <tr><th>Name</th><th>Category</th></tr>
      <tr><td>Laptops</td><td>Electronics</td></tr>
    </table>
  `
})
export class AdminSubCategoriesComponent {
  form = this.fb.group({
    name: ['', Validators.required],
    categoryId: [1, Validators.required],
    description: ['']
  });

  constructor(private fb: FormBuilder) {}

  save(): void {
    if (this.form.valid) {
      console.log('Save subcategory', this.form.value);
    }
  }
}
