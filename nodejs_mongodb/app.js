const express = require('express');
const mongoose = require('mongoose');
const jwt = require('jsonwebtoken');

const Item = require('./models/item');

const app = express();

// Connect to MongoDB
// mongodb://mongo:27017/
mongoose.connect('mongodb://daniel:work#0911@localhost:27017/MyDb?authSource=admin',
    { useNewUrlParser: true, useUnifiedTopology: true}   
    
)
.then(() => console.log('MongoDB Connected'))
.catch(err => console.log(err));

app.post("/api/token", (req, res) => {
    const user = {
        type: req.query.type,
        name: req.query.name
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
            const newItem = new Item({
                temperature: req.query.t,
                humidity: req.query.h
            });
            
            newItem.save().then(item =>res.send({"temperature":item.temperature,"humidity":item.humidity}));
            
           
        }
    })
})

//Get Data
app.get('/api/data', verifyToken ,(req, res) => {
    jwt.verify(req.token, 'secretkey', (err,authData) => {
        if(err){
            res.sendStatus(403)
        }else {
            Item.findOne().sort({ _id: -1 }).then(item =>res.send({"temperature":item.temperature,"humidity":item.humidity}));
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

const port = 6000;

app.listen(port, () => console.log('Server running...'));