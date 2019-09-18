# <img src="/src/icon.png" height="30px"> Australian Electorate information

[![Build status](https://ci.appveyor.com/api/projects/status/mds12hp4duduyie8/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/australianelectorates)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.svg?label=AustralianelEctorates&cacheSeconds=86400)](https://www.nuget.org/packages/AustralianelEctorates/)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.Bogus.svg?label=AustralianelEctorates.Bogus&cacheSeconds=86400)](https://www.nuget.org/packages/AustralianelEctorates.Bogus/)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.DetailMaps.svg?label=AustralianelEctorates.DetailMaps&cacheSeconds=86400)](https://www.nuget.org/packages/AustralianelEctorates.DetailMaps/)

All information about electorates is available at [/Data/electorates.json](/Data/electorates.json).

toc


## Electorates

Location: [/Data/electorates.json](/Data/electorates.json)

Sample:

snippet: Snippets.ElectoratesSampleJson.approved.txt


## Localities

Location: [/Data/Localities.json](/Data/Localities.json)

Sample:

snippet: Snippets.LocalitiesSampleJson.approved.txt


## Parties

Location: [/Data/parties.json](/Data/parties.json)

Sample:

snippet: Snippets.PartiesSampleJson.approved.txt


## Detail Maps

AEC publishes pdf electorate maps with some extra detail. eg:

 * Rivers
 * Main Roads
 * Neighboring electorates

The size of these pdfs is significant. The smaller file variants are approx 600MB in total.

To change these to a more manageable size and format, they have been converted to png and are now approx 30MB in total. Located in [/Data/DetailMaps](/Data/DetailMaps).


## GeoJson Maps

Maps in [geojson format](http://geojson.org/).

The following grouping of maps exist:

 * Future (Next election) [/Data/Maps/Future](/Data/Maps/Future)
 * 2019 election [/Data/Maps/2019](/Data/Maps/2019)
 * 2016 election [/Data/Maps/2016](/Data/Maps/2016)


### Structure

Each of the above groupings have the following structure.

 * Australia and state "combined electorate" maps are at the root.
 * Specific electorate maps are located inside a subdirectory based on the state they exist in.


### Map variants

Each maps has multiple variants based on simplification.

With the two options combined, there are 5 different options for each map.

Below is the combinations for [Bass](https://www.aec.gov.au/profiles/tas/bass.htm)

| Size  | File name                                                        | Simplification |
| -----:| ---------------------------------------------------------------- | --------------:|
| 2.8MB | [bass.geojson](/Data/Maps/Future/Electorates/bass.geojson)       | none           |
| 231KB | [bass_20.geojson](/Data/Maps/Future/Electorates/bass_20.geojson) | 20%            |
| 94KB  | [bass_10.geojson](/Data/Maps/Future/Electorates/bass_10.geojson) | 10%            |
| 46KB  | [bass_05.geojson](/Data/Maps/Future/Electorates/bass_05.geojson) | 5%             |
| 8KB   | [bass_01.geojson](/Data/Maps/Future/Electorates/bass_01.geojson) | 1%             |


#### Simplification

Simplification uses [MapShaper simplify option](https://github.com/mbloch/mapshaper/wiki/Command-Reference#-simplify)

> Visvalingam simplification iteratively removes the least important point from a polyline. The importance of points is measured using a metric based on the geometry of the triangle formed by each non-endpoint vertex and the two neighboring vertices

The level of simplification is represented as a percent number. 20, 10, 5, and 1. representing 20%, 10%, 5%, and 1%. The smaller the number the smaller the file, but with the loss of some accuracy.


## NuGets

The NuGets contain a static copy of all the electorate data. This data is embedded as resources inside the assembly. No network calls are done by the assembly. To get the latests version of the data do a NuGet update. There are several options to help keep a NuGet update:

 * [Dependabot](https://dependabot.com/): Creates pull requests to keep your dependencies secure and up-to-date.
 * [Using NuGet wildcards](https://docs.microsoft.com/en-us/nuget/reference/package-versioning#version-ranges-and-wildcards).
 * [Libraries.io](https://libraries.io/): Supports subscribing to NuGet package updates.

https://nuget.org/packages/AustralianElectorates/ [![NuGet Status](https://img.shields.io/nuget/v/AustralianElectorates.svg?cacheSeconds=86400)](https://www.nuget.org/packages/AustralianElectorates/)

https://nuget.org/packages/AustralianElectorates.Bogus/ [![NuGet Status](https://img.shields.io/nuget/v/AustralianElectorates.Bogus.svg?cacheSeconds=86400)](https://www.nuget.org/packages/AustralianElectorates.Bogus/)

https://nuget.org/packages/AustralianElectorates.DetailMaps/ [![NuGet Status](https://img.shields.io/nuget/v/AustralianElectorates.DetailMaps.svg?cacheSeconds=86400)](https://www.nuget.org/packages/AustralianElectorates.DetailMaps/)


## Usage

snippet: usage


## Bogus Usage

snippet: usagebogus


## DetailMaps Usage

snippet: usageDetailMaps


## Copyright


### Code

The code in this repository is licensed under MIT.

Copyright &copy; 2018 Commonwealth of Australia (Department of the Prime Minister and Cabinet)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.


### Content/Data

The content/data that is rendered (all files under [/Data/](/Data/)) is sourced from the [Australian Electoral Commission (AEC)](https://www.aec.gov.au/) and remains under the [AEC Copyright](https://www.aec.gov.au/footer/Copyright.htm).

The content/data in this repository does not necessarily represent the latest data made available by the AEC. The Department of the Prime Minister and Cabinet gives no warranty regarding the accuracy, completeness, currency or suitability of the content/data for any particular purpose.

This product (AustralianElectorates) incorporates data that is:  
© Commonwealth of Australia (Australian Electoral Commission) 2018

The Data (Commonwealth Electoral Boundaries (various years)) has been used in AustralianElectorates with the permission of the Australian Electoral Commission. The Australian Electoral Commission has not evaluated the Data as altered and incorporated within AustralianElectorates, and therefore gives no warranty regarding its accuracy, completeness, currency or suitability for any particular purpose.

Limited End-user licence provided by the Australian Electoral Commission: You may use AustralianElectorates to load, display, print and reproduce views obtained from the Data, retaining this notice, for your personal use, or use within your organisation only.


## Re-Generating the data

Note: The below are only required by the maintainers and contributors of this project. They are not required when consuming the NuGet package.

Some tools are required.

### Adding new elections

Elections are currently added manually as they are declared.



snippet: elections

### ogr2ogr

Part of [Geospatial Data Abstraction Library (GDAL)](https://www.gdal.org/)

 * Download https://trac.osgeo.org/gdal/wiki/DownloadingGdalBinaries


### MapShaper

[MapShaper](https://github.com/mbloch/mapshaper/)

Installation:

* Install [Node](https://nodejs.org/)
* [Install MapShaper globally](https://github.com/mbloch/mapshaper#installation) `npm install -g mapshaper`
* Ensure `C:\Users\USER\AppData\Roaming\npm` is in path

https://github.com/mbloch/mapshaper/wiki/Command-Reference


### GhostScript

C:\Program Files\gs\gs9.27\bin\


### pngquant

https://pngquant.org/


## Purge history

```
git checkout --orphan newBranch
# Add all files and commit them
git add -A
git commit -m "Initial commit"
# Delete the master branch
git branch -D master
# Rename the current branch to master
git branch -m master
# Force push master branch to github
git push -f origin master
# remove the old files
git gc --aggressive --prune=all
```


## Notes

Media feed ftp://mediafeed.aec.gov.au/24310/Standard/Verbose/

> Verbose Feed: Contains up-to-date election results and information. The feed contains static data, such as candidate names, and dynamic data such as votes. The verbose feed also contains calculated results like swings and aggregated results to the state and national level. This feed is suitable for users who have their own IT system and who may or may not have pre-loaded data and is also suitable for those users who do not have an IT system and simply wish to transform the XMLfile into another format.

From: https://www.aec.gov.au/media/mediafeed/files/media-feed-user-guide-v4.pdf


## Release Notes

See [closed milestones](../../milestones?state=closed).


## Icon

Icon designed by [Iconathon](https://thenounproject.com/Iconathon1) from [The Noun Project](https://thenounproject.com).