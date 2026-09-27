# SafeVault

Small ASP.NET Core Web API built for the Security and Authentication course project.

The project covers:
- input validation
- SQL injection prevention
- password hashing
- JWT authentication
- role-based authorization
- XSS protection
- security-focused tests

## Security notes

Database access is handled through Entity Framework Core, so user input is parameterized instead of being concatenated into SQL. A raw SQL example is also included with `FromSqlInterpolated`.

Passwords are hashed with ASP.NET Core's `PasswordHasher<TUser>`. After login, the API issues a short-lived JWT containing the user identity and role.

Protected routes use `[Authorize]`, while the admin route uses:

```csharp
[Authorize(Roles = "Admin")]
```

User-controlled text that is returned as HTML is encoded before being rendered to reduce XSS risk.

## Issues fixed

During the security review I focused on a few common problems:

- SQL injection: replaced unsafe query construction with parameterized EF Core queries.
- XSS: encoded user input before placing it into HTML.
- Password storage: used password hashing instead of storing plain text passwords.
- Authorization: restricted admin endpoints with role-based access control.
- Login responses: used the same error message for an unknown user and an incorrect password.

## Copilot

I used Microsoft Copilot while working through the activities to draft parts of the validation, authentication and authorization code, and to suggest test cases for SQL injection and XSS. I reviewed the suggestions and adjusted them to fit the project.

## Tests

The test project includes checks for:
- registration validation
- SQL injection input
- XSS encoding
- admin role protection

Run the tests with:

```bash
dotnet test
```

## Running the project

Set a JWT signing key first:

```bash
cd SafeVault.Api
dotnet user-secrets set "Jwt:Key" "use-a-long-development-secret-key-at-least-32-characters"
```

Then:

```bash
dotnet restore
dotnet run --project SafeVault.Api
```
