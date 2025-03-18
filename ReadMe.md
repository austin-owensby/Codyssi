# Codyssi
This repository is setup to assist with solving challenges from [Codyssi](https://www.codyssi.com).

It allows you to easily run solutions in C#, submit answers and see the response, pull down your inputs, and run test inputs via a Web API or Console app described below.
This also references some utilities to make some solutions easier. See my [Shared Utility repo](https://github.com/austin-owensby/SharedUtilities) file for more info.

The `main` branch contains a ready to use template to start your own solutions.
You may also reference the `aowensby-solutions` branch which contains my own solutions.

## Work in Progress
This is still a work in progress and needs more organization to work with this event's specific format

## Quick Start
1. Run the Program (See [Setup](#setup) below)
1. (Optional) Create a Cookie.txt file to enable puzzle input/submission (See [Puzzle Helper](#puzzle-helper) below)
1. (Optional) Make the `import-input-file` API call or use the equivalent in the console app to get your input for the day you're trying to solve (See [API](#post-apiimport-input-file) below)
1. Code your solution in one of the Service files
1. Make the `run-solution` API call or use the equivalent in the console app and optionally submit the solution (See [API](#get-apirun-solution) below)

## Setup
1. If not already installed, install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download)
1. Run the program
   - You have 2 options, either as a Web API with endpoints you can hit or a console app
   - If using Visual Studio or VSCode, use the play button to build and run the code after selecting your desired startup method
   - If using a CLI, run `dotnet run` from the repo's base folder
   - If using the console app, you will be prompted for inputs, you can bypass these by setting default values in the `Console/Program.cs` file.
1. Run API calls (Only needed if running the Wbe API project)
   - You can use [Swagger](https://swagger.io/), an API documentation and execution library, to execute the API calls, or use your own tool to call to the API
   - Visual Studio
      - The browser should open Swagger by default, to change this behavior, update the `Properties/launchSettings.json`
   - VSCode
      - Use the `Launch (web)` or `Launch (web - no browser)` configuration to toggle if you want the browser to open automatically
      - If you'd prefer to remain within VSCode to make the API calls, I've used the [Thunder Client](https://marketplace.visualstudio.com/items?itemName=rangav.vscode-thunder-client) extension. You can import the collection provided at `thunder_collection_Codyssi.json`.
   - Other
      - Visit https://localhost:5001/swagger in your browser

## Puzzle Helper
This allows you to easily create the needed services as well as submitting answers.

In the `Shared/PuzzleHelper` folder, create a `Cookie.txt` file and add your own cookie that gets created when logging into the Codyssi website. While on the Codyssi website, if you open the Network tab in your browser's Dev Tools, you'll see the cookie in the header of API calls that are made while navigating the site.

Ensure that the Cookie.txt is all 1 line.

If you would like to avoid this setup, you can always manually add you input and submit your solutions without having to create a Cookie.txt file.
The file is only required when interacting with the Codyssi website.

### Automation
The Puzzle Helper attempts to prevent spamming the server by:
* Outbound calls are throttled to every 3 minutes in the CodyssiGateway's `ThrottleCall()` function
* Once inputs are downloaded, they are cached locally (PuzzleHelper's `WriteInputFile(int day)` function) through the `api/import-input-file` endpoint described below.
* If you suspect your input is corrupted, you can get a fresh copy by deleting the old file and re-running the `api/import-input-file` endpoint.
* The User-Agent header in the Program.cs's gateway configuration is set to me since I maintain this tool :)

## API

### GET `api/run-solution`
- Query parameters
   - day (Ex. 14) (Defaults to 1)
   - send (Ex. true) (Defaults to false) Submit the result to Codyssi
   - example (Ex. true) (Defaults to false) Use an example file instead of the regular input, you must add the example at `Inputs/<DD>_example.txt`
- Ex. `GET api/run-solution?day=14&&send=true`

Runs a specific day's solution, and optionally posts the answer to Codyssi and returns the result.

### POST `api/import-input-file`
- Query parameters
   - day (Ex. 14) (Defaults to 1)
- Ex. `POST api/import-input-file?day=14`

Imports the input and example from Codyssi for a specific day.

The program is idempotent (You can run this multiple times as it will only add a file if it is needed.)
