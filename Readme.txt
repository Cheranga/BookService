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

Secure the API
	- Add HTTPS Support
	- Authentication / Authorization

DI using Autofac

Automapper

Testing	