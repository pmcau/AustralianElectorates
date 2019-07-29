# <img src="https://raw.github.com/pmcau/AustralianElectorates/master/icon.png" height="40px"> Australian Electorate information

All information about electorates is available at [/Data/electorates.json](/Data/electorates.json).

toc


## Example content

```json
[
  {
    "Name": "Canberra",
    "ShortName": "canberra",
    "State": "ACT",
    "Area": 312.0,
    "ExistIn2016": true,
    "ExistIn2019": true,
    "ExistInFuture": true,
    "DateGazetted": "2018-07-13",
    "Description": "<p>The Division of Canberra covers an area in central ACT consisting of the Districts of:</p><ul><li>Canberra Central,</li><li>Kowen,</li><li>Majura,</li><li>part of Belconnen,</li><li>part of Jerrabomberra,</li><li>part of Molonglo Valley,</li><li>part of Weston Creek, and</li><li>part of Woden Valley</li></ul>",
    "DemographicRating": "<strong>Inner Metropolitan</strong> - situated in capital cities and consisting of well-established built-up suburbs",
    "ProductsAndIndustry": "Mainly residential with tourism, retail and some light industry at Fyshwick and Beard",
    "NameDerivation": "A locality name derived from an Aboriginal word which is held to mean 'meeting place'.",
    "Members": [
      {
        "Name": "Brodtmann, G",
        "Begin": 2010,
        "Party": "ALP"
      },
      {
        "Name": "Ellis, A",
        "Begin": 1998,
        "End": 2010,
        "Party": "ALP"
      }
    ]
  },
```


## Maps

All maps are in [geojson format](http://geojson.org/).

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

https://nuget.org/packages/AustralianElectorates/ [![NuGet Status](http://img.shields.io/nuget/v/AustralianElectorates.svg?longCache=true&style=flat)](https://www.nuget.org/packages/AustralianElectorates/)


https://nuget.org/packages/AustralianElectorates.Bogus/ [![NuGet Status](http://img.shields.io/nuget/v/AustralianElectorates.Bogus.svg?longCache=true&style=flat)](https://www.nuget.org/packages/AustralianElectorates.Bogus/)


## Usage

snippet: usage


## Bogus Usage

snippet: usagebogus


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

Some tools are required for GIS operations.


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


## Icon

<a href="https://thenounproject.com/term/gerrymandering/15219/" target="_blank">Gerrymandering</a> designed by <a href="https://thenounproject.com/Iconathon1" target="_blank">Iconathon</a> from The Noun Project
