# FIFA World Cup Betting - Quick Start Guide

## Phase 1 Complete ✅

This project now has a fully functional **backend API** with authentication and user management, ready for the Angular frontend implementation.

## What's Built

### ✅ Backend (.NET 8 API)
- **Clean Architecture** (Domain, Application, Infrastructure, API layers)
- **JWT Authentication** with secure password hashing
- **PostgreSQL Database** with Entity Framework Core
- **Email Service** for password reset
- **Database Migrations** ready to apply
- **Swagger Documentation** for API testing
- **Docker Support** for easy deployment

### ✅ Database Models (Ready for Phase 2)
- Users, Teams, Matches, UserPredictions, Bets, TournamentStages
- Complete FIFA World Cup team data seeded
- Relationships and constraints configured

### 🚧 Frontend (Next Step)
- Angular application with Angular Material
- Authentication pages (login, register, password reset)
- Responsive design
- JWT token handling

## Quick Start (5 minutes)

### 1. Start PostgreSQL Database
```bash
docker-compose -f docker-compose.dev.yml up -d
```

### 2. Run the API
```bash
dotnet run --project FifaWorldCupBetting.Api
```

### 3. Test the API
Open your browser to: **https://localhost:7239/swagger**

## Test the Authentication

### Register a User
```json
POST /api/auth/register
{
  "username": "testuser",
  "email": "test@example.com", 
  "password": "Password123!",
  "confirmPassword": "Password123!"
}
```

### Login
```json
POST /api/auth/login
{
  "email": "test@example.com",
  "password": "Password123!"
}
```

### Get User Info (with JWT token)
```
GET /api/user/me
Authorization: Bearer <your-jwt-token>
```

## What's Next

### 🎯 Angular Frontend Implementation
1. **Create Angular Project**
   ```bash
   ng new fifa-betting-frontend
   cd fifa-betting-frontend
   ng add @angular/material
   ```

2. **Install Dependencies**
   ```bash
   npm install @angular/common @angular/forms @angular/router
   ```

3. **Create Authentication Module**
   - Login component
   - Register component
   - Password reset components
   - JWT interceptor
   - Auth guard

4. **Create Core Services**
   - AuthService (login, register, logout)
   - UserService (get current user)
   - HTTP interceptor for JWT

5. **Implement Responsive Design**
   - Angular Material components
   - Mobile-first approach
   - Navigation with user status

### 🎲 Phase 2 - Betting Logic
The backend is already prepared with:
- Team and match models
- Prediction and betting entities
- Tournament stage management

Next features to implement:
- Group stage prediction interface
- Knockout bracket visualization
- Scoring and leaderboard system
- Real-time match result updates

## Project Structure
```
FifaWorldCupBetting/
├── 🟢 FifaWorldCupBetting.Api/          # ✅ Complete
├── 🟢 FifaWorldCupBetting.Application/  # ✅ Complete  
├── 🟢 FifaWorldCupBetting.Domain/       # ✅ Complete
├── 🟢 FifaWorldCupBetting.Infrastructure/ # ✅ Complete
├── 🔵 frontend/                         # 🚧 To be created
├── 🟢 docker-compose.yml               # ✅ Complete
└── 🟢 README.md                        # ✅ Complete
```

## Configuration

### Email Setup (Required for Password Reset)
Update `appsettings.json`:
```json
{
  "EmailSettings": {
    "SmtpHost": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "your-email@gmail.com",
    "SmtpPassword": "your-app-password"
  }
}
```

### Database Connection
Default (Docker): `Host=localhost;Database=FifaWorldCupBetting;Username=postgres;Password=postgres`

## Ready for Frontend Development! 🚀

The backend API is production-ready and follows best practices:
- ✅ Clean Architecture
- ✅ Security (JWT, BCrypt, input validation)
- ✅ Error handling and logging
- ✅ CORS configured for Angular
- ✅ Docker containerization
- ✅ Comprehensive documentation

Start building the Angular frontend by calling the API endpoints documented in Swagger!
