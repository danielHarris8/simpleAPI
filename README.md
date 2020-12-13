# simpleAPI
just wanted to make a simple api to do somthing.

## motivation
I learned Arduino, made an android app and a simple website, so I wanted to integrate them.
Currently, l used C# and javascript to make API, maybe I will use other programming languages to do the same thing.

## principle of work
First, The NodeMcu board requests a token from the server.
If no token, we cannot post our data to the server. It's an important thing because it ensures that the post of some data is permitted.
then, the board adds what token the board gets adding to the header and post what data get from the D22 sensor together.

Similarly, the Android app requests a token from the server beginning.
then, the app adds what token the app gets adding to the header and get data from the server. 
