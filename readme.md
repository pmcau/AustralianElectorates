# <img src="/src/icon.png" height="30px"> Australian Electorate information

[![Build status](https://ci.appveyor.com/api/projects/status/mds12hp4duduyie8/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/australianelectorates)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.svg?label=AustralianelEctorates)](https://www.nuget.org/packages/AustralianelEctorates/)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.Bogus.svg?label=Bogus)](https://www.nuget.org/packages/AustralianelEctorates.Bogus/)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.DetailMaps.svg?label=DetailMaps)](https://www.nuget.org/packages/AustralianelEctorates.DetailMaps/)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.DetailMaps.Landscape.svg?label=DetailMaps.Landscape)](https://www.nuget.org/packages/AustralianelEctorates.DetailMaps.Landscape/)
[![NuGet Status](https://img.shields.io/nuget/v/AustralianelEctorates.DetailMaps.Portrait.svg?label=DetailMaps.Portrait)](https://www.nuget.org/packages/AustralianelEctorates.DetailMaps.Portrait/)

All information about electorates is available at [/Data/electorates.json](/Data/electorates.json).


## Electorates

Location: [/Data/electorates.json](/Data/electorates.json)

Sample:

<!-- snippet: Snippets.ElectoratesSampleJson.verified.txt -->
<a id='snippet-Snippets.ElectoratesSampleJson.verified.txt'></a>
```txt
[
  {
    "Name": "Canberra",
    "ShortName": "canberra",
    "State": "ACT",
    "Area": 312.0,
    "Exist2016": true,
    "Exist2019": true,
    "ExistInFuture": true,
    "DateGazetted": "2018-07-13",
    "Description": "<p>The Division of Canberra covers an area in central ACT consisting of the Districts of:</p><ul><li>Canberra Central,</li><li>Kowen,</li><li>Majura,</li><li>part of Belconnen,</li><li>part of Jerrabomberra,</li><li>part of Molonglo Valley,</li><li>part of Weston Creek, and</li><li>part of Woden Valley</li></ul>",
    "DemographicRating": "<strong>Inner Metropolitan</strong> – situated in capital cities and consisting of well-established built-up suburbs",
    "NameDerivation": "A locality name derived from an Aboriginal word which is held to mean 'meeting place'.",
    "Enrollment": 95348,
    "TwoCandidatePreferred": {
      "Elected": {
        "FamilyName": "Payne",
        "GivenNames": "Alicia",
        "PartyCode": "ALP",
        "Votes": 57961,
        "Swing": 4.14,
        "PartyId": 200
      },
      "Other": {
        "FamilyName": "Zaki",
        "GivenNames": "Mina",
        "PartyCode": "LP",
        "Votes": 28442,
        "Swing": -4.14,
        "PartyId": 177
      }
    },
    "Locations": [
      {
        "Postcode": 2601,
        "Localities": [
          "Acton",
          "Canberra Central",
          "Canberra City"
        ]
      },
      {
        "Postcode": 2602,
        "Localities": [
          "Ainslie",
          "Dickson",
          "Downer",
          "Hackett",
          "Lyneham",
          "O'Connor",
```
<sup><a href='/src/Tests/Snippets.ElectoratesSampleJson.verified.txt#L1-L50' title='Snippet source file'>snippet source</a> | <a href='#snippet-Snippets.ElectoratesSampleJson.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Localities

Location: [/Data/Localities.json](/Data/Localities.json)

Sample:

<!-- snippet: Snippets.LocalitiesSampleJson.verified.txt -->
<a id='snippet-Snippets.LocalitiesSampleJson.verified.txt'></a>
```txt
[
  {
    "Place": "Acton",
    "Postcode": 2601,
    "Electorate": "CANBERRA"
  },
  {
    "Place": "Canberra Central",
    "Postcode": 2601,
    "Electorate": "CANBERRA"
  },
  {
    "Place": "Canberra City",
    "Postcode": 2601,
    "Electorate": "CANBERRA"
  },
  {
    "Place": "Ainslie",
    "Postcode": 2602,
    "Electorate": "CANBERRA"
  },
```
<sup><a href='/src/Tests/Snippets.LocalitiesSampleJson.verified.txt#L1-L21' title='Snippet source file'>snippet source</a> | <a href='#snippet-Snippets.LocalitiesSampleJson.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Parties

Location: [/Data/parties.json](/Data/parties.json)

Sample:

<!-- snippet: Snippets.PartiesSampleJson.verified.txt -->
<a id='snippet-Snippets.PartiesSampleJson.verified.txt'></a>
```txt
[
  {
    "Id": 1339,
    "Name": "Animal Justice Party",
    "Code": "AJP",
    "Abbreviation": "AJP",
    "RegisterDate": "3 May 2011",
    "AmendmentDate": "3 May 2011",
    "Address": "PO Box Q1688\nQUEEN VICTORIA BUILDING NSW 1230",
    "Officer": {
      "Title": "Ms",
      "FamilyName": "Bellenger",
      "GivenNames": "Carol",
      "Address": {
        "Line1": "8/80 John Street",
        "Suburb": "Pyrmont",
        "State": "NSW",
        "Postcode": 2009
      }
    },
    "DeputyOfficers": [
      {
        "Title": "Mr",
        "FamilyName": "Poon",
        "GivenNames": "Bruce ",
        "Address": {
          "Line1": "4/15 Exploration Lane",
          "Suburb": "Melbourne",
          "State": "VIC",
          "Postcode": 3000
        }
      },
      {
        "Title": "Mr",
```
<sup><a href='/src/Tests/Snippets.PartiesSampleJson.verified.txt#L1-L34' title='Snippet source file'>snippet source</a> | <a href='#snippet-Snippets.PartiesSampleJson.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Detail Maps

AEC publishes pdf electorate maps with some extra detail. eg:

 * Rivers
 * Main Roads
 * Neighboring electorates

The size of these pdfs is significant. The smaller file variants are approx 600MB in total.

To change these to a more manageable size and format, they have been converted to png and are now approx 30MB in total. Located in [/Data/DetailMaps](/Data/DetailMaps).

There are 3 variants of these files:

 * Default: A direct conversion from the AEC pdf. Have no suffix ie `electorate.png`.
 * Landscape: Converted to landscape. Has the suffix `_landscape.` ie `electorate_landscape.png`.
 * Portrait: Converted to portrait. Has the suffix `_portrait.` ie `electorate_portrait.png`.


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

The NuGets contain a static copy of the electorate data. This data is embedded as resources inside the assembly. No network calls are done.

 * [AustralianElectorates](https://nuget.org/packages/AustralianElectorates/)
 * [AustralianElectorates.Bogus](https://nuget.org/packages/AustralianElectorates.Bogus/)
 * [AustralianElectorates.DetailMaps](https://nuget.org/packages/AustralianElectorates.DetailMaps/)
 * [AustralianElectorates.DetailMaps.Landscape](https://nuget.org/packages/AustralianElectorates.DetailMaps.Landscape/)
 * [AustralianElectorates.DetailMaps.Portrait](https://nuget.org/packages/AustralianElectorates.DetailMaps.Portrait/)

To get the latests version of the data do a NuGet update. There are several options to help keep a NuGet update:

 * [Dependabot](https://dependabot.com/): Creates pull requests to keep dependencies secure and up-to-date.
 * [Using NuGet wildcards](https://docs.microsoft.com/en-us/nuget/reference/package-versioning#version-ranges-and-wildcards).
 * [Libraries.io](https://libraries.io/): Supports subscribing to NuGet package updates.


## Usage

<!-- snippet: usage -->
<a id='snippet-usage'></a>
```cs
// get an electorate by name
var fenner = DataLoader.Fenner;
Debug.WriteLine(fenner.Description);

// get an electorate by string
var canberra = DataLoader.Electorates.Single(x => x.Name == "Canberra");
Debug.WriteLine(canberra.Description);

// get an electorates maps (geojson) by string
var fennerGeoJson2016 = DataLoader.Fenner.Get2016Map();
Debug.WriteLine(fennerGeoJson2016);
var fennerGeoJson2019 = DataLoader.Fenner.Get2019Map();
Debug.WriteLine(fennerGeoJson2019);
var futureFennerGeoJson = DataLoader.Fenner.GetFutureMap();
Debug.WriteLine(futureFennerGeoJson);

// get an electorates maps (geojson) by string
var canberraGeoJson2016 = DataLoader.Maps2016.GetElectorate("Canberra");
Debug.WriteLine(canberraGeoJson2016);
var canberraGeoJson2019 = DataLoader.Maps2019.GetElectorate("Canberra");
Debug.WriteLine(canberraGeoJson2019);
var futureCanberraGeoJson = DataLoader.MapsFuture.GetElectorate("Canberra");
Debug.WriteLine(futureCanberraGeoJson);

// export all data to a directory
// structure will be
// /electorates.json
// /2016 (2016 states and australia geojson files)
// /2016/Electorates (2016 electorate geojson files)
// /2019 (2019 states and australia geojson files)
// /2019/Electorates (2019 electorate geojson files)
// /Future (future states and australia geojson files)
// /Future/Electorates (future electorate geojson files)
var directory = Path.Combine(Environment.CurrentDirectory, "Maps");
Directory.CreateDirectory(directory);
DataLoader.Export(directory);
```
<sup><a href='/src/Tests/Snippets.cs#L19-L56' title='Snippet source file'>snippet source</a> | <a href='#snippet-usage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Bogus Usage

<!-- snippet: usagebogus -->
<a id='snippet-usagebogus'></a>
```cs
var faker = new Faker<Target>()
    .RuleFor(
        property: u => u.RandomElectorate,
        setter: (f, _) => f.AustralianElectorates().Electorate())
    .RuleFor(
        property: u => u.RandomElectorateName,
        setter: (f, _) => f.AustralianElectorates().Name());
var targetInstance = faker.Generate();
```
<sup><a href='/src/Tests/Snippets.cs#L88-L98' title='Snippet source file'>snippet source</a> | <a href='#snippet-usagebogus' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## DetailMaps Usage

<!-- snippet: usageDetailMaps -->
<a id='snippet-usagedetailmaps'></a>
```cs
var pathToPng = DetailMaps.MapForElectorate("Bass");
```
<sup><a href='/src/Tests/Snippets.cs#L62-L64' title='Snippet source file'>snippet source</a> | <a href='#snippet-usagedetailmaps' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Copyright


### Code

The code in this repository is licensed under MIT.

> Copyright &copy; 2018 Commonwealth of Australia (Department of the Prime Minister and Cabinet)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy
> of this software and associated documentation files (the "Software"), to deal
> in the Software without restriction, including without limitation the rights
> to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
> copies of the Software, and to permit persons to whom the Software is
> furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all
> copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
> IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
> FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
> AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
> LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
> OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
> SOFTWARE.


### Content/Data

The content/data that is rendered (all files under [/Data/](/Data/)) is sourced from the [Australian Electoral Commission (AEC)](https://www.aec.gov.au/) and remains under the [AEC Copyright](https://www.aec.gov.au/footer/Copyright.htm).

The content/data in this repository does not necessarily represent the latest data made available by the AEC. The Department of the Prime Minister and Cabinet gives no warranty regarding the accuracy, completeness, currency or suitability of the content/data for any particular purpose.

This product (AustralianElectorates) incorporates data that is:  
© Commonwealth of Australia (Australian Electoral Commission) 2018

The Data (Commonwealth Electoral Boundaries (various years)) has been used in AustralianElectorates with the permission of the Australian Electoral Commission. The Australian Electoral Commission has not evaluated the Data as altered and incorporated within AustralianElectorates, and therefore gives no warranty regarding its accuracy, completeness, currency or suitability for any particular purpose.

Limited End-user licence provided by the Australian Electoral Commission:

> You may use AustralianElectorates to load, display, print and reproduce views obtained from the Data, retaining this notice, for your personal use, or use within your organisation only.


## Re-Generating the data

Note: The below are only required by the maintainers and contributors of this project. They are not required when consuming the NuGet package.

Some tools are required.


### Adding new elections

Elections are currently added manually as they are declared.

<!-- snippet: elections -->
<a id='snippet-elections'></a>
```cs
return new()
{
    new()
    {
        Parliament = 45,
        Year = 2016,
        Date = new(2016, 07, 02, 0, 0, 0),
        Electorates = Electorates.Where(_ => _.Exist2016).ToList()
    },
    new()
    {
        Parliament = 46,
        Year = 2019,
        Date = new(2019, 05, 18, 0, 0, 0),
        Electorates = Electorates.Where(_ => _.Exist2019).ToList()
    }
};
```
<sup><a href='/src/AustralianElectorates/DataLoader.cs#L79-L99' title='Snippet source file'>snippet source</a> | <a href='#snippet-elections' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


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



## Icon

Icon designed by [Iconathon](https://thenounproject.com/Iconathon1) from [The Noun Project](https://thenounproject.com).
