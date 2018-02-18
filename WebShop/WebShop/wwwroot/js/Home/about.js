function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(45.3799811, 13.9177101),
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
}