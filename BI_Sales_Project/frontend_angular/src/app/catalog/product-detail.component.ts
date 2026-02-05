import { Component } from '@angular/core';

@Component({
  selector: 'app-product-detail',
  template: `
    <h2>Product Detail</h2>
    <div class="card">
      <h3>Demo Product</h3>
      <p>Description for demo product.</p>
      <p>Price: $120.00</p>
      <button>Add to Cart</button>
    </div>
  `
})
export class ProductDetailComponent {}
