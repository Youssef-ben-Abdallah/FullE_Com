import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login.component';
import { RegisterComponent } from './auth/register.component';
import { CatalogComponent } from './catalog/catalog.component';
import { ProductDetailComponent } from './catalog/product-detail.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './cart/checkout.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderDetailComponent } from './orders/order-detail.component';
import { AdminCategoriesComponent } from './admin/admin-categories.component';
import { AdminSubCategoriesComponent } from './admin/admin-subcategories.component';
import { AdminProductsComponent } from './admin/admin-products.component';
import { DashboardComponent } from './analytics/dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: 'catalog', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'catalog', component: CatalogComponent },
  { path: 'product/:id', component: ProductDetailComponent },
  { path: 'cart', component: CartComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'orders/:id', component: OrderDetailComponent },
  { path: 'admin/categories', component: AdminCategoriesComponent },
  { path: 'admin/subcategories', component: AdminSubCategoriesComponent },
  { path: 'admin/products', component: AdminProductsComponent },
  { path: 'dashboard', component: DashboardComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
