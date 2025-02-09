// var current_url = "http://localhost:44475";
var current_url = "http://localhost:9395";
makeScript = function(url){
    var script = document.createElement('script');
    script.setAttribute('src', url);
    script.setAttribute('type', 'text/javascript');
    document.getElementById('mainDiv').appendChild(script);
}