* Projetc runs on .Net Core 8 APIs platform, EF Core as ORM, MS SQL Server as DBMS holds project migrations with Angular 17 Client
  
* Project contains different Crud operations for tow types of entitites, UserProfile and Posts, taking into consideration the one to many relashionship between the tow entities

* Bearer authentication is used to authorize Profiles, so that it can perform CRUD operations to it's posts aand also to be able to edit the profile throughout changing the email or password

* UserProfile entity is an identity user with additional properties

* JWT Tokens are used to validate the current user by setting the token after login or register operation  into a session storage then setting a bearer authorization header throughout an authentication
 interceptor and then activate authorized resources with an authentication guard , so that it can get authorized actions

* Used packges are EntityFrameWorkCore.sqlServer, EntityFrameWorkCore.tools and Authentication.JWTBearer.

* There should be a database script to creat the database, if any trouble a github repository with migrations folder will be provided, a zipped folder with both client and server side source code screenshots
  displaying our running UI and a postman collection to be able to send requests and receive responses from endpoints. 
