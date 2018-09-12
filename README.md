

## Electorate information

All information about electorates is avaliable at [/Data/electorates.json](https://github.com/SimonCropp/AustralianElectorates/blob/master/Data/electorates.json)

### Example content

```
[
  {
    "Name":"Canberra",
    "State":"ACT",
    "Description":"<p>The Division of Canberra covers an area in central ACT consisting of the Districts of:</p><ul><li>Canberra Central,</li><li>Kowen,</li><li>Majura,</li><li>part of Belconnen,</li><li>part of Jerrabomberra,</li><li>part of Molonglo Valley,</li><li>part of Weston Creek, and</li><li>part of Woden Valley</li></ul>",
    "Area":312.0,
    "ProductsAndIndustry":"Mainly residential with tourism, retail and some light industry at Fyshwick and Beard",
    "NameDerivation":"A locality name derived from an Aboriginal word which is held to mean 'meeting place'.",
    "DateGazetted":"2018-07-13T00:00:00",
    "Members":[
      {
        "Name":"Brodtmann, G",
        "Begin":2010,
        "Party":"ALP"
      },
      {
        "Name":"Ellis, A",
        "Begin":1998,
        "End":2010,
        "Party":"ALP"
      }
    ],
    "DemographicRating":"<strong>Inner Metropolitan</strong> - situated in capital cities and consisting of well-established built-up suburbs"
  },
```

## Maps

All maps are in [geojson format](http://geojson.org/).

 * Future (Next election) [/Data/Maps/Future](https://github.com/SimonCropp/AustralianElectorates/tree/master/Data/Maps/Future)
 * 2016 (Previous election) [/Data/Maps/2016](https://github.com/SimonCropp/AustralianElectorates/tree/master/Data/Maps/2016)

### Structure

Each of the above groupings have the following structure.

 * Australia and state "combined electorate" maps are at the root.
 * Specific elctorate maps are located inside a subdirectry based on the state they exist in.

### Map variants

Each maps has multiple variants based on the combination of simplification and trimming.

With the below two options combined, there are eight different options for each map.

#### Simplication

Simplication uses [MapShaper simplify option](https://github.com/mbloch/mapshaper/wiki/Command-Reference#-simplify)

> Visvalingam simplification iteratively removes the least important point from a polyline. The importance of points is measured using a metric based on the geometry of the triangle formed by each non-endpoint vertex and the two neighboring vertices
 
The level of simplifation is represented as a percent number. 20, 10 and 5. representing 20%, 10% and 5%. The smalled the number the smaller the file, but with the loss of some accuracy.  

#### Trimming

Trimming removes small islands to reduce the file size. Maps that have been trimmed have `trimmed` in the file name.

## Copyright

The code in this repos is licensed under MIT.

The content that is dendered is source from the [Australian Electoral Commision (AEC)](https://www.aec.gov.au/). See the [AEC Copyright](https://www.aec.gov.au/footer/Copyright.htm)  for more information on data usage.

## Re-Generating the data

### MapShaper

[MapShaper](https://github.com/mbloch/mapshaper/) is required for GIS operations.

Installation:

* Install [Node](https://nodejs.org/)
* [Install MapShaper globally](https://github.com/mbloch/mapshaper#installation) `npm install -g mapshaper`

https://github.com/mbloch/mapshaper/wiki/Command-Reference