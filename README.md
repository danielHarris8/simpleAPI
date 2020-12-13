# simpleAPI
just wanted to make a simple api to do somthing.

<table>
  <tr>
    <td><img src="https://user-images.githubusercontent.com/66697879/102014457-0d711880-3d91-11eb-826e-2f30fc0bcac7.jpg" height="400px" width="300px"></td>
    <td><img src="https://user-images.githubusercontent.com/66697879/102014483-31ccf500-3d91-11eb-91fe-1949a37649d1.png" height="400px" width="200px"></td>
  </tr>
</table>

## motivation
I learned Arduino, made an android app and a simple website, so I wanted to integrate them.
Currently, l used C# and javascript to make API, maybe I will use other programming languages to do the same thing.

## principle of work
First, The NodeMcu board requests a token from the server.
If no token, we cannot post our data to the server. It's an important thing because it ensures that the post of some data is permitted.
then, the board adds what token the board gets adding to the header and post what data get from the D22 sensor together.

Similarly, the Android app requests a token from the server beginning.
then, the app adds what token the app gets adding to the header and get data from the server. 
