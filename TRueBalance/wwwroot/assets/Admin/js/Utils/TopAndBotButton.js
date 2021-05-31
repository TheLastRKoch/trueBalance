// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function() {scrollFunction()};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("btnTop").style.display = "block";
        document.getElementById("btnBot").style.display = "block";
    } else {
        document.getElementById("btnTop").style.display = "none";
        document.getElementById("btnBot").style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function TopFunction() {
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}

function BotFunction() {
   document.documentElement.scrollTop = document.body.scrollHeight; // For Chrome, Firefox, IE and Opera
}