


window.addEventListener("load", function () {
  document.getElementById("loading").style.display = "none";
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
    820: {
      items: 2,
      margin: 2,
    },
    1000: {
      items: 3,
      margin: 3,
    },
  },
});
var swiper = new Swiper(".product-slider", {
  spaceBetween: 30,
  effect: "fade",
  loop: false,
  navigation: {
    nextEl: ".next",
    prevEl: ".prev",
  },
  on: {
    init: function () {
      var index = this.activeIndex;

      var target = $(".product-slider__item").eq(index).data("target");

     

      $(".product-img__item").removeClass("active");
      $(".product-img__item#" + target).addClass("active");
    },
  },
});

swiper.on("slideChange", function () {
  var index = this.activeIndex;

  var target = $(".product-slider__item").eq(index).data("target");

  console.log(target);

  $(".product-img__item").removeClass("active");
  $(".product-img__item#" + target).addClass("active");

  if (swiper.isEnd) {
    $(".prev").removeClass("disabled");
    $(".next").addClass("disabled");
  } else {
    $(".next").removeClass("disabled");
  }

  if (swiper.isBeginning) {
    $(".prev").addClass("disabled");
  } else {
    $(".prev").removeClass("disabled");
  }
});

$(window).scroll(function () {
  if ($(this).scrollTop() > 100) {
    $(".second-header").addClass("active");
  } else {
    $(".second-header").removeClass("active");
  }
});

$(document).ready(function() {
  let $btn = $(".close");
  let $lorem = $(".lorem");
  let isOpened = [false, false, false, false, false];

  let styl = {
    padding: "10px",
  };
  let style = {
    width: "100%",
    height: "50px",
  };
  $btn.css(style);
  $lorem.css(styl);

  $btn.each(function(index) {
    let $icon = $(this).find("i");
    if (!isOpened[index]) {
      $icon.removeClass("active");
      $(this).next().hide();
    }

    $(this).click(function() {
      $icon.toggleClass("active");
      if (isOpened[index]) {
        $(this).next().slideUp();
        isOpened[index] = false;
      } else {
        $(this).next().slideDown();
        isOpened[index] = true;
      }
    });
  });
});


$(".Gaming").mouseover(function () {
  $(".drone-text-second").addClass("active");
});

$(".Gaming").mouseout(function () {
  $(".drone-text-second").removeClass("active");
});




$(".gaming").mouseover(function () {
  $(".drone-text-seconds").addClass("active");
});

$(".gaming").mouseout(function () {
  $(".drone-text-seconds").removeClass("active");
});


$(".pages").mouseover(function () {
  $(".drone-text").addClass("active");
});

$(".pages").mouseout(function () {
  $(".drone-text").removeClass("active");
});

$(".Pages").mouseover(function () {
  $(".page-text").addClass("active");
});

$(".Pages").mouseout(function () {
  $(".page-text").removeClass("active");
});

$(".basket").click(function () {
  $(".basket-menu-exit").addClass("active");
  document.querySelector('body').style.overflow="hidden"
  
});
$(".exit").click(function (e) {
  e.stopPropagation()
  
  $(".basket-menu-exit").removeClass("active");
  document.querySelector('body').style.overflow="scroll"

});


$(".bars").click(function () {
  $(".bars-menu-exit").addClass("active");
  document.querySelector('body').style.overflow="hidden"
  
});
$(".exit").click(function (e) {
  e.stopPropagation()
  
  $(".bars-menu-exit").removeClass("active");
  document.querySelector('body').style.overflow="scroll"

});


