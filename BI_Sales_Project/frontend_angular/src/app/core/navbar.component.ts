import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  template: `
    <div class="toolbar">
      <div>BI Sales</div>
      <div class="nav-links">
        <a routerLink="/catalog">Catalog</a>
        <a routerLink="/cart">Cart</a>
        <a routerLink="/orders">Orders</a>
        <a routerLink="/dashboard">Dashboard</a>
        <a routerLink="/admin/categories">Admin</a>
        <a routerLink="/login">Login</a>
      </div>
    </div>
  `
})
export class NavbarComponent {}
