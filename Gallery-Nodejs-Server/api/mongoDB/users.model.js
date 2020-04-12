var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var userSchema = new Schema ({
    name :{
        type: String,
        unique : false,
        required : true
    }
});

module.exports = userSchema;
