import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  template: `
    <h2>Your Cart</h2>
    <table class="table">
      <tr><th>Product</th><th>Qty</th><th>Price</th></tr>
      <tr><td>Demo</td><td>2</td><td>$200</td></tr>
    </table>
    <button routerLink="/checkout">Checkout</button>
  `
})
export class CartComponent {}
