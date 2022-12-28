//Allow users to enter numbers only
$(document).ready(function () {
    //numeric only
    $(".number").on("keypress",function (event) {    
        //$(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    $('.number').keyup(function (event) {
        // skip for arrow keys
        if(event.which >= 37 && event.which <= 40) return;

        // format number
        $(this).val(function(index, value) {
            return value
            .replace(/\D/g, "")
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            ;
        });
    });


    //numeric with 2 digit decimal
    $('.number2digit').on("keypress",function (event) {    

        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });


    
    $('.number2digit').keyup(function (event) {
        if (event.which >= 37 && event.which <= 40) return;
        $(this).val(function(index, value) {
            return value
              // Keep only digits, decimal points, and dashes at the start of the string:
              .replace(/[^\d.-]|(?!^)-/g, "")
              // Remove duplicated decimal points, if they exist:
              .replace(/^([^.]*\.)(.*$)/, (_, g1, g2) => g1 + g2.replace(/\./g, ''))
              // Keep only two digits past the decimal point:
              .replace(/\.(\d{2})\d+/, '.$1')
              // Add thousands separators:
              .replace(/\B(?=(\d{3})+(?!\d))/g, ",")
        });
      // skip for arrow keys
      //if(event.which >= 37 && event.which <= 40) return;

     
    });

});


//$(".numericOnly").bind('keypress', function (e) {
//    if (e.keyCode == '9' || e.keyCode == '16') {
//        return;
//    }
//    var code;
//    if (e.keyCode) code = e.keyCode;
//    else if (e.which) code = e.which;
//    if (e.which == 46)
//        return false;
//    if (code == 8 || code == 46)
//        return true;
//    if (code < 48 || code > 57)
//        return false;
//});

////Disable paste
//$(".numericOnly").bind("paste", function (e) {
//    e.preventDefault();
//});

//$(".numericOnly").bind('mouseenter', function (e) {
//    var val = $(this).val();
//    if (val != '0') {
//        val = val.replace(/[^0-9]+/g, "")
//        $(this).val(val);
//    }
//});