# SortedName

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
