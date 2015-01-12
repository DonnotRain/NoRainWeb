
var getLocation = function() {
	navigator.mylocation.getlocation("lat","lon", callback);
}

var callback = function(data) {
	console.log(data.lat + ", " + data.lon);
	localStorage.lat = data.lat;
	localStorage.lon = data.lon;
}
