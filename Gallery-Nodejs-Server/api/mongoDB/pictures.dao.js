var mongoose = require('mongoose');
var picturesSchema = require('./pictures.model');

picturesSchema.statics = {
    create : function(data, cb) {
        var picture = new this(data);
        picture.save(cb);         
    },

    get: function(query, cb) {
        this.find(query, cb);
    },

    getByName: function(query, cb) {
        this.find(query, cb);
    },

    update: function(query, updateData, cb) {
        this.findOneAndUpdate(query, {$set: updateData},{new: true}, cb);
    },

    delete: function(query, cb) {
        
        this.findOneAndDelete(query,cb);
    },
    deleteOfAlbum: function(query, cb){
        this.deleteMany(query, cb);
    }
}

var pictureModel = mongoose.model('pictures',picturesSchema);
module.exports = pictureModel;