var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var pictureSchema = new Schema ({
    name :{
        type: String,
        unique : false,
        required : true
    },
    location :{
        type: String,
        unique : false,
        required : true
    },
    creationDate :{
        type: String,
        unique : false,
        required : true
    },
    albumId :{
        type: String,
        unique : false,
        required : true
    }
});

module.exports = pictureSchema;
