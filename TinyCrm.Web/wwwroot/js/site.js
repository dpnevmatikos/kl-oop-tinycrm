﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.js-btn-search').on('click', () => {
    let email = $('.js-email').val();
    let vatnumber = $('.js-vatnumber').val();

    $.ajax({
        url: '/customer/SearchCustomers',
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

let contacts = [];

$('.js-add-contact').on('click', function () {
    let $firstname = $('.js-contact-firstname');
    let $email = $('.js-contact-email');

    let firstname = $firstname.val();
    let email = $email.val();

    if (email.length === 0 || firstname.length === 0) {
        return;
    }

    contacts.push({
        firstname: firstname,
        email: email
    });

    $firstname.val('');
    $email.val('');
    console.log(contacts);
});

$('.js-submit-customer').on('click', () => {

    $('.js-submit-customer').attr('disabled', true);

    let email = $('.js-email').val();
    let vatnumber = $('.js-vatnumber').val();

    let data = JSON.stringify({
        email: email,
        vatNumber: vatnumber,
        contacts: contacts
    });

    $.ajax({
        url: '/customer/CreateCustomer',
        type: 'POST',
        contentType: 'application/json',
        data: data
    }).done((customer) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-customer-success');
        $alertArea.html(`Successfully added customer with id ${customer.id}`);
        $alertArea.show();

        $('form.js-create-customer').hide();
    }).fail((xhr) => {
        $('.alert').hide();

        let $alertArea = $('.js-create-customer-alert');
        $alertArea.html(xhr.responseText);
        $alertArea.fadeIn();

        setTimeout(function () {
            $('.js-submit-customer').attr('disabled', false);
        }, 300);
    });
});
