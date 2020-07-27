function initialize() {
    var markers = [];
    var map = new google.maps.Map(document.getElementById("map"), {
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
    var lat = parseFloat(51.5195421);
    var lon = parseFloat(-0.154301);
    var defaultBounds = new google.maps.LatLng(lat, lon);
    var image = {
        url: "/Content/images/pin.png",
        scaledSize: new google.maps.Size(50, 50)
    }
    var markerForPlace = new google.maps.Marker({
        map: map,
        icon: image,
        position: defaultBounds,
        animation: google.maps.Animation.DROP,
    });
    markerForPlace.setMap(map);

    map.setCenter(defaultBounds);
    map.setZoom(17);
}
google.maps.event.addDomListener(window, "load", initialize);

$('#contactForm').validate({
    rules: {
        EmailAddress: {
            required: true,
            email: true
        }
    }
});