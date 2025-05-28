function initializeInactivityTimer(dotnetHelper) {
    var timer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, 3600000); // 60 minutos
    }

    function logout() {
        dotnetHelper.invokeMethodAsync("Logout");
    }

}