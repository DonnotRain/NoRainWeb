if ( typeof cordova !== "undefined") {
	function GetLocationPlugin() {
		this._callback;
	}

	GetLocationPlugin.prototype.test = function(testData1, testData2, cb) {
		this._callback = cb;
		return cordova.exec(cb, null, 'GetLocationPlugin', "test", [testData1, testData2]);
	};

	cordova.addConstructor(function() {
		console.log("plugin new");
		cordova.addConstructor(function() {
			cordova.addPlugin('GetLocationPlugin', new GetLocationPlugin());
		});
		if (!window.plugins) {
			window.plugins = {};
		}
		window.plugins.GetLocationPlugin = new GetLocationPlugin();
	});
} else {
	console.log("cordova is undefined!");
}

//if ( typeof cordova !== "undefined") {
//	var GetLocationPlugin = function() {
//	
//	}
//	
//	GetLocationPlugin.prototype.test = function(testData1, testData2, cb) {
//		//this._callback = cb;
//		return cordova.exec(cb, null, 'GetLocationPlugin', "test", [testData1, testData2]);
//	};
//
//	cordova.addConstructor(function() {
//		//cordova.addConstructor(function() {
//			cordova.addPlugin('GetLocationPlugin', new GetLocationPlugin());
//		//});
//		console.log("plugin new");
//		if (!window.plugins) {
//			window.plugins = {};
//		}
//		window.plugins.GetLocationPlugin = new GetLocationPlugin();
//	})
//} else {
//	console.log("cordova is undefined!");
//}