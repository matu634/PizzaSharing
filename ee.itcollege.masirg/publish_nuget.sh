#!/bin/sh

# delete old stuff
find . -iname "bin" -o -iname "obj" -print0 | xargs -0 rm -rf

# build release dll
dotnet build --configuration release 

# add .nuget files.
dotnet pack --configuration release  

#publish all the package files
nuget push -ApiKey oy2eovrdzbt4nliek2aq6ml5h3vhz6julhgxeemovyy7tu -Source https://api.nuget.org/v3/index.json ee.itcollege.masirg**Release/ee.itcollege.masirg*.nupkg 
