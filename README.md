# FIFA World Cup Betting Application

A full-stack web application for FIFA World Cup betting with an Angular frontend, .NET 8 backend, and PostgreSQL database.

## Features

### Phase 1 - Authentication & User Management ✅
- User registration and login
- JWT-based authentication
- Password reset via email
- Secure password hashing with BCrypt
- User profile management

### Phase 2 - Betting Logic (Prepared)
- Group stage predictions
- Knockout stage betting
- Tournament progression calculation
- Leaderboards and scoring

## Technology Stack

### Backend
- **.NET 8** - Web API with clean architecture
- **PostgreSQL** - Database with Entity Framework Core
- **JWT** - Authentication and authorization
- **BCrypt** - Password hashing
- **SMTP** - Email service for password reset

### Frontend (To be implemented)
- **Angular** (latest stable) - SPA framework
- **Angular Material** - UI component library
- **Responsive Design** - Mobile and desktop friendly

### Infrastructure
- **Docker** - Containerization
- **Docker Compose** - Local development environment

## Project Structure

```
FifaWorldCupBetting/
├── FifaWorldCupBetting.Api/          # Web API layer
├── FifaWorldCupBetting.Application/  # Business logic layer
├── FifaWorldCupBetting.Domain/       # Domain entities
├── FifaWorldCupBetting.Infrastructure/ # Data access and external services
├── docker-compose.yml                # Docker setup
└── README.md                         # This file
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- Docker and Docker Compose
- Node.js and npm (for Angular frontend)
- PostgreSQL (or use Docker)

### Local Development Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd FifaWorldCupBetting
   ```

2. **Start PostgreSQL with Docker**
   ```bash
   docker-compose up postgres -d
   ```

3. **Configure Email Settings**
   
   Update `appsettings.json` in the API project:
   ```json
   {
     "EmailSettings": {
       "SmtpHost": "smtp.gmail.com",
       "SmtpPort": 587,
       "SmtpUsername": "your-email@gmail.com",
       "SmtpPassword": "your-app-password",
       "FromEmail": "your-email@gmail.com",
       "FromName": "FIFA World Cup Betting"
     }
   }
   ```

   **For Gmail:**
   - Enable 2-factor authentication
   - Generate an App Password
   - Use the App Password in the configuration

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update --startup-project FifaWorldCupBetting.Api --project FifaWorldCupBetting.Infrastructure
   ```

5. **Run the Backend API**
   ```bash
   dotnet run --project FifaWorldCupBetting.Api
   ```

   The API will be available at: `https://localhost:7239` or `http://localhost:5000`

6. **View API Documentation**
   
   Navigate to `https://localhost:7239/swagger` to see the interactive API documentation.

### Using Docker Compose

To run the entire stack with Docker:

```bash
# Build and start all services
docker-compose up --build

# Run in detached mode
docker-compose up -d

# Stop all services
docker-compose down

# View logs
docker-compose logs -f api
```

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/request-password-reset` - Request password reset
- `POST /api/auth/reset-password` - Reset password with token

### User Management
- `GET /api/user/me` - Get current user info (requires authentication)

## Database Schema

### Phase 1 Tables
- **Users** - User accounts and authentication
- **Teams** - World Cup teams (prepared for Phase 2)
- **Matches** - Tournament matches (prepared for Phase 2)
- **UserPredictions** - User match predictions (prepared for Phase 2)
- **Bets** - Tournament outcome bets (prepared for Phase 2)
- **TournamentStages** - Tournament stages (prepared for Phase 2)

## Configuration

### Environment Variables

#### Development
- `ConnectionStrings__DefaultConnection` - PostgreSQL connection string
- `JwtSettings__SecretKey` - JWT signing key (minimum 256 bits)
- `EmailSettings__SmtpHost` - SMTP server host
- `EmailSettings__SmtpUsername` - SMTP username
- `EmailSettings__SmtpPassword` - SMTP password

#### Production
Set these environment variables in your production environment:
- `ASPNETCORE_ENVIRONMENT=Production`
- `ConnectionStrings__DefaultConnection`
- `JwtSettings__SecretKey`
- `EmailSettings__*` (all email settings)

## Security Features

- **JWT Authentication** - Secure token-based authentication
- **Password Hashing** - BCrypt with salt
- **Password Reset Tokens** - Secure, time-limited tokens
- **CORS Configuration** - Configured for Angular frontend
- **Input Validation** - Data annotations and model validation
- **SQL Injection Protection** - Entity Framework parameterized queries

## Development Guidelines

### Adding New Features
1. Define entities in the `Domain` layer
2. Create DTOs in the `Application` layer
3. Implement business logic in `Application` services
4. Add data access in `Infrastructure` repositories
5. Create API endpoints in the `Api` layer
6. Write tests for all layers

### Database Migrations
```bash
# Add new migration
dotnet ef migrations add <MigrationName> --startup-project FifaWorldCupBetting.Api --project FifaWorldCupBetting.Infrastructure

# Apply migrations
dotnet ef database update --startup-project FifaWorldCupBetting.Api --project FifaWorldCupBetting.Infrastructure

# Remove last migration (if not applied)
dotnet ef migrations remove --startup-project FifaWorldCupBetting.Api --project FifaWorldCupBetting.Infrastructure
```

## Next Steps - Angular Frontend

The backend API is ready for the Angular frontend implementation. The frontend should include:

1. **Authentication Module**
   - Login/Register components
   - JWT token handling
   - Route guards
   - Password reset flow

2. **User Dashboard**
   - User profile
   - Navigation
   - Responsive layout with Angular Material

3. **Betting Modules** (Phase 2)
   - Group stage predictions
   - Knockout bracket
   - Leaderboards

## Testing the API

Use the provided Swagger UI at `/swagger` or test with tools like Postman:

### Example Registration Request
```json
POST /api/auth/register
{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!"
}
```

### Example Login Request
```json
POST /api/auth/login
{
  "email": "test@example.com",
  "password": "Password123!"
}
```

## Troubleshooting

### Common Issues

1. **Database Connection Issues**
   - Ensure PostgreSQL is running
   - Check connection string in `appsettings.json`
   - Verify firewall settings

2. **Email Not Sending**
   - Check SMTP settings
   - Verify email credentials
   - Check spam folder for test emails

3. **JWT Token Issues**
   - Ensure `JwtSettings:SecretKey` is at least 256 bits
   - Check token expiration time
   - Verify CORS settings for frontend

### Logs
- Application logs are written to console and can be configured in `appsettings.json`
- Entity Framework logs SQL queries in development mode
- Email sending errors are logged with full exception details

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests
5. Submit a pull request

## License

This project is licensed under the MIT License.
