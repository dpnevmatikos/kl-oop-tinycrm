// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.js-btn-search').on('click', () => {
    let email = $('.js-email').val();
    let vatnumber = $('.js-vatnumber').val();

    $.ajax({
        url: 'https://localhost:5001/customer/SearchCustomers',
        type: 'GET',
        data: {
            email: email,
            vatNumber: vatnumber
        }
    }).done((customers) => {
        let $customersList = $('.js-customer-list');
        $customersList.html('');

        customers.forEach(element => {
            let listItem =
                `<tr>
                    <td>${element.vatNumber}</td>
                    <td>${element.email}</td>
                </tr>`;

            $customersList.append(listItem);
        });
    }).fail((xhr) => {
        alert(xhr.responseText);
    });
});

