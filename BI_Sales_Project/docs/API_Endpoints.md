# API Endpoints

## Auth
- POST `/api/auth/login`
- POST `/api/auth/register`
- POST `/api/auth/seed`

## Catalog
- GET `/api/categories`
- POST `/api/categories` (Admin)
- PUT `/api/categories/{id}` (Admin)
- DELETE `/api/categories/{id}` (Admin)
- GET `/api/categories/{id}/subcategories`
- POST `/api/subcategories` (Admin)
- PUT `/api/subcategories/{id}` (Admin)
- DELETE `/api/subcategories/{id}` (Admin)
- GET `/api/products`
- GET `/api/products/{id}`
- POST `/api/products` (Admin)
- PUT `/api/products/{id}` (Admin)
- DELETE `/api/products/{id}` (Admin)

## Cart & Orders
- GET `/api/cart`
- POST `/api/cart/items`
- PUT `/api/cart/items/{itemId}`
- DELETE `/api/cart/items/{itemId}`
- POST `/api/cart/checkout`
- GET `/api/orders/my`
- GET `/api/orders/{id}`
- GET `/api/admin/orders` (Admin)
- PUT `/api/admin/orders/{id}/status` (Admin)

## Analytics
- GET `/api/analytics/kpis`
- GET `/api/analytics/sales/time`
- GET `/api/analytics/top/products`
- GET `/api/analytics/top/customers`
- GET `/api/analytics/by/territory`
- GET `/api/analytics/by/shipmethod`
