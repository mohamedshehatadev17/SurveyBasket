# Survey Basket: A Comprehensive Survey Management System

The Survey Basket is a robust and efficient system designed for creating, managing, and analyzing surveys. Built with the **.NET framework**, this project streamlines the process of gathering valuable data and insights from users, customers, or stakeholders, making it an ideal solution for businesses, educational institutions, and research organizations.

---
Sequence Diagram
<img width="1036" height="473" alt="image" src="https://github.com/user-attachments/assets/257fad83-9e74-40cc-96a1-14e9bc600015" />
---
### Key Features

* **Survey Creation**: A user-friendly interface for designing customizable surveys with support for various question types, including multiple-choice, short answers, and scales.
* **Participant Management**: Invite participants via email or generate shareable links, ensuring secure and personalized survey access.
* **Response Collection**: Track responses in real-time with an intuitive dashboard and send automated reminders to participants.
* **Data Management (Pagination & Filtering)**: Efficiently handle large datasets with built-in **pagination** for navigating through extensive lists of surveys and responses. Robust **filtering** options help you quickly locate specific data points based on various criteria.
* **Authentication & Authorization**: Secure user management with robust **authentication** to verify user identities and granular **authorization** controls to manage access permissions based on user roles (e.g., administrators, survey creators, participants).
* **Rate Limiting**: Protects the API from excessive requests, ensuring fair usage, system stability, and preventing abuse or Denial-of-Service (DoS) attacks. This limits the number of requests a user or client can make within a specified time window.
* **API Versioning**: Manages changes and updates to the API in a structured manner, allowing for the introduction of new features or modifications without disrupting existing client applications, ensuring backward compatibility and smooth transitions.
* **Background Jobs**: Execute long-running or scheduled tasks asynchronously, such as sending email notifications (e.g., survey invitations, reminders, report delivery), generating complex reports, or performing data cleanup, without impacting the responsiveness of the main application.
* **Multi-language Support**: Create and answer surveys in multiple languages for global reach.
* **Secure and Scalable**: Built on .NET to ensure high performance, scalability, and robust security, including encryption of survey data and compliance with privacy standards.

---

### Technical Details

* **Backend**: An API-driven architecture powered by **ASP.NET Core** handles business logic and data management. This includes implementing RESTful endpoints for data operations, supporting efficient data retrieval with pagination and filtering, and managing user authentication (e.g., JWT, cookie-based) and authorization (e.g., role-based access control).
    * **Rate Limiting Implementation**: Configured to restrict the number of requests per client or IP address over a defined period, returning a `429 Too Many Requests` status code when limits are exceeded.
    * **API Versioning Strategy**: Supports API evolution by employing versioning (e.g., via URL path or HTTP headers) to allow clients to consume specific API versions, ensuring stability as the system grows.
    * **Background Job Processing**: Utilizes ASP.NET Core's hosted services or dedicated libraries like Hangfire/Quartz.NET to reliably execute tasks outside the main request-response cycle, ensuring smooth operation for time-consuming operations.
* **Database**: **SQL Server** is used for secure storage of survey data and user information, designed to support efficient querying for filtered and paginated data.

---

### Project Goals

The Survey Basket aims to be a powerful tool for organizations looking to make data-driven decisions through comprehensive and customizable surveys. Its modular design and robust functionality ensure adaptability to various use cases, from employee feedback to market research.

---

### Getting Started

To get a local copy up and running, follow these simple steps.

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/mohamedshehatadev17/SurveyBasket.git](https://github.com/mohamedshehatadev17/SurveyBasket.git)
    ```
2.  **Navigate to the project directory:**
    ```bash
    cd SurveyBasket
    ```
3.  **Restore dependencies and build the project:**
    ```bash
    dotnet restore
    dotnet build
    ```
4.  **Run database migrations (if applicable):**
    This project uses **Entity Framework Core** for database management.
    * **Prerequisites:** Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed. Also, install the EF Core command-line tools globally:
        ```bash
        dotnet tool install --global dotnet-ef
        ```
        If you already have it installed, ensure it's up to date:
        ```bash
        dotnet tool update --global dotnet-ef
        ```
    * **Applying Existing Migrations:** To update your database schema to the latest version defined by the project's migrations, navigate to the directory containing your `DbContext` (or the startup project if `DbContext` is in a different assembly) and run:
        ```bash
        dotnet ef database update
        ```
    * **Creating New Migrations (for developers):** If you make changes to the data model (entities or `DbContext`), you'll need to create a new migration. Replace `[MigrationName]` with a descriptive name for your changes:
        ```bash
        dotnet ef migrations add [MigrationName]
        ```
        Then, apply this new migration to your database using `dotnet ef database update`.

5.  **Run the application:**
    ```bash
    dotnet run
    ```
    *(Refer to your project's specific startup configuration for API or UI access details, typically found in `appsettings.json` or `Program.cs`.)*

---

### Contributing

*(Add guidelines for how others can contribute to your project. This typically includes information on reporting bugs, suggesting features, and submitting pull requests.)*

---

### License

*(Add your project's license information here. For example: `This project is licensed under the MIT License - see the LICENSE.md file for details.`)*

---

### Contact

For any questions or inquiries, please contact Mohamed Shehata at [mohamedshehatadev0@gmail.com](mailto:mohamedshehatadev0@gmail.com).

***

**Source:** [Survay Basket - Mostaql](https://mostaql.com/portfolio/2333946-survay-basket)
