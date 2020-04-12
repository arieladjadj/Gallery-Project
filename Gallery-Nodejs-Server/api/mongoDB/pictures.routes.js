var Pictures = require('./Pictures.controller');

module.exports = function(router) {
    router.get('/PicturesOfAlbum', Pictures.getPicturesOfAlbum);
    router.post('/createPicture', Pictures.createPicture);
    router.get('/Pictures', Pictures.getPictures);
    router.get('/Picture', Pictures.getPicture);
    router.post('/updatePicture', Pictures.updatePicture);
    router.delete('/removePicture', Pictures.removePicture);
}