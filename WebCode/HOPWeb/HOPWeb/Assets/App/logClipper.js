  function getEntry() {
    var entry = getQueryString("entry");
    return entry;
  }
  
  function getButton(event) {
    var butt ;
    butt = event.which;
    var button = "other";
    switch(butt){
        case 1:
        button = "left";
        break;
        case 3:
        button = "right";
        break;
    }
    return button;
  }

  function trackButton(event) {
    var button = getButton(event);
    logClick(button);
  }

  function logUA(){
    var i = new Image();
    var ua = navigator.userAgent || '';
    i.src="/yws/mapi/ilogrpt?method=putwcplog&keyfrom=wcp&ua="+encodeURIComponent(ua);
    return true;
  }
  
  function logEntry(){

    var i=new Image();

    i.src="/yws/mapi/ilogrpt?method=putwcplog&keyfrom=wcp&entry=" + getEntry();

    logUA();
 
    return true;
  }
  
  function logClick(click){
    var i=new Image();

    i.src="/yws/mapi/ilogrpt?method=putwcplog&keyfrom=wcp&entry=" + getEntry() +"&click=" + click;

    return true;
  }
  $(function() {
    logEntry();
    $('#indexWebclipperBtn').mousedown(function (e) {
        trackButton(e);
    }).dblclick(function (e) {
        logClick('doubleclick');
    });

    $('#clipper-bd').mousedown(function (e) {
        trackButton(e);
    }).dblclick(function (e) {
        logClick('doubleclick');
        });
    $('.down').click(function () {
        var img = new Image();
        img.src = 'http://note.youdao.com/yws/mapi/ilogrpt?method=putwcplog&chrome_clipper=download2';
        return;
    });
  });
