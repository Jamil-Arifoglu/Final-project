
const quantityEl = document.querySelector('.quantity__input');

const decrementBtn = document.querySelector('.quantity-decrement');

const incrementBtn = document.querySelector('.quantity-increment');


decrementBtn.addEventListener('click', () => {
  if (quantityEl.value > 1) {
    quantityEl.value--;
  }
});

incrementBtn.addEventListener('click', () => {
  quantityEl.value++;
});

// vertical gallery slider

jQuery(document).ready( function($) {

  $('.slider-main').slick({
      slidesToShow: 1,
      arrows: false,
      asNavFor: '.slider-nav',
      vertical: true,
      autoplay: false,
      verticalSwiping: true,
      centerMode: false
    });
    if($(window).width()>1180){
    $('.slider-nav').slick({
      slidesToShow: 5,
      arrows: false,
      asNavFor: '.slider-main',
      vertical: true,
      focusOnSelect: true,
       verticalSwiping: false,
      autoplay: false,
      centerMode: false
      
    });
  }
  else{
      $('.slider-nav').slick(
          {
              slidesToShow: 3,
              arrows: false,
              asNavFor: '.slider-main',
              vertical: true,
              focusOnSelect: true,
               verticalSwiping: false,
              autoplay: false,
              centerMode: false
            });

  }
    //on resizing the window fix to broken responsiveness
    $(window).on('resize orientationchange', function() {
      
      $('.slider-main').slick('unslick');
      $('.slider-nav').slick('unslick');

      $('.slider-main').slick({
          slidesToShow: 1,
          arrows: false,
          asNavFor: '.slider-nav',
          vertical: true,
          autoplay: false,
          verticalSwiping: true,
          centerMode: false
        });
        if($(window).width()>1000){
        $('.slider-nav').slick(
          {
              slidesToShow: 5,
              asNavFor: '.slider-main',
              arrows: false,
              vertical: true,
              focusOnSelect: true,
               verticalSwiping: false,
              autoplay: false,
              centerMode: false
            });
          }
          else{
              $('.slider-nav').slick(
                  {
                      slidesToShow: 3,
                      asNavFor: '.slider-main',
                      arrows: false,
                      vertical: true,
                      focusOnSelect: true,
                       verticalSwiping: false,
                      autoplay: false,
                      centerMode: false
                    });
          }


    });
      $('.nav-container .next').click(function(){
      $('.slider-nav').slick('slickNext');

    });
    $('.nav-container .prev').click(function(){
      $('.slider-nav').slick('slickPrev');

    });

});


var $owl = $(".owl-carousel");

$owl.owlCarousel({
  center: true,
  loop: true,

  items: 5,
  nav: true,
  navText: ["<", ">"],
  //autoplay: true
  responsive: {
    0: {
      items: 1,
      margin: 1,
    },
    640: {
      items: 2,
      margin: 3,
    },
    1000: {
      items: 2,
      margin: 2,
    },
    1180: {
      items: 4,
      margin: 3,
    },
  },
});





