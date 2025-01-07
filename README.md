# URL Shortener API

## Overview

The URL Shortener API is a backend service for generating short URLs, managing URL data, and tracking access logs. API supports URL redirection, click tracking, and detailed statistics for URL usage.

## Features

- Shorten URLs: Generate short URLs for any valid long URL.

- Redirect URLs: Automatically redirect users from short URLs to their original destinations.

- Access Logging: Record user-agent and IP address data when URLs are accessed.

- URL Management: Update or delete existing shortened URLs.

- Statistics: Retrieve logs and access counts for all URLs.

## Endpoints

### 1. Get All URLs

**GET /api/**

Retrieves a list of all shortened URLs.

### 2. Get URL Data by ID

**GET /api/url/{id}**

Fetches detailed information about a specific URL, including access logs.

Parameters:

- id (string): Unique identifier of the URL.

Responses:

- 200 OK: URL entity data.

- 404 Not Found: URL not found.

- 400 Bad Request: Invalid ID format.

### 3. Redirect to Original URL

**GET /api/{shortUrl}**

Redirects to the original URL corresponding to the given short URL.

Parameters:

- shortUrl (string): Shortened URL identifier.

Responses:

- 302 Found: Redirects to the original URL.

- 404 Not Found: Short URL not found.

### 4. Create a Short URL

**POST /api/shorten/{url}**

Generates a short URL for the given long URL.

Parameters:

- url (string): The original long URL.

Responses:

- 200 OK: Short URL created successfully.

- 409 Conflict: Long URL already exists.

- 400 Bad Request: Invalid URL format.

### 5. Update URL Entity

**PUT /api/{id}**

Updates the short URL or other data associated with a given ID.

Parameters:

- id (string): URL entity ID.

- UrlEntityDto (body): New data for the URL entity.

Responses:

- 200 OK: URL updated successfully.

- 404 Not Found: URL not found.

- 400 Bad Request: Invalid input data.

- 409 Conflict: Short URL already exists.

### 6. Delete URL Entity

**DELETE /api/url/{id}**

Deletes a URL entity by ID.

Parameters:

- id (string): URL entity ID.

Responses:

- 200 OK: URL deleted successfully.

- 400 Bad Request: Invalid ID format.

### 7. Get Access Logs

**GET /api/stats**

Retrieves URL entities along with access logs.

Responses:

- 200 OK: List of URL entities with logs.

- 404 Not Found: No logs available.

### 8. Login

**GET /api/user/login**

Logs in user if credentials are correct.

Responses:

- 200 OK: List of URL entities with logs.

- 401 Unauthorized: No logs available.

## Tech Stack

- Framework: ASP.NET Core

- Database: PostgreSQL

- Language: C#

## Setup and Installation
Clone the repository:

`git clone https://github.com/paplauskis/url-shortener`

Navigate to the project folder:

`cd url_shortener`

Build the project:

`dotnet build`

Run the application:

`dotnet run`
