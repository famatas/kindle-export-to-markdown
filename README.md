# kindle-export-to-markdown

Simple project to generate Markdown document from the HTML exported file generated with notes using the Kindle Mobile APP.

## Installation

Use dotnet to install the API.

```bash
dotnet restore

dotnet build .\KindleExportToMarkdown.sln

dotnet run --project .\KindleExportToMarkdown\KindleExportToMarkdown.csproj --no-restore

```

You can find the API spec on https://localhost:7041/swagger/index.html

## Usage

Send a request to the endpoint `api/Export/ExportBook` with the exported file.
