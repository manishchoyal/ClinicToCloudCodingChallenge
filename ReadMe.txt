Introduction: 

This project is created to 
1. retrieve Patient details.
2. retrieve Patient details using pagination -- I have used pagination with entity framework but this can be done using Stored Procedure.
3. add a new patient.
4. update an existing patient based on the id.
5. I have used Entity Frameworks' InMemory database to load 15 patient details during project Startup.
6. I have used DI to injected Service reference into the Controller's constructor.

Technology used:

1. ASP.NET Core 2.2.0
2. InMemory database instead of stand alone database
3. GitHub as source control
4. nUnit for testing

Detailed Explanation:
1. List all Patients:
	This end point returns all the Patients present in the database. The code uses Asynchronous call so the end user have a good experience.

        Additional: List of all Patients with pagination:

		I have created another end point to send Patient details if the page and pagesize values are provided as part of the request. 
		This API will send back all the resources within the criteria

2. Adding a new Patient (Post /v1/patients)
	This end point will work with a request body and insert a new record in the database. 201 status code will be returned when the patient is created successfully. 
	The API will also check if a patient record is already present before creating a new one to avoid duplication

3. Update Patient details ( Put v1/patients/{id})
	This end point will update Patient details. This API will first check if the ID is present in the database and then update the Patient record. 
	Return Ok 200 status code when updated successfully. Will return 304 status code when a patient with the passed Id is not present in the database.
