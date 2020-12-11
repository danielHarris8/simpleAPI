const express = require('express');
const jwt = require('jsonwebtoken');

const app = express();

const dataPath = "./data.json";
const fs = require("fs");

app.post("/api/token", (req, res) => {
    const user = {
        type: req.type,
        name: req.name
    }
    jwt.sign({user}, 'secretkey', {expiresIn:'30s'}, (err, token) => {
       res.send(token);
    })
})

// Sent Data
app.post('/api/data', verifyToken ,(req, res) => {
    jwt.verify(req.token, 'secretkey', (err,authData) => {
        if(err){
            res.sendStatus(403)
        }else {
            
            fs.readFile(dataPath, "utf8", (err, data) => {
                if (err) throw err;
                //query , body , params
                // temperature humidity
                var input = { temperature: req.query.t, humidity:req.query.h};
                // console.log(data);
            
                var originData = JSON.parse(data);
                //console.log(originData);
                originData.push(input);
                //console.log(originData);
                fs.writeFile(dataPath, JSON.stringify(originData), (err) => {
                  if (err) throw err;
                  console.log("Data written to file");
                  console.log(input);
                  res.send(input);
                });
            });
        }
    })
})

//Get Data
app.get('/api/data', verifyToken ,(req, res) => {
    jwt.verify(req.token, 'secretkey', (err,authData) => {
        if(err){
            res.sendStatus(403)
        }else {
            fs.readFile(dataPath, 'utf8', (err, data) => {
                if (err)  throw err
                arr1 = JSON.parse(data)
                console.log("Data read to file");
                console.log(arr1.slice(-1)[0]);
                arr1 = arr1.slice(-1)[0]
                
                res.send(arr1);
            })
        }
    })
})

// Verify Token
function verifyToken(req, res, next){
    // Get auth header value
    const bearHeader = req.headers['authorization']
    // Check if bearer is undefine
    if(typeof bearHeader !== 'undefined'){
        // Split at the space
        const bearer = bearHeader.split(' ')
        // Get token from array
        const bearerToken = bearer[1];
        // Set the token
        req.token = bearerToken;
        // Next middleware
        next();
    }else{
        // Forbidden
        res.sendStatus(403)
    }

}

app.listen(3000, () => console.log('Server started on part 3000'));