var express = require('express');
var bodyParser = require('body-parser');

var properties = require('./config/properties');
var db = require('./config/database');
var app = express();
//User routes
var usersRoutes = require('./api/mongoDB/users.routes');
var albumsRoutes = require('./api/mongoDB/albums.routes')
var picturesRoutes = require('./api/mongoDB/pictures.routes')
//configure bodyparser
var bodyParserJSON = bodyParser.json();
var bodyParserURLEncoded = bodyParser.urlencoded({extended:true});

//initialise express router
var router = express.Router();

// call the database connectivity function
db();

// configure app.use()
app.use(bodyParserJSON);
app.use(bodyParserURLEncoded);

// Error handling
app.use(function(req, res, next) {
    res.setHeader("Access-Control-Allow-Origin", "*");
     res.setHeader("Access-Control-Allow-Credentials", "true");
     res.setHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
     res.setHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers,Authorization");
   next();
 });

// use express router
app.use('/api',router);
//call users routing
try{
usersRoutes(router);
albumsRoutes(router);
picturesRoutes(router);
}catch{

}

// intialise server
app.listen(properties.PORT, (req, res) => {
    console.log(`Server is running on ${properties.PORT} port.`);
})





