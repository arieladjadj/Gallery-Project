var Users = require('./users.dao');
var Albums = require('./Albums.dao');
var Pictures = require('./pictures.dao');

exports.createUser = function (req, res, next) {
    var user = {
        name: req.body.name,
    };

    Users.create(user, function(err, user) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "User created successfully"
        })
    })
}

exports.getUsers = function(req, res, next) {
    Users.get({}, function(err, users) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            users: users
        })
    })
}

exports.getUser = function(req, res, next) {
    Users.get({name: req.params.name}, function(err, users) {
        if(err) {
            res.json({
                error: err
            })
        }
        res.json({
            users: users
        })
    })
}

exports.updateUser = function(req, res, next) {
    var user = {
        name: req.body.name,
    }
    Users.update({_id: req.query.id}, user, function(err, user) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "User updated successfully"
        })
    })
}

exports.removeUser = function(req, res, next) {
    Albums.get({userId:req.query.id}, function(err,albums){
        for(var pos in albums) {
            album = albums[pos];
           // console.log(album);
            Pictures.deleteMany({albumId : album._id});
         }
    });
    Albums.deleteMany({userId: req.query.id}, function(err, res){
        //console.log(res);
    })
    Users.delete({_id: req.query.id  }, function(err, user) {
        if(err) {
            res.json({
                error : err
            })
        }
        res.json({
            message : "user deleted successfully"
        })
    })
}