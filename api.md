# Gallery API:



## Users:

### <span style="text-decoration: underline">Get all users:</span>

###### 	Request

​	GET: http::/127.0.0.1:5000/api/users

###### 	Response

```
{
  "users": [
    {
      "_id": "5e8daf23a6c420873ca3fdab",
      "name": "User 1",
      "__v": 0
    },
    {
      "_id": "5e8daf27a6c420873ca3fdac",
      "name": "User 2",
      "__v": 0
    },
    {
      "_id": "5e8daf2aa6c420873ca3fdad",
      "name": "User 3",
      "__v": 0
    }
  ]
}
```

### <span style="text-decoration: underline">Create User:</span>

###### 	Request

​	POST: http::/127.0.0.1:5000/api/createUser 

​	Param: name=XXXX

###### 	Response	

````````````
{
  "message": "User created successfully"
}
````````````

### <span style="text-decoration: underline">Update User:</span>

###### 	Request

​	POST: http::/127.0.0.1:5000/api/updateUser?id=XXXX

​	Param: name=XXXX

###### 	Response	

````````````
{
  "message": "User updated successfully"
}
````````````

### <span style="text-decoration: underline">Delete User:</span>

###### 	Request

GET: http::/127.0.0.1:5000/removeUser?id=XXXX

###### Response

````
{
  "message": "User deleted successfully"
}
````

## Albums:

### <span style="text-decoration: underline">Get all albums:</span>

###### 	Request

​	GET: http::/127.0.0.1:5000/api/albums

###### 	Response

```
{
  "albums": [
    {
        "_id" : ObjectId("5e922e52dfba1257d0442a53"),
        "name" : "Album 1",
        "creationDate" : "12/4/2020 - 00:47:26",
        "userId" : "5e922d5cdfba1257d0442a52",
        "__v" : 0
	},
	{
        "_id" : ObjectId("5e9257985b1c379dccfa1b31"),
        "name" : "Album 2",
        "creationDate" : "12/4/2020 - 12:21:10",
        "userId" : "5e922d5cdfba1257d0442a52",
        "__v" : 0
	},
	{
        "_id" : ObjectId("5e92eba4f0ee6c3e4c2edb54"),
        "name" : "Album 1",
        "creationDate" : "12/4/2020 - 12:21:24",
        "userId" : "5e92eb9df0ee6c3e4c2edb53",
        "__v" : 0
	}
  ]
}
```



### <span style="text-decoration: underline">Get all albums of user:</span>

###### 	Request

​	GET: http::/127.0.0.1:5000/api/albumsOfUser?userId=XXXX

###### 	Response

```
{
  "albums": [
    {
        "_id" : ObjectId("5e922e52dfba1257d0442a53"),
        "name" : "Album 1",
        "creationDate" : "12/4/2020 - 00:47:26",
        "userId" : "5e922d5cdfba1257d0442a52",
        "__v" : 0
	},
	{
        "_id" : ObjectId("5e9257985b1c379dccfa1b31"),
        "name" : "Album 2",
        "creationDate" : "12/4/2020 - 12:21:10",
        "userId" : "5e922d5cdfba1257d0442a52",
        "__v" : 0
	}
  ]
}
```



### <span style="text-decoration: underline">Create Album:</span>

###### 	Request

​	POST: http::/127.0.0.1:5000/api/createAlbum

​	Param: 

​			name=XXXX,

​			creationDate:   d/M/yyyy - HH:mm:ss,

   		 userId: XXXX

​	{Eg. name=Album 2&creationDate=12/4/2020 - 12:21:10&userId=5e922d5cdfba1257d0442a52}

###### 	Response	

````````````
{
  "message": "Album created successfully"
}
````````````

### <span style="text-decoration: underline">Update Album:</span>

###### 	Request

​	POST: http::/127.0.0.1:5000/api/updateAlbum?id=xxx 

​	Param: 

​			name=XXXX,

​			creationDate:   d/M/yyyy - HH:mm:ss,

   		 userId: XXXX

​	{Eg. name=Album 2&creationDate=12/4/2020 - 12:21:10&userId=5e922d5cdfba1257d0442a52}

###### 	Response	

````````````
{
  "message": "Album updated successfully"
}
````````````

### <span style="text-decoration: underline">Delete Album:</span>

###### 	Request

GET: http::/127.0.0.1:5000/removeAlbum?id=xxxx

###### Response

````
{
  "message": "Album deleted successfully"
}
````

## Pictures:

### <span style="text-decoration: underline">Get all pictures:</span>

###### 	Request

​	GET: http::/127.0.0.1:5000/api/pictures

###### 	Response

```
{
  "pictures": [
   {
        "_id" : ObjectId("5e930dceb28ce5cd2d170e89"),
        "name" : "Cat 1",
        "location" : "http://127.0.0.1:5000/data/cat-1.jpg",
        "creationDate" : "12/4/2020 - 14:50:36",
        "albumId" : "5e922e52dfba1257d0442a53"
	}
	{
        "_id" : ObjectId("5e930e7ea84a957e217819ca"),
        "name" : "Cat 2",
        "location" : "http://127.0.0.1:5000/data/cat-2.jpg",
        "creationDate" : "12/4/2020 - 14:50:40",
        "albumId" : "5e922e52dfba1257d0442a53"
	}
  ]
}
```



### <span style="text-decoration: underline">Get all pictures of user:</span>

###### 	Request

​	GET: http::/127.0.0.1:5000/api/PicturesOfAlbum?albumId=XXXX

###### 	Response

```
{
  "pictures": [
   {
        "_id" : ObjectId("5e930dceb28ce5cd2d170e89"),
        "name" : "Cat 1",
        "location" : "http://127.0.0.1:5000/data/cat-1.jpg",
        "creationDate" : "12/4/2020 - 14:50:36",
        "albumId" : "5e922e52dfba1257d0442a53"
	}
	{
        "_id" : ObjectId("5e930e7ea84a957e217819ca"),
        "name" : "Cat 2",
        "location" : "http://127.0.0.1:5000/data/cat-2.jpg",
        "creationDate" : "12/4/2020 - 14:50:40",
        "albumId" : "5e922e52dfba1257d0442a53"
	}
  ]
}
```



### <span style="text-decoration: underline">Create Picture:</span>

###### 	Request

​	POST: http::/127.0.0.1:5000/api/createPicture

​	Param: 

​			name=XXXX,

​			location=XXX  (?)

​			creationDate:   d/M/yyyy - HH:mm:ss,

   		 albumId: XXXX

​	{Eg. name=CT 1&location=XXX&creationDate=12/4/2020 - 12:21:10&albumId=5e922d5cdfba1257d0442a52}

###### 	Response	

````````````
{
  "message": "Picture created successfully"
}
````````````

### <span style="text-decoration: underline">Update Picture:</span>

###### 	Request

​	POST: http::/127.0.0.1:5000/api/updatePicture?id=xxx 

​	Param:  

​			name=XXXX,

​			location=XXX  (?)

​			creationDate:   d/M/yyyy - HH:mm:ss,

   		 userId: XXXX

​	{Eg. name=cat 2&location=XXX&creationDate=12/4/2020 - 12:21:10&albumId=5e922d5cdfba1257d0442a52}

###### 	Response	

````````````
{
  "message": "Picture updated successfully"
}
````````````

### <span style="text-decoration: underline">Delete Picture:</span>

###### 	Request

GET: http::/127.0.0.1:5000/removePicture?id=xxxx

###### Response

````
{
  "message": "Picture deleted successfully"
}
````













