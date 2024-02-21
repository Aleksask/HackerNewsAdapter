# Hacker News API Adapter

This api implementation allows user to request up to `n` best hacker news articles (max `n` is limited by hacker news api)

## Design
Hacker news API Adapter is a simple wrapper around hackernews api with asp.net core minimal api exposing one endpoint `/api/best`.

Since only vanilla HTTP GET requests were needed Refit library was chosen to abstract interaction via with HackerNews API instead of raw `HttpClient`.

To protect overloading of the hacker news api concurrent rate limiter middleware was added and configured to take only 10 concurrent user requests (can be modified). 
To further the load reduction on the hackernews api in memory cache had been added so the items which had been downloaded wouldn't be repeatedly downloaded.
To improve the performance of the cold load (when there are no articles in memory cache) article details are downloaded in parallel.

## Solution structure

/src/Api - contains the REST api endpoint for the hackernews api adapter

/src/Client - contains hackernews api endpoint definitions

/src/Models - contains the models for hackernews Api and the hackernews api adapter, together with a mapper config

/tests - contains tests for the interaction with the hackernews api  


## Usage
1. Run the HackerNewsAdapter application with the `https` profile (swagger api explorer can be reached via https://localhost:7095/swagger/index.html)
2. To get full list of the hackernews stories sorted by score use url : https://localhost:7095/api/best
3. To specify just subset of top stories you can specify filter https://localhost:7095/api/best?n=5 where 5 is the number of top stories


Assumptions made : 
- that the rate limiting is adequate and hackernews api can cope with the load
- that hackernews can process 100 (10 simultaneous users chunk of 10 story requests) simultaneous requests
- hackernews api doesn't allow to fetch more than 200 stories so if the user requests 10000 only 200 will be returned 

Potential improvements :
- improve cold-load performance of the api
- make cache persistable to permanent storage
- implement background service that would get live updates on best stories list and populate the cache
- write more tests for the coverage
- move configuration of the rate limiter middleware, parallel request etc. to the appsettings.json
- more tests added to test the validity of returned models

