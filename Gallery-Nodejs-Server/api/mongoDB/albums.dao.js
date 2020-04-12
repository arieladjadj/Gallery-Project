var mongoose = require('mongoose');
var albumSchema = require('./albums.model');

albumSchema.statics = {
    create : function(data, cb) {
        var album = new this(data);
        album.save(cb);
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
    }
}

var albumModel = mongoose.model('albums',albumSchema);
module.exports = albumModel;