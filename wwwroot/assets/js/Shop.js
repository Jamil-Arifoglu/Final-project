// $(document).ready(function () {
//     let $btn = $("button");
//     let $lorem = $(".lorem");
    
  
   
//     let styl = {
//       padding: "10px",
//       display: "none",
//     };
//     let style = {
//       width: "100%",
//       height: "50px",
//       backgroundColor: "grey",
//     };
//     $btn.css(style);
//     $lorem.css(styl);
  
  
//     $(".btn").click(function () {
//       let $icon = $(this).find("i");
//       $icon.toggleClass("active");
     
//       $(this).next().slideToggle();
//       $(".lorem").not($(this).next()).slideUp();
      
//     });
//   });

// $(document).ready(function () {
//     let $btn = $("button");
//     let $lorem = $(".lorem");
    
  
   
//     let styl = {
//       padding: "10px",
//     };
//     let style = {
//       width: "100%",
//       height: "50px",
//       backgroundColor: "grey",
//     };
//     $btn.css(style);
//     $lorem.css(styl);
  
  
//     $(".btn").click(function () {
//       let $icon = $(this).find("i");
//       $icon.toggleClass("active");
     
//       $(this).next().slideToggle();
//       $(this).siblings(".lorem").slideUp();
      
//     });
// });


$(document).ready(function () {
    let $btn = $(".close");
    let $lorem = $(".lorem");
    let isOpened = [true, true, true,true,true  ]; 

    let styl = {
        padding: "10px",
    };
    let style = {
        width: "100%",
        height: "50px",
        
    };
    $btn.css(style);
    $lorem.css(styl);

    $(".close").click(function () {
        let index = $btn.index(this);
        let $icon = $(this).find("i");
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


// İstek listesi düğmelerini seçme




//   $(document).ready(function(){
//     $(".btn").click(function(){
//         let lorem=$(this).next();
//         $(lorem).toggleClass("lorem");
//         if($(lorem).hasClass("lorem")){
//             $(lorem).slideDown();
//         }
//         else{
//             $(lorem).slideUp();
//         }
//     });
// });



