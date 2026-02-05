import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-categories',
  template: `
    <h2>Manage Categories</h2>
    <form [formGroup]="form" class="form-grid" (ngSubmit)="save()">
      <input formControlName="name" placeholder="Name">
      <input formControlName="description" placeholder="Description">
      <button type="submit">Save</button>
    </form>
    <table class="table">
      <tr><th>Name</th><th>Description</th></tr>
      <tr><td>Electronics</td><td>Devices</td></tr>
    </table>
  `
})
export class AdminCategoriesComponent {
  form = this.fb.group({
    name: ['', Validators.required],
    description: ['']
  });

  constructor(private fb: FormBuilder) {}

  save(): void {
    if (this.form.valid) {
      console.log('Save category', this.form.value);
    }
  }
}
