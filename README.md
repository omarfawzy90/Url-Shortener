# URL Shortener

A production-ready URL shortening service built with ASP.NET Core, featuring MySQL for persistent storage and Redis caching for high-performance URL resolution.

## 🚀 Features

- ✨ RESTful API for URL shortening and redirection
- 🔄 Automatic redirection with cache-first strategy
- 💾 MySQL database with Entity Framework Core ORM
- ⚡ Redis distributed caching for sub-second response times
- 🛡️ Comprehensive input validation and error handling
- 📊 Structured logging and error responses
- 🔐 Secure URL validation (HTTP/HTTPS only)

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core 8.0
- **Database:** MySQL 8.0 with Pomelo EF Core provider
- **Cache:** Redis (StackExchange.Redis)
- **ORM:** Entity Framework Core 9.0
- **API Documentation:** Swagger/OpenAPI

## 📋 Prerequisites

- .NET 8.0 SDK or higher
- MySQL Server 8.0+
- Redis Server 6.0+ (optional, for caching)
- Entity Framework Core CLI tools

## ⚙️ Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/UrlShortner.git
cd UrlShortner
```

2. Install dependencies:
```bash
dotnet restore
```

3. Update connection strings in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=short-url;User=root;Password=YOUR_PASSWORD;"
  },
  "Redis": {
    "Connection": "localhost:6379"
  }
}
```

4. Apply database migrations:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

5. Run the application:
```bash
dotnet run
```

## 📝 API Endpoints

### Shorten URL
**POST** `/shorten`

Request body:
```json
"https://www.example.com/very/long/url"
```

Response:
```json
{
  "shortUrl": "https://localhost:5001/abc12345"
}
```

### Redirect to Original URL
**GET** `/{shortId}`

Redirects to the original URL or returns 404 if not found.

## 🏗️ Project Structure

```
UrlShortner/
├── controllers/
│   └── UrlControllers.cs    # API endpoints
├── data/
│   └── AppDbContext.cs       # Database context
├── models/
│   └── UrlMapping.cs         # URL entity model
├── Program.cs                # Application entry point
└── appsettings.json          # Configuration
```

## 🔒 Security Notes

- Never commit `appsettings.json` with real credentials
- Use environment variables or Azure Key Vault in production
- Implement rate limiting for production use
- Add authentication/authorization as needed

## 📈 Future Enhancements

- [ ] Custom short URLs
- [ ] Analytics and click tracking
- [ ] Expiration dates for short URLs
- [ ] QR code generation
- [ ] User authentication
- [ ] Dashboard UI

## 👨‍💻 Author

Your Name - Omar Fawzy

## 📄 License

This project is licensed under the MIT License.
