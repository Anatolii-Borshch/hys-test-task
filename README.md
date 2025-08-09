# hys-test-task

## Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/Anatolii-Borshch/hys-test-task.git
   cd hys-test-task
   ```
2. **Restore dependencies**
    ```bash
    dotnet restore
    ```
3. **Run the project**
    ```bash
    dotnet run --project ScheduleMeetingSystem.Api
    ```

## Used packages
- "Microsoft.AspNetCore.OpenApi" Version="8.0.17"
- "Microsoft.EntityFrameworkCore.InMemory" Version="9.0.8"
- "Microsoft.EntityFrameworkCore.Tools" Version="9.0.8"
- "Microsoft.Extensions.DependencyInjection.Abstractions" Version="9.0.8"
- "xunit" Version="2.9.3"
- "Microsoft.EntityFrameworkCore" Version="9.0.8"

## Limitations

- No persistent storage
- No authentication/authorization
- No advanced conflict resolution
- Minimal input validation
- Single time zone assumption
