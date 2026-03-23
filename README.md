# StudentTaskApi

ASP.NET Core Web API (.NET 9)

Purpose of this project:
Learning and implementing about following subjects: SQLite, Relational databases, Migrations, REST API, JSON API, OpenAPI Specs, JWT Token authentication, HTTP, BCrypt.

is a simple backend system to manage student tasks.
It lets you store:
students, the tasks assigned to them, and the learning info connected to those tasks (courses, categories, and instructors).
It also provides a basic login system so only authenticated users can access protected endpoints.

Models(Entities) and relationships in the API:
`Student`
 - A person who has tasks.
 - One student can have many tasks(StudentTasks)
  
 - `StudentTask`
  - A task/assignment for a specific student.
  - It includes a title, description (optional), due date, and whether it is completed.
  - Each task always belongs to exactly one `Student`.
  - A task can optionally be linked to:
    - a `Course` (like “Math”)
    - a `Category` (like “Homework”)
- `Instructor`

Example workflow for registering > login > create student > create studenttask:

1. Register a user (`POST /api/auth/register`)
2. Login (`POST /api/auth/login`) and copy the returned `token`
3. Create a student (`POST /api/Students`) using Swagger, with `Authorization: Bearer <token>` if required
4. Create a student task (`POST /api/StudentTasks`) for that `studentId` (and optionally `courseId` / `categoryId`)

Download and run:
In the solutions csproj file open a powershell terminal and run commands: 
dotnet restore
dotnet ef database update

