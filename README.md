[![.NET Core](https://github.com/dianper/payment-gateway/workflows/.NET%20Core/badge.svg)](https://github.com/dianper/payment-gateway/actions?query=workflow%3A%22.NET+Core%22)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=dianper_payment-gateway&metric=alert_status)](https://sonarcloud.io/dashboard?id=dianper_payment-gateway)

# Checkout Payment Gateway
Checkout Payment Gateway in .NetCore

## Requirements
- Docker

## Run Fake Acquiring Bank Client
```
# cd /src/AcquiringBank
docker build -t acquiringbankapi .
docker run -d -p 44385:80 --name fakebank acquiringbankapi
```

## Run Payment Gateway API
```
# cd /src/PaymentGateway
docker build -t paymentgatewayapi .
docker run -d -p 44386:80 --name paymentgateway paymentgatewayapi
```

## Mock Data
```
# Valid MerchantIds
- 63d33faf-7781-48c9-a2f5-a035d1799735
- e5f18ed2-3a06-40b1-85db-3eec9624cc0f

# Card Numbers & Transaction Status
4485008383107041 - Failed
5468797069745763 - Rejected
4913317908108661 - Accepted (if amount > 150 or transaction will be rejected)

# Credit Card Number Generator
https://www.freeformatter.com/credit-card-number-generator-validator.html
```

## Features
- Model validation using FluentValidation
- Data Storage using EF Core Database In Memory
- Authentication using JWT
- API Docs using Swagger
- CI using Github Actions
- Code Quality using Sonarqube
- Fake Acquiring Bank using HttpClient
- Dockerfile to Acquiring Bank and Payment Gateway API
- UnitTests

## Swagger
- http://localhost:44386/swagger

## Endpoints
- POST: Authentication (http://localhost:44386/api/v1/auth)

Body
```json
{
    "username": "paymentgateway",
    "password": "2020"
}
```

Response
```json
{
    "result": {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MDQ4NzUwODUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTA5NTYiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwOTU2In0.EQn3ztnNg_DaV5T8-hKAL3vj6WvW6gQeyGVtu9zC9-k",
        "expiration": "2020-11-08T22:38:05.7333286+00:00"
    },
    "success": true,
    "errors": {}
}
```

- POST: Payment Process (http://localhost:44386/api/v1/payment)

Body
```json
{
    "merchantId": "63d33faf-7781-48c9-a2f5-a035d1799735",
    "cardNumber": "4913317908108661",
    "expiryMonth": 12,
    "expiryYear": 2020,
    "securityCode": 123,
    "amount": 1500,
    "currency": "USD"
}
```
Response
```json
{
  "result": {
    "paymentId": "c1870526-0a9f-4256-9594-8213646af506",
    "transactionId": "214e22ee-a33a-4ca4-8b32-6a12b83bdbf4",
    "transactionStatus": "Accepted"
  },
  "success": true,
  "errors": {}
}
```

- GET: Payment Details (http://localhost:44386/api/v1/payment)

Body
```json
{
    "paymentId": "c1870526-0a9f-4256-9594-8213646af506"
}
```

Response
```json
{
  "result": {
    "paymentId": "c1870526-0a9f-4256-9594-8213646af506",
    "paymentDate": "2020-11-08T20:45:32.8736427+00:00",
    "amount": 1500,
    "cardNumberMasked": "************8661",
    "transactionId": "214e22ee-a33a-4ca4-8b32-6a12b83bdbf4",
    "transactionStatus": "Accepted"
  },
  "success": true,
  "errors": {}
}
```
