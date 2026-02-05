import { Component } from '@angular/core';

@Component({
  selector: 'app-catalog',
  template: `
    <h2>Catalog</h2>
    <div class="form-grid">
      <input placeholder="Search">
      <select>
        <option>All Categories</option>
      </select>
      <select>
        <option>All Subcategories</option>
      </select>
      <div class="form-grid">
        <input placeholder="Min Price" type="number">
        <input placeholder="Max Price" type="number">
      </div>
      <button>Apply Filters</button>
    </div>
    <div class="card-grid">
      <div class="card" *ngFor="let product of products">
        <h4>{{ product.name }}</h4>
        <p>SKU: {{ product.sku }}</p>
        <p>{{ product.price | currency }}</p>
        <button>View</button>
      </div>
    </div>
  `
})
export class CatalogComponent {
  products = [
    { name: 'Demo Product', sku: 'SKU-001', price: 100 }
  ];
}
