document.addEventListener('DOMContentLoaded', function () {
    // Находим все ссылки в меню
    document.querySelectorAll('.menu-link').forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault(); // Отключаем стандартное поведение ссылки

            const targetTabId = this.getAttribute('data-target'); // Получаем целевой ID

            // Скрываем все разделы
            document.querySelectorAll('.tab-content').forEach(function (tab) {
                tab.classList.remove('active');
            });

            // Показываем выбранный раздел
            document.getElementById(targetTabId).classList.add('active');
        });
    });
});
