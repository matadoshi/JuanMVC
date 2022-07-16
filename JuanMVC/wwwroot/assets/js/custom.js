$(document).ready(function () {
    $(document).on('click', '.addbasket', function (e) {
        e.preventDefault();
        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => {
                $('.header-cart').html(data)
            });
    })
    $(document).on('click', '.deletebasket', function (e) {
        e.preventDefault();

        fetch($(this).attr('href'))
            .then(res => res.text())
            .then(data => { $('.header-cart').html(data) });
    })
}