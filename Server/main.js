jQuery(function() {
var wrap = $('#vidwrap');
var newImage = new Image();
var lastImage = new Image();
window.update = true;

//var disp = {n:new Image(),o:new Image()}
//var left = {n:new Image(),o:new Image()}
//var right = {n:new Image(),o:new Image()}

function updateImage() {
	if (!window.update) { return; }
	if(newImage.complete) {
		var hash = new Date().getTime();
		newImage = new Image();
		newImage.src = 'disp.jpg.' + hash;
		wrap.empty();
		wrap.append(lastImage);
		lastImage = newImage
	}
}
window.cancel = function() { window.update = false; }
window.start = function(s) { window.update = true; setInterval(updateImage, 1); }
window.start();
});