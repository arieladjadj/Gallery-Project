var Albums = require('./albums.dao');
var Pictures = require('./pictures.dao');

exports.createAlbum = function (req, res, next) {
    var album = {
        name: req.body.name,
        creationDate: req.body.creationDate,
        userId: req.body.userId
    };

    Albums.create(album, function(err, album) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "Album created successfully"
        })
    })
}

exports.getAlbums = function(req, res, next) {
    Albums.get({}, function(err, albums) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            albums: albums
        })
    })
}

exports.getAlbumsOfUser = function(req, res, next) {
    Albums.get({userId : req.query.userId }, function(err, albums) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            albums: albums
        })
    })
}

exports.getAlbum = function(req, res, next) {
    Albums.get({userId: req.query.name}, function(err, Albums) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            Albums: Albums
        })
    })
}

exports.updateAlbum = function(req, res, next) {
    var Album = {
        name: req.body.name,
        creationDate: req.body.creationDate,
        userId: req.body.userId
    };
    Albums.update({_id: req.query.id}, Album, function(err, Album) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "Album updated successfully"
        })
    })
}

exports.removeAlbum = function(req, res, next) {
    Pictures.deleteMany({albumId : req.query.id});
    Albums.delete({_id: req.query.id}, function(err, Album) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "Album deleted successfully"
        })
    })
}