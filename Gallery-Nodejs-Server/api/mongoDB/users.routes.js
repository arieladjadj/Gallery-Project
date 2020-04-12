var Users = require('./users.controller');

module.exports = function(router) {
    router.post('/createUser', Users.createUser);
    router.get('/users', Users.getUsers);
    router.get('/users/:name', Users.getUser);
    router.post('/updateUser', Users.updateUser);
    router.delete('/removeUser', Users.removeUser);
}