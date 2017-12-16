var pictureData;
var label;

function setData(myPictureData, myLabel){
    pictureData = myPictureData;
    label = myLabel;
}

$("div#swipe_like").on("click", function () {
	swipeLike();
});

$("div#swipe_dislike").on( "click", function() {
	swipeDislike();
});
    
function swipe() {
	Draggable.create("#photo", {
		throwProps:true,
		onDragEnd:function(endX) {
	   		if(Math.round(this.endX) > 0 ) {
	   			swipeLike();
	   		}
	   		else {
	   			swipeDislike();
	   		}
		}
	});
}

function swipeLike() {

		var $photo = $("div.content").find('#photo');

		var swipe = new TimelineMax({repeat:0, yoyo:false, repeatDelay:0, onComplete:remove, onCompleteParams:[$photo]});
		swipe.staggerTo($photo, 0.8, {bezier:[{left:"+=400", top:"+=300", rotation:"60"}], ease:Power1.easeInOut});

		addNewProfile();
}

function swipeDislike() {

		var $photo = $("div.content").find('#photo');

		var swipe = new TimelineMax({repeat:0, yoyo:false, repeatDelay:0, onComplete:remove, onCompleteParams:[$photo]});
		swipe.staggerTo($photo, 0.8, {bezier:[{left:"+=-350", top:"+=300", rotation:"-60"}], ease:Power1.easeInOut});

		addNewProfile();
}

function remove(photo) {
	$(photo).remove();
}

function addNewProfile() {
	var names = ['FlyFit', 'Raw Gym', 'Gym', 'Ben Dunne', '1Ecape', 'DCU Gym', 'NS Fitness'][Math.floor(Math.random() * 7)];
	var ages = ['â‚¬20/m','â‚¬30/m','â‚¬60/m','â‚¬15/m','â‚¬40/m', 'â‚¬30/m', 'â‚¬60/monthly'][Math.floor(Math.random() * 7)]
	var likes = ['100','500','70','30','60', '10', '15'][Math.floor(Math.random() * 7)]
	var photos = ['1', '2', '3', '4', '5', '6', '7'][Math.floor(Math.random() * 7)]
    $("div.content").prepend('<div class="photo" id="photo" style="background-image:url(\'data:image/png;base64,' + pictureData + '\')">'
    + '<span class="meta">'
    //+ '<p>'+names+', '+ages+' ('+likes+' likes)</p>'
    + '<p>' + label + '</p>'
    + '</span>'
    + '</div>');

    swipe();
}