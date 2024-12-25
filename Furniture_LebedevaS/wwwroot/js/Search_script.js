document.addEventListener('DOMContentLoaded', function () {
    const mySearch = document.querySelector('#mySearch');

    if (mySearch) {
        const clear = document.querySelector('.clear');
        if (clear) {
            clear.addEventListener('click', function () {
                mySearch.value = '';
                triggerSearch();
            });
        }

        mySearch.oninput = triggerSearch;
    }

    function insertMark(str, pos, len) {
        return str.slice(0, pos) + '<mark>' + str.slice(pos, pos + len) + '</mark>' + str.slice(pos + len);
    }

    function triggerSearch() {
        let val = mySearch.value.trim().toLowerCase();
        let tours = document.querySelectorAll('.tour-item');

        tours.forEach(function (tour_item) {
            let city = tour_item.querySelector('.city').innerText.toLowerCase();
            let nameHotel = tour_item.querySelector('.nameHotel').innerText.toLowerCase();

            if (city.search(val) === -1 && nameHotel.search(val) === -1) {
                tour_item.classList.add('hide');
            } else {
                tour_item.classList.remove('hide');
            }

            if (city.search(val) !== -1) {
                let str = tour_item.querySelector('.city').innerText;
                tour_item.querySelector('.city').innerHTML = insertMark(str, city.search(val), val.length);
            } else {
                tour_item.querySelector('.city').innerHTML = tour_item.querySelector('.city').innerText;
            }

            if (nameHotel.search(val) !== -1) {
                let str = tour_item.querySelector('.nameHotel').innerText;
                tour_item.querySelector('.nameHotel').innerHTML = insertMark(str, nameHotel.search(val), val.length);
            } else {
                tour_item.querySelector('.nameHotel').innerHTML = tour_item.querySelector('.nameHotel').innerText;
            }
        });
    }

});
