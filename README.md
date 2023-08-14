# SortedName

Add Input and Run:
Place your unsorted-names-list.txt file in the NameSorterApp folder.
Open a command-line interface and navigate to the NameSorterApp folder.
Run the following command to build and run the console app:
dotnet run

Check the Output:
After running the console app, you'll see the sorted names displayed on the console, and a file named sorted-names-list.txt will be created in the same directory with the sorted names.

# WorkFlow: 
name: Build and Test

on:
  push:
    branches:
      - main

jobs:
  build-test:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout code
        uses: actions/checkout@v2

      - name: Setup .NET
        uses: actions/setup-dotnet@v2
        with:
          dotnet-version: 5.x

      - name: Build and Test
        run: |
          dotnet restore
          dotnet build
          dotnet test NameSorterApp.Tests
