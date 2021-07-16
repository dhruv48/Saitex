
function saveevent(evt) {
    
    evt = (evt) ? evt : window.event;
    var find_counter = 0;

    // code for alt ctrlKey and shiftKey
    if (evt.altKey || evt.ctrlKey || evt.shiftKey) {
    }

    if (evt.which == 0 && evt.keyCode >= 112 && evt.keyCode <= 121) {

        if (evt.keyCode == 112) {
            find_counter = 1;
            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].src.indexOf('clear.jpg') > 0) {
                    //if (confirm("Do you want to Clear?")) {
                    imgs[i].click();
                    //}
                }
            }
        }

        if (evt.keyCode == 113) {
            find_counter = 1;
            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].src.indexOf('edit1.jpg') > 0) {
                    //                            if (confirm("Do you want to Update?")) {
                    imgs[i].click();
                    //                            }
                }
            }
        }

        if (evt.keyCode == 114) {
            find_counter = 1;
            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].src.indexOf('save.jpg') > 0) {
                    //                            if (confirm("Do you want to save?")) {
                    imgs[i].click();
                    //                            }
                }
            }
        }

        var currFFZoom = 1;

        if (evt.keyCode == 115) {
            
            find_counter = 1;
            if ($("#Mastertr1").css('display') == 'none') {
                $("#Mastertr1").show();
                $("#Mastertr2").show();

//                var step = 0.1;
//                currFFZoom -= step;
//                $('body').css('MozTransform', 'scale(' + 1 + ')');
                                         
            }
            else {
                $("#Mastertr1").hide();
                $("#Mastertr2").hide();

//                var step = 0.12;
//                currFFZoom += step;
//                $('body').css('MozTransform', 'scale(' + currFFZoom + ')');
            }
        }

        //                if (evt.keyCode == 116) {
        //                    find_counter = 1;
        //                    var imgs = document.getElementById("btnsaveTRNDetail");
        //                    for (var i = 0; i < imgs.length; i++) {
        //                        if (imgs[i].src.indexOf('link_exit.png') > 0) {
        //                            imgs[i].click();
        //                        }
        //                    }
        //                }

        if (evt.keyCode == 117) {
            find_counter = 1;

            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].id.indexOf('btnsaveTRNDetail') > 0) {
                    imgs[i].click();
                }
            }
        }

        if (evt.keyCode == 118) {
            find_counter = 1;
            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].src.indexOf('link_exit.png') > 0) {
                    imgs[i].click();
                }
            }
        }

        if (evt.keyCode == 119) {
            find_counter = 1;
            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].src.indexOf('del6.png') > 0) {
                    //                            if (confirm("Do you want to Delete?")) {
                    imgs[i].click();
                    //                            }
                }
            }
        }

        if (evt.keyCode == 120) {
            find_counter = 1;
            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].src.indexOf('link_find.png') > 0) {
                    //                            if (confirm("Do you want to Find?")) {
                    imgs[i].click();
                    //                            }
                }
            }
        }

        if (evt.keyCode == 121) {
            find_counter = 1;
            var imgs = document.getElementsByTagName("input");
            for (var i = 0; i < imgs.length; i++) {
                if (imgs[i].src.indexOf('link_print.png') > 0) {
                    //                            if (confirm("Do you want to Print?")) {
                    imgs[i].click();
                    //                            }
                }
            }
        }

        if (evt.keyCode == 116) {
            return true;
        }

        if (find_counter == 0) {
            return true;
        }

        return false
    }
}
