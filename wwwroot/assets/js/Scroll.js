
var scrollPosition = window.pageXOffset || document.documentElement.scrollLeft || document.body.scrollLeft;

document.body.style.overflowX = 'hidden';


window.onscroll = function() {
  window.scrollTo(scrollPosition, window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop);
};




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
  
  
  