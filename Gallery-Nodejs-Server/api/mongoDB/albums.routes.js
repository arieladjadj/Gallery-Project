var Albums = require('./albums.controller');

module.exports = function(router) {
    router.get('/albumsOfUser', Albums.getAlbumsOfUser);
    router.post('/createAlbum', Albums.createAlbum);
    router.get('/albums', Albums.getAlbums);
    router.get('/album', Albums.getAlbum);
    router.post('/updateAlbum', Albums.updateAlbum);
    router.delete('/removeAlbum', Albums.removeAlbum);
}