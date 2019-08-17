#!/bin/bash

dotnet publish -c Release -o out -r osx-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true