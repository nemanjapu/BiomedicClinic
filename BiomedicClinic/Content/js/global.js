var primaryMenu = $('#primaryNav');
var secondaryMenu = $('#secondaryNav');

$('#secondaryNavOpen').on('click', function () {
	if (primaryMenu.hasClass('open')) {
		primaryMenu.removeClass('open');
	}
	secondaryMenu.addClass('open');
});

$('#secondaryNavClose').on('click', function () {
	secondaryMenu.removeClass('open');
});

$('#primaryNavOpen').on('click', function () {
	if (secondaryMenu.hasClass('open')) {
		secondaryMenu.removeClass('open');
	}
	primaryMenu.addClass('open');
});
$('#primaryNavClose').on('click', function () {
	primaryMenu.removeClass('open');
});

AOS.init({
    disable: 'mobile',
    once: true,
    duration: 1000,
    easing: 'ease-in-out-quart'
});