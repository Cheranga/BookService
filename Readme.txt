DONE
----

Apply CORS
	- Use nuget to install 'Microsoft.AspNet.WebApi.Cors' package.
	- Entire API will be CORS supported
		- In 'WebApiConfig', call the method 'EnableCors' on the HttpConfiguration object.

Use Swagger to document the API
	- Use nuget to install 'swashbuckle'. This will install swagger and it will add a new file to the project 'SwaggerConfig'.
	- In 'WebApiConfig' call the 'Register' method of 'SwaggerConfig'.
	- In the project properties set XML documentation. This will enable to get the code documentation which you provide in code.
	- In 'SwaggerConfig' call 'IncludeXmlComments' to get the path for the generated XML documentation.

Add versioning support
	- Versioning support made via 'accept-header' in the request
	- Created and registered a custom HttpController selector, which will inspect the 'accept' header, and choose the correct controller name.


Add caching support
	- Implemented both memory and sql server caching with CacheCow (separate methods, pick one)
	- Use nuget to install 'CacheCow.Server' (this is all you need if in-memory caching is what you want)	
	- Use nuget to install 'CacheCow.Server.EntityTagStore.SqlServer' (you need this if you need to persist state in database)
		- Created a database manually (in this case 'cache.mdf' and executed the scripts in the 'packages/CacheCow.Server.EntityTagStore.SqlServer.1.0.0') folder
	- Voila, caching is implemented!

DI using Autofac
	- Use nuget to install 'Autofac.WebApi2'. Refer 'http://docs.autofac.org/en/latest/integration/webapi.html' for further reference.

Structure into separate projects



========================================================================================================================================================================

TODO
----


	

Secure the API
	- Add HTTPS Support
	- Authentication / Authorization



Automapper

Testing	

Deploying

Caching
	- Implement caching using 'Redis'