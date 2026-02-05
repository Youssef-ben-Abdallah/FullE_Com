import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavbarComponent } from './core/navbar.component';
import { LoginComponent } from './auth/login.component';
import { RegisterComponent } from './auth/register.component';
import { CatalogComponent } from './catalog/catalog.component';
import { ProductDetailComponent } from './catalog/product-detail.component';
import { AdminCategoriesComponent } from './admin/admin-categories.component';
import { AdminSubCategoriesComponent } from './admin/admin-subcategories.component';
import { AdminProductsComponent } from './admin/admin-products.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './cart/checkout.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderDetailComponent } from './orders/order-detail.component';
import { DashboardComponent } from './analytics/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    CatalogComponent,
    ProductDetailComponent,
    AdminCategoriesComponent,
    AdminSubCategoriesComponent,
    AdminProductsComponent,
    CartComponent,
    CheckoutComponent,
    OrdersComponent,
    OrderDetailComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    AppRoutingModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
