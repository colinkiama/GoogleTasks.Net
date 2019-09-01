# GoogleTasks.Net
[![nuget version](https://img.shields.io/nuget/v/GoogleTasks.NET.ColinKiama)](https://www.nuget.org/packages/GoogleTasks.NET.ColinKiama/)

A Google Tasks .Net Standard API

Everything in the API has been implemented except Patch methods (which aren't necessary)

## How to use:
1) Get your API Keys from here (you'll need to create a consent screen): https://console.developers.google.com/
2) Create an `OAuthClient` with your API keys.
3) Generate an Authorization URL with the `OAuthClient`.
4) Launch the URL in a browser to login
5) Handle the query strings returned from the browser redirect with one of the `FinishOAuthAsync` methods.
6) You should have obtained a token from step 5. Create a `TasksClient` with the API Keys and token you've obtained. You will now be able to interact with the Tasks API using methods from `TasksClient`.

Note: You are responsible for ensuring that your token isn't expired while using the API. You can refresh the token with the `RefreshTokenAsync` method from `OAuthClient`.

You should save your Token to avoid logging in everytime you wish to access the API.


## Issues
For bugs, feature requests or anything else, open up a new issue.

## References: 
Google Tasks API: https://developers.google.com/tasks/v1/reference/ 

Google OAuth 2.0 for Mobile & Desktop Apps: https://developers.google.com/identity/protocols/OAuth2InstalledApp

Google OAuth Apps For Windows Samples: https://github.com/googlesamples/oauth-apps-for-windows
