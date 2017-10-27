$(function () {

  var userId=$("#userId").text();
  var myselfId=$("#myselfId").text();

  $(".button-collapse").sideNav();

  $("#status").click(function () {
    $("#tweetFooter").css("display","inline-block");
  });

  $(window).click(function() {
    $("#tweetFooter").css("display","none");
  });

  $('#status').click(function (event) {
      event.stopPropagation();
  });

  $("#tweetEnterButton").click(function(){
      var count = $("#status").val();
    var toolong = $("#tweetLetterCount").text();
    if(toolong > 140)
      alert("Tweet'iniz 140 karakterden büyük olamaz.");
    else if(!count )
      alert("Tweet boş olamaz.");
    else
      twitSend();
  });

  $('#tweetEnterButton').click(function(event){
      event.stopPropagation();
  });

  $("#status").keyup(function () {
      var count = $("#status").val().length;
    if(count <= 140)
      $("#tweetLetterCount").css("color","black");
    else
    $("#tweetLetterCount").css("color","red");

    $("#tweetLetterCount").text(count);
  });
});
