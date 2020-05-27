$(document).ready(function () {
    var sTop = $(window).scrollTop();
    if (sTop > 10) {
        $('.main-menu').addClass("sticky");
    }
    $('.testimonials-carousel').owlCarousel({
        items: 1,
        loop: true,
        nav: true,
        dots: false,
        autoplay: true,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        mouseDrag: false,
        touchDrag: false,
        pullDrag: false,
        navText: ['<i class="fas fa-caret-left"></i>', '<i class="fas fa-caret-right"></i>']
    });
    $('.partners-carousel').owlCarousel({
        loop: true,
        nav: false,
        dots: false,
        autoplay: true,
        mouseDrag: false,
        touchDrag: false,
        pullDrag: false,
        responsive: {
            0: {
                items: 2
            },
            700: {
                items: 4
            },
            1200: {
                items: 6
            }
        },
    });
    //new TypeIt('.banner-text-con .banner-text h1', {
    //    speed: 50,
    //    cursor: false
    //}).go();
    var myText = "the biomedic is a medical centre for bioregulatory medicine.";
    var myWords = myText.split(" ").reverse();

    var ntrvl = setInterval(function () {
        addNextWord();
    }, 700);
    function addNextWord() {
        var nextWord = myWords.pop();
        if (nextWord !== undefined) {
            var textNow = $(".banner-text-con .banner-text h1").text() + " " + nextWord;
            $(".banner-text-con .banner-text h1").text(textNow);
        }
    }
});

$(document).scroll(function () {
    var sTop = $(window).scrollTop();

    if (sTop > 10) {
        $('.main-menu').addClass("sticky");
    }
    else {
        $('.main-menu').removeClass("sticky");
    }
});

function initialize() {
    var markers = [];
    var map = new google.maps.Map(document.getElementById("map"), {
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });
    var lat = parseFloat(51.5195454);
    var lon = parseFloat(-0.1564301);
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