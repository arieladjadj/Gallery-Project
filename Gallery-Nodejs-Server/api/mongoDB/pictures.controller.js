var Pictures = require('./pictures.dao');

exports.createPicture = function (req, res, next) {
    var Picture = {
        name: req.body.name,
        location: req.body.path,
        creationDate: req.body.creationDate,
        albumId: req.body.albumId
    };

    Pictures.create(Picture, function(err, Picture) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "Picture created successfully"
        })
    })
}

exports.getPictures = function(req, res, next) {
    Pictures.get({}, function(err, Pictures) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            Pictures: Pictures
        })
    })
}

exports.getPicturesOfAlbum = function(req, res, next) {
    Pictures.get({albumId : req.query.albumId }, function(err, Pictures) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            Pictures: Pictures
        })
    })
}
exports.getPicture = function(req, res, next) {
    Pictures.get({_id: req.query.id}, function(err, Pictures) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            Picture: Pictures
        })
    })
}

exports.updatePicture = function(req, res, next) {
    var Picture = {
        name: req.body.name,
        location: req.body.path,
        creationDate: req.body.creationDate,
        albumId: req.body.albumId
    }
    Pictures.update({_id: req.query.id}, Picture, function(err, Picture) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "Picture updated successfully"
        })
    })
}

exports.removePicture = function(req, res, next) {
    Pictures.delete({_id: req.query.id  }, function(err, Picture) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "Picture deleted successfully"
        })
    })
}