// JScript File

//    document.onkeydown = KeyDownHandler;
//    document.onkeyup = KeyUpHandler; 
    
//    var CTRL = false;      
//    var SHIFT = false;     
//    var ALT = false;
//    var CHAR_CODE = -1;
    var URLF9;
    function KeyDownHandler(e)
        {
            var x = '';
            if (document.all)
            {
                var evnt = window.event;
                x = evnt.keyCode;               
            }
            else
            {
                x = e.keyCode;               
            }
            DetectKeys(x, true);
        }       
        function KeyUpHandler(e)
        {
            var x = '';
            if (document.all)
            {
                var evnt = window.event;
                x = evnt.keyCode;                
            }
            else
            {
                x = e.keyCode;                              
            }
            DetectKeys(x, false);
        }
        function DetectKeys(KeyCode, IsKeyDown)
        { 
       // window.alert(KeyCode);            
        // Function Key F9 Is Pressed
            if(KeyCode == '120')
            {
          //  window.open(URL,'_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,width=700,height=600');
          
                      }   
          
//            if (KeyCode == '16')
//            {
//                SHIFT = IsKeyDown;
//            }
//            else if (KeyCode == '17')
//            {
//                CTRL = IsKeyDown;
//            }
//            else if (KeyCode == '18')
//            {
//                ALT = IsKeyDown;
//            }
//            else
//            {
//                if(IsKeyDown)
//                    CHAR_CODE = KeyCode;
//                else
//                    CHAR_CODE = -1;
//            }
       }
    function SetURLF9(url)
    {          
       URLF9=url;      
    }
//    function ReplaceLowerCaseToUpperCase(KeyCode)
//    {
//        if(KeyCode>96)
//        {
//            if(KeyCode<123)
//            {          
//                var character = document.characterCode.character.value.substring(0,1);
//                var code = document.characterCode.character.value.charCodeAt(0);
//                var msg = "ASCII = "+code+"";
//                alert(msg);
//            }
//        }
//   }
 