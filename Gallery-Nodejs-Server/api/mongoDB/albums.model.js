var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var albumSchema = new Schema ({
    name :{
        type: String,
        unique : false,
        required : true
    }, creationDate :{
        type: String,
        unique : false,
        required : true
    },    userId :{
        type: String,
        unique : false,
        required : true
    }
});

module.exports = albumSchema;
