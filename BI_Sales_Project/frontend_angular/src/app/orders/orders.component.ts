import { Component } from '@angular/core';

@Component({
  selector: 'app-orders',
  template: `
    <h2>My Orders</h2>
    <table class="table">
      <tr><th>Order #</th><th>Status</th><th>Total</th></tr>
      <tr><td>ORD-001</td><td>Pending</td><td>$120</td></tr>
    </table>
  `
})
export class OrdersComponent {}
