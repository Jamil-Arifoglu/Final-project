$(window).scroll(function() {
    if ($(this).scrollTop() > 10) { 
      $('.second-header').addClass('active');
    } else {
      $('.second-header').removeClass('active');
    }
  });