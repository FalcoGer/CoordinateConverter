# CoordinateConverter
Convert coordinates to and from L/L, L/L Decimal, UTM and MGRS as well as Bullseye offsets.

# How to use

1. Select the coordinate system you want to convert from using the tabs in the input panel.
2. Enter the coordinates you wish to convert. More details on valid inputs down below.  
Invalid inputs will mark the relevant textbox pink to indicate the error.
3. When the input is valid, the conversion will be written to the output in various formats.

# L/L - Latitude/Longitude

## Usage for entering L/L
Coordinates may be entered with their units, such as  
`45° 23' 13.1234"`  
Spaces are optional and can be used to visualize the breaks between degrees, minutes and seconds.  
`45 23 13.1234`  
Leading zeros must be included and zeroed out minutes and seconds must be included as well. The decimal part is optional.  
`02 00 00` would be 2° N/S.  
For Longitude, 3 digits are required for the degrees.

## Explaination of L/L
L/L assumes a spherical earth. The earth is divided up into degrees, minutes and seconds.  
1° = 60', or 60 minutes  
and 1' = 60", or 60 seconds.  
Additional fractions of seconds may be added behind a decimal point.  
Each point in space has a unique coordinate, except for the poles at +/- 90° latitude, where any longitude displays the same point.

### Latitude
The North/South component is called Latitude. It divides the earth into 180 slices.
Slices north of the equator are positive, labeled N. Slices south of the equator are negative values, labeled S.
### Longitude

The East/West component is called Longitude and divides the earth into 360° slices of Longitude along the equator.
180 of those slices are west of the meridian, indicated with a negative value, or the letter W.
180 of them are east, indicated with a positive value or E.

# L/L - Decimal

## Usage for entering L/L Decimal
Coordinates may be entered with their units, such as  
`45° 23.1234'`  
Spaces are optional and can be used to visualize the breaks between degrees, minutes and seconds.  
`45 23.1234'`  
Invalid inputs will mark the textbox pink to indicate the error.
Leading zeroes must be included and zeroed out minutes and seconds must be included as well. The decimal part is optional.  
`02 00` would be 2° N/S.  
For Longitude, 3 digits are required for the degrees.

## Explaination of L/L Decimal
L/L Decimal uses the same coordinate system as L/L.  
Except for seconds and fractions of seconds, those are converted into fractions of whole minutes.

# MGRS

## Usage for entering MGRS
Enter the UTM Grid number indicating the Longitude band into the first textbox. Valid numbers are 01 through 60. Leading zeros are required.
Enter the Latitude band Index into the second textbox. Valid input is any letter, except O and I.  
Enter the sub grid ident, called digraph, into the third textbox
Enter your easting and your northing into the fourth textbox. Decimals are not allowed. Only even numbers of digits are allowed, including none, in which case 00 is assumed.  
`37 | T | GG | 46245002`

## Explaination of MGRS
MGRS, or Military Grid Reference System, uses the same grid layout as UTM.  
Each of those grids is divided into sub grids, indexed with two letters. Those grids are 10000m x 10000m size.  
From those sub grids you indicate a specific position with a set of numbers. Depending on the length of those numbers, you can indicate precision in up to 1m steps.  

Numbers&nbsp;&nbsp; | Precision \[m\]
--------------------|----------------
0                   | 100 000
2                   |  10 000
4                   |   1 000
6                   |     100
8                   |      10
10                  |       1

# UTM

## Usage for entering UTM
Enter the UTM Grid number indicating the Longitude band into the first textbox. Valid numbers are 01 through 60. Leading zeros are required.
Enter the Latitude band Index into the second textbox. Valid input is any letter, except O and I.  
Enter your easting and your northing in m into textbox 3 and 4. Decimals are allowed. Unit names are not allowed and meters are implied.  
`37 | T | 743300 | 4624500`

## Explaination of UTM
UTM, or Universal Transverse Mercator, divides the earth into a grid of 20 bands of latitude labeled C through X, omitting I and O, 60 bands of longitude labeled 01 through 60. From that grid's south west corner, you go to the east and to the north, indicated by the easting and northing part of the coordinates in meters.  
Some UTM grids have been adjusted to include countries so they are not split.  
The poles are separated into Grids A/B for the south pole and Y/Z for the north pole. Those grids are not supported.

# Bullseye

## Usage for entering Bullseye coordinates
A valid bullseye must be described by entering it's coordinates into the bullseye panel in L/L format. The same constraints as L/L conversion apply.  
Enter a bearing from 0 through 360 in whole degrees. Decimals are not allowed.  
Enter a range in whole nautical miles. Decimals are not allowed.

## Explaination of Bullseye
The bullseye is an arbitrary point chosen by a coalition in a military operation to be used as a reference point across all participants of a mission.  
From that point, any other point can be identified by an offset from this point in degrees, called a bearing, and a range, typically in nautical miles.

