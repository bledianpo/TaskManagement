# Task Management App

A full-stack task management application: **.NET 8 API** (backend) and **React + TypeScript** (frontend).

---

## Features

### Authentication
- **Register** – create account (username, email, password)
- **Login** – JWT-based auth; token stored in browser
- **Session** – 401 handling with “Session expired” message and redirect to login
- **Roles** – Admin and User; admin can see all tasks, users see only their own

### Tasks
- **List** – paginated task list with status filter (Draft, In Progress, Completed)
- **Create** – add task (title, description, priority, status)
- **Edit** – update existing task
- **Delete** – with confirmation dialog
- **Priority & status** – Low/Medium/High; Draft, In Progress, Completed

### API
- REST API with Swagger (Development)
- JWT Bearer authentication
- Paginated task list returns `totalCount` and `totalPages`
- Admin seeder on startup (optional, via config)
- Centralized error handling and validation (FluentValidation)

### Frontend
- React 19, TypeScript, Vite
- Chakra UI, React Router, TanStack Query, Axios
- Responsive layout; inline errors and toasts for feedback
- API error messages shown (e.g. “Invalid email or password” on login)

---

## Project structure

```
TaskManagement/
├── API/                 # ASP.NET Core 8 Web API
├── Application/         # Use cases, DTOs, validators
├── Application.Tests/   # Unit tests (validators)
├── Domain/              # Entities, enums
├── Infrastructure/     # EF Core, repos, JWT, seeding
├── frontend/            # React + Vite app
└── README.md
```

---

## Prerequisites

- **.NET 8 SDK**
- **Node.js** (v18+)
- **SQL Server** (LocalDB / Express or full) – API uses `DefaultConnection` in `appsettings.json`

---

## Backend (API)

### 1. Database

Connection string is in `API/appsettings.json` under `ConnectionStrings:DefaultConnection`. Default example:

```json
"DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=TaskManagementDb;Integrated Security=True;TrustServerCertificate=True"
```

Adjust `Data Source` and catalog to match your SQL Server. On first run, migrations create the database.

### 2. JWT configuration

The API requires a non-empty **JWT signing key**. Set it in one of these ways:

**Option A – User secrets (recommended for local dev)**

```bash
cd API
dotnet user-secrets set "Jwt:Key" "your-secret-key-at-least-32-characters-long"
```

**Option B – appsettings.Development.json**

Add or override:

```json
"Jwt": {
  "Key": "your-secret-key-at-least-32-characters-long",
  "Issuer": "TaskManagementApp",
  "Audience": "TaskManagementApp",
  "ExpiryMinutes": 60
}
```

(Do not commit real secrets to git.)

### 3. Optional: admin seed user

To create an admin user on startup, set in user secrets or config:

```bash
dotnet user-secrets set "Seed:AdminEmail" "admin@example.com"
dotnet user-secrets set "Seed:AdminPassword" "YourPassword123"
dotnet user-secrets set "Seed:AdminUsername" "admin"
```

### 4. Run the API

```bash
cd API
dotnet run
```

- **HTTPS:** https://localhost:7225  
- **Swagger:** https://localhost:7225/swagger (when `ASPNETCORE_ENVIRONMENT=Development`)

### 5. Run backend tests

```bash
dotnet test Application.Tests/Application.Tests.csproj
```

---

## Frontend

### 1. Install dependencies

```bash
cd frontend
npm install
```

### 2. Environment (optional)

To point at a different API base URL, create `frontend/.env`:

```
VITE_API_BASE_URL=https://localhost:7225/api
```

If omitted, the app uses `https://localhost:7225/api` by default.

### 3. Run the app

```bash
cd frontend
npm run dev
```

- App: http://localhost:5173 (or the port Vite prints)
- Log in or register; then use **My Tasks** / **All Tasks** (admin).

### 4. Build for production

```bash
npm run build
npm run preview   # optional: preview production build
```

### 5. Run frontend tests

```bash
npm run test        # single run
npm run test:watch  # watch mode
```

---

## Running the full app

1. Start **SQL Server** and ensure the connection string in `API/appsettings.json` is correct.
2. Set **Jwt:Key** (and optional admin seed) for the API.
3. Start the **API**: `cd API` → `dotnet run`.
4. Start the **frontend**: `cd frontend` → `npm install` → `npm run dev`.
5. Open http://localhost:5173, register or log in, and use the task list.

---

## Tech stack

| Layer      | Backend                    | Frontend                          |
|-----------|----------------------------|------------------------------------|
| Runtime   | .NET 8                     | Node.js                            |
| Framework | ASP.NET Core Web API       | React 19, Vite                     |
| Auth      | JWT Bearer                 | Context + localStorage             |
| Data      | EF Core, SQL Server        | TanStack Query, Axios              |
| Validation| FluentValidation           | –                                  |
| UI        | –                          | Chakra UI v3, React Router 7       |
| Tests     | xUnit (Application.Tests)  | Vitest, jsdom                      |
