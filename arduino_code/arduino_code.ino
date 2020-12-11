#include <ESP8266HTTPClient.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <WiFiClientSecureBearSSL.h>

#include "DHT.h"

#define DHTPIN 4

#define DHTTYPE DHT22   // DHT 22  (AM2302), AM2321

DHT dht(DHTPIN, DHTTYPE);

// "BF C4 C7 46 34 FB F4 46 3A 8F 6D 59 17 15 EC 4A C5 48 97 E4"
const uint8_t fingerprint[20] = {0xBF, 0xC4, 0xC7, 0x46, 0x34, 0xFB, 0xF4, 0x46, 0x3A, 0x8F, 0x6D, 0x59, 0x17, 0x15, 0xEC, 0x4A, 0xC5, 0x48, 0x97, 0xE4};


void setup() {
  pinMode(5, OUTPUT);     // Initialize the LED_BUILTIN pin as an output
  Serial.begin(115200);                                  //Serial connection
  WiFi.begin("SSID", "PASSWORD");   //WiFi connection
 
  while (WiFi.status() != WL_CONNECTED) {  //Wait for the WiFI connection completion
 
    delay(500);
    Serial.println("Waiting for connection");
 
  }
 dht.begin();
}
 
void loop() {
 
 if(WiFi.status()== WL_CONNECTED){   //Check WiFi connection status

   float h = dht.readHumidity();
  // Read temperature as Celsius (the default)
  float t = dht.readTemperature();
  // Read temperature as Fahrenheit (isFahrenheit = true)
  float f = dht.readTemperature(true);

  // Check if any reads failed and exit early (to try again).
  if (isnan(h) || isnan(t) || isnan(f)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }




  

  String token = "https://yourdomain/api/token?type=board&name=nodemcu";  
  Serial.println(token);
  digitalWrite(5, HIGH);

  std::unique_ptr<BearSSL::WiFiClientSecure>client(new BearSSL::WiFiClientSecure);

  client->setFingerprint(fingerprint);
  
   HTTPClient https;    //Declare object of class HTTPClient
  // "BF C4 C7 46 34 FB F4 46 3A 8F 6D 59 17 15 EC 4A C5 48 97 E4"
   https.begin(*client,token);      //Specify request destination
   // http.setAuthorization("guest", "guest");
   // http.setAuthorization("Z3Vlc3Q6Z3Vlc3Q=");
   https.addHeader("Content-Type", "text/json");  //Specify content-type header
 
   int tokenCode = https.POST("");   //Send the request
   String token_data = https.getString();                  //Get the response payload
 
   Serial.println(tokenCode);   //Print HTTP return code
   Serial.println(token_data);    //Print request response payload
  
   https.end();  //Close connection
   
   String measure = "https://yourdomain/api/data?t="+String(t, 2)+"&h="+String(h,2);
   https.begin(*client,measure);      //Specify request destination
   https.addHeader("Content-Type", "text/json");  //Specify content-type header
   https.addHeader("Authorization","Bearer "+token_data);
   int measureCode = https.POST("");   //Send the request
   String measure_data = https.getString();                  //Get the response payload
 
   Serial.println(measureCode);   //Print HTTP return code
   Serial.println(measure_data);    //Print request response payload
  
   https.end();  //Close connection
   
   
    delay(1000);
   digitalWrite(5, LOW);
 
 }else{
 
    Serial.println("Error in WiFi connection");   
 
 }
  
  delay(30000);  //Send a request every 30 seconds
 
}
