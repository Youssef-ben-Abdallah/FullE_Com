import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <h2>Analytics Dashboard</h2>
    <div class="form-grid">
      <div>
        <label>From</label>
        <input type="date">
      </div>
      <div>
        <label>To</label>
        <input type="date">
      </div>
      <div>
        <label>Top N</label>
        <input type="number" value="10">
      </div>
      <div>
        <label>Granularity</label>
        <select>
          <option value="day">Day</option>
          <option value="month">Month</option>
          <option value="year">Year</option>
        </select>
      </div>
      <button>Refresh</button>
    </div>
    <div class="card-grid">
      <div class="card">
        <h4>Total Sales</h4>
        <p>$1,250,000</p>
      </div>
      <div class="card">
        <h4>Total Orders</h4>
        <p>2,450</p>
      </div>
      <div class="card">
        <h4>Total Customers</h4>
        <p>1,120</p>
      </div>
      <div class="card">
        <h4>Avg Order Value</h4>
        <p>$510</p>
      </div>
      <div class="card">
        <h4>Avg Discount</h4>
        <p>5.2%</p>
      </div>
    </div>
    <div class="card-grid">
      <div class="card">Line Chart: Sales Over Time</div>
      <div class="card">Bar Chart: Top Products</div>
      <div class="card">Bar Chart: Top Customers</div>
      <div class="card">Pie Chart: Sales by Territory</div>
      <div class="card">Bar Chart: Sales by Ship Method</div>
    </div>
  `
})
export class DashboardComponent {}
