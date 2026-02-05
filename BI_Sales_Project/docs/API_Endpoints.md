# API Endpoints

## Auth
- POST `/api/auth/login`
- POST `/api/auth/register`
- POST `/api/auth/seed`

## Catalog
- GET `/api/categories`
- POST `/api/categories`
- PUT `/api/categories/{id}`
- DELETE `/api/categories/{id}`
- GET `/api/categories/{id}/subcategories`
- POST `/api/subcategories`
- PUT `/api/subcategories/{id}`
- DELETE `/api/subcategories/{id}`
- GET `/api/products`
- GET `/api/products/{id}`
- POST `/api/products`
- PUT `/api/products/{id}`
- DELETE `/api/products/{id}`

## Cart
- GET `/api/cart`
- POST `/api/cart/items`
- PUT `/api/cart/items/{itemId}`
- DELETE `/api/cart/items/{itemId}`
- POST `/api/cart/checkout`

## Orders
- GET `/api/orders/my`
- GET `/api/orders/{id}`
- GET `/api/admin/orders`
- PUT `/api/admin/orders/{id}/status`

## Analytics
- GET `/api/analytics/kpis`
- GET `/api/analytics/sales/time`
- GET `/api/analytics/top/products`
- GET `/api/analytics/top/customers`
- GET `/api/analytics/by/territory`
- GET `/api/analytics/by/shipmethod`
