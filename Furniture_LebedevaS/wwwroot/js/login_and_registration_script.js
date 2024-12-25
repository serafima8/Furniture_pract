document.addEventListener('DOMContentLoaded', function () {
    function hiddenOpen_Closeclick(container) {
        // Если container — строка, найти элемент
        if (typeof container === "string") {
            container = document.querySelector(container);
        }

        // Проверяем, является ли container DOM-элементом
        if (!(container instanceof HTMLElement)) {
            console.error("Invalid container or not an HTMLElement:", container);
            return;
        }

        // Меняем display, если он не определён, считаем его как "none"
        const currentDisplay = getComputedStyle(container).display;

        if (currentDisplay === "none") {
            container.style.display = "grid";
        } else {
            container.style.display = "none";
        }
    }

    const btn_singin = document.getElementById("click-to-hide");
    if (btn_singin) {
        btn_singin.addEventListener("click", function () {
            const element = document.querySelector(".container-login-registration");
            if (element) {
                hiddenOpen_Closeclick(element);
            }
        });
    }
    

    document.getElementById("hamburger-login").addEventListener("click", function () {
        hiddenOpen_Closeclick(".container-login-registration");
    });

    document.querySelector(".overlay").addEventListener("click", function () {
        hiddenOpen_Closeclick(".container-login-registration");
    });

    document.querySelector(".button_confirm_close").addEventListener("click", function () {
        hiddenOpen_Closeclick (".confirm-email-container");
    });

    const toggleElements = ['click-to-hide', 'overlay'];
    toggleElements.forEach(id => {
        const element = document.getElementById(id) || document.querySelector(`${id}`);
        if (element) {
            element.addEventListener('click', hiddenOpen_Closeclick);
        }
    });

    const signInBtn = document.querySelector(".signin-btn");
    const signUpBtn = document.querySelector(".signup-btn");
    const formBox = document.querySelector(".form-box");
    const block = document.querySelector(".block");

    if (signUpBtn) {
        signUpBtn.addEventListener('click', function () {
            if (formBox && block) {
                formBox.classList.add('active');
                block.classList.add('active');
            }
        });
    }

    if (signInBtn) {
        signInBtn.addEventListener('click', function () {
            if (formBox && block) {
                formBox.classList.remove('active');
                block.classList.remove('active');
            }
        });
    }

    setupFormSubmit('.form_btn_signin', '/Home/Login', {
        email: '#signin_email',
        password: '#signin_password'
    }, 'error-messages-singin');

    setupFormSubmit('.form_btn_signup', '/Home/Register', {
        login: '#signup_login',
        email: '#signup_email',
        password: '#signup_password',
        passwordReset: '#signup_confirm_password'
    }, 'error-messages-singup');

    function setupFormSubmit(buttonSelector, url, fields, errorContainerId) {
        const button = document.querySelector(buttonSelector);
        if (!button) return;

        button.addEventListener('click', function () {
            const formData = {};
            for (const [key, selector] of Object.entries(fields)) {
                const input = document.querySelector(selector);
                if (input) {
                    formData[key] = input.value;
                }
            }

            const errorContainer = document.getElementById(errorContainerId);
            sendRequest('POST', url, formData)
                .then(data => {
                    cleaningAndClosingForm(fields, errorContainer);
                    console.log('Успешный ответ:', data);

                    hiddenOpen_Closeclick(".confirm-email-container");
                    confirmEmail(data);
                })
                .catch(err => {
                    displayErrors(err, errorContainer);
                    console.error(err);
                });
        });
    }


    function sendRequest(method, url, body = null) {
        const headers = {
            'Content-Type': 'application/json'
        };

        return fetch(url, {
            method: method,
            body: JSON.stringify(body),
            headers: headers
        }).then(response => {
            if (!response.ok) {
                return response.json().then(errorData => {
                    throw errorData;
                });
            }
            return response.json();
        });
    }

    function displayErrors(errors, errorContainer) {
        if (!errorContainer) return;
        errorContainer.innerHTML = '';

        if (!Array.isArray(errors)) {
            errors = [errors.message || 'Произошла ошибка'];
        }

        errors.forEach(error => {
            const errorMessage = document.createElement('div');
            errorMessage.classList.add('error');
            errorMessage.textContent = error;
            errorContainer.appendChild(errorMessage);
        });
    }

    function cleaningAndClosingForm(fields, errorContainer) {
        if (errorContainer) {
            errorContainer.innerHTML = '';
        }

        for (const key in fields) {
            const input = document.querySelector(fields[key]);
            if (input) {
                input.value = ''; // Очищаем значение поля
            }
        }

        hiddenOpen_Closeclick(".container-login-registration");
    }
    const hamburgerMenu = document.getElementById('side-menu');
    const hamburger = document.getElementById('hamburger');
    const sideMenu = document.getElementById('side-menu');
    const menuOverlay = document.getElementById('menu-overlay');

    function toggleMenu() {
        sideMenu.classList.toggle('active');
        menuOverlay.classList.toggle('active');
    }

    hamburger.addEventListener('click', toggleMenu);
    menuOverlay.addEventListener('click', toggleMenu);

    // Логика для кнопки Вход
    const hamburgerLoginBtn = document.getElementById('hamburger-login');
    if (hamburgerLoginBtn) {
        hamburgerLoginBtn.addEventListener('click', () => {
            showRegistrationForm();
            toggleMenu(); // Закрываем меню
        });
    }

    window.addEventListener('scroll', function () {
        const header = document.getElementById('header-top');
        const scrollTop = window.scrollY;
        const maxScroll = 250;
        const opacity = Math.min(scrollTop / maxScroll, 1);
        header.style.backgroundColor = '#434B4D';
    });
    function showRegistrationForm() {
        const container = document.querySelector(".container-login-registration");
        if (container) {
            container.style.display = "grid";
        }
    }
    if (hamburgerLoginBtn) {
        hamburgerLoginBtn.addEventListener('click', () => {
            showRegistrationForm(); // Показываем форму
            toggleMenu(); // Закрываем меню
        });

    }

    function confirmEmail(body) {
        const sendConfirmButton = document.querySelector(".send_confirm");
        const errorContainer = document.getElementById('error-messages-confirm'); // Контейнер для ошибок

        if (!sendConfirmButton) return;

        sendConfirmButton.addEventListener('click', function () {
            // Получаем код из поля ввода
            body.codeConfirm = document.getElementById('code_confirm').value;

            const requestURL = '/Home/ConfirmEmail';

            // Отправляем запрос на сервер
            sendRequest('POST', requestURL, body)
                .then(data => {
                    console.log("Код подтверждения:", data);

                    // Закрываем окно подтверждения
                    hiddenOpen_Closeclick(".confirm-email-container");

                    // Обновляем интерфейс, чтобы отобразить "Выйти"
                    updateInterfaceAfterLogin();

                    // Перезагружаем страницу для обновления данных
                    location.reload();
                })
                .catch(err => {
                    // Отображаем ошибки, если запрос завершился неудачно
                    displayErrors(err, errorContainer);
                    console.log("Ошибка подтверждения:", err);
                });
        });
    }

    // Функция для обновления интерфейса после успешного входа
    function updateInterfaceAfterLogin() {
        const loginButton = document.querySelector("#login-button");
        if (loginButton) {
            loginButton.textContent = "Выйти";
            loginButton.href = "/logout"; // Меняем ссылку, чтобы выйти
        }
    }

    const google = document.querySelectorAll('.google');
    if (google) {
        google.forEach(btn => {
            btn.addEventListener('click', function () {
                window.location.href = `/Home/AuthenticationGoogle?returnUrl=${encodeURIComponent(window.location.href)}`;
            });
        });
    }
    const userIcon = document.getElementById('user-icon');
    const modal = document.getElementById('user-modal');
    const modalClose = document.getElementById('modal-close');

    if (userIcon && modal) {
        // Показать/скрыть всплывающее окно
        userIcon.addEventListener('click', function (event) {
            const isVisible = modal.style.display === 'block';
            modal.style.display = isVisible ? 'none' : 'block';
            event.stopPropagation(); // Чтобы не закрывать окно при клике на саму иконку
        });

        // Закрыть окно, если клик был вне иконки или всплывающего окна
        document.addEventListener('click', function (event) {
            if (!userIcon.contains(event.target) && !modal.contains(event.target)) {
                modal.style.display = 'none';
            }
        });
    }


    function showUserInfo() {
        // Получаем значения из формы
        const name = document.getElementById("signup_name").value;
        const birthDate = document.getElementById("signup_date").value;
        const address = document.getElementById("signup_address").value;
        const email = document.getElementById("signup_email").value;

        // Заполняем модальное окно
        document.getElementById("user-name").textContent = "Ф.И.О.: " + name;
        document.getElementById("user-date").textContent = "Дата рождения: " + birthDate;
        document.getElementById("user-address").textContent = "Город: " + address;
        document.getElementById("user-email").textContent = "Email: " + email;

        console.log(name, birthDate, address, email);
        // Открываем модальное окно
        const modal = document.getElementById("user-modal");
        modal.style.display = "block";
    }

    // Добавляем возможность закрыть модальное окно при клике вне его
    window.onclick = function (event) {
        const modal = document.getElementById("user-modal");
        if (event.target === modal) {
            modal.style.display = "none"; // Закрываем модальное окно
        }
    }

});