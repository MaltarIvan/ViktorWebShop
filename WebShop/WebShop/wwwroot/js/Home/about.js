function myMap() {
    var latLng = { lat: 45.378724, lng: 13.921050};
    var mapOptions = {
        center: latLng,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
    var marker = new google.maps.Marker({
        position: latLng,
        map: map,
        title: 'Istra Mushrooms'
    });
}