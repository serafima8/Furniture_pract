
document.addEventListener('DOMContentLoaded', function () {
    const servicesLink = document.querySelector('a[href="#services-section"]'); // Ссылка "Услуги"
    const servicesSection = document.getElementById('services-section'); // Секция с услугами

    // Когда нажимают на "Услуги" в меню
    servicesLink.addEventListener('click', function (e) {
        e.preventDefault(); // Отменяем стандартное поведение якоря

        // Плавное отображение секции
        servicesSection.style.display = 'block'; // Показываем секцию с услугами

        // Прокручиваем страницу до секции
        window.scrollTo({
            top: servicesSection.offsetTop,
            behavior: 'smooth'
        });
    });
});