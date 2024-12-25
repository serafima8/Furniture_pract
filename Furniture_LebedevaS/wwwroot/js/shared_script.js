document.addEventListener('DOMContentLoaded', function () {
    window.addEventListener('scroll', function () {
        var header = document.getElementById('header-top');
        var scrollTop = window.scrollY;
        var maxScroll = 250;
        var opacity = Math.min(scrollTop / maxScroll, 1);
        header.style.backgroundColor = `rgb(67, 75, 77)`;
    });
    function toggleMenu() {
        const sideMenu = document.getElementById('side-menu');
        sideMenu.classList.toggle('active');
    }

    const hamburger = document.getElementById('hamburger');
    const sideMenu = document.getElementById('side-menu');
    const menuOverlay = document.getElementById('menu-overlay');

    function toggleMenu() {
        const sideMenu = document.getElementById('side-menu');
        const menuOverlay = document.getElementById('menu-overlay');

        sideMenu.classList.toggle('active');
        menuOverlay.classList.toggle('active');
    }
    hamburger.addEventListener('click', toggleMenu);

    // Закрытие меню при клике на оверлей
    menuOverlay.addEventListener('click', toggleMenu);


})