import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  template: `
    <h2>Register</h2>
    <form [formGroup]="form" class="form-grid" (ngSubmit)="register()">
      <input formControlName="email" placeholder="Email">
      <input formControlName="password" type="password" placeholder="Password">
      <button type="submit">Register</button>
    </form>
  `
})
export class RegisterComponent {
  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  constructor(private fb: FormBuilder) {}

  register(): void {
    if (this.form.valid) {
      console.log('Register', this.form.value);
    }
  }
}
