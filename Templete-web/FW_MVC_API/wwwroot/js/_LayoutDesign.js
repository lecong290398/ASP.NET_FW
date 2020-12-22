
var options = [];
$('.dropdown-menu a').on('click', function (event) {
    var $target = $(event.currentTarget),
        val = $target.attr('data-value'),
        $inp = $target.find('input'),
        idx;
    if ((idx = options.indexOf(val)) > -1) {
        options.splice(idx, 1);
        setTimeout(function () { $inp.prop('checked', false) }, 0);
    } else {
        options.push(val);
        setTimeout(function () { $inp.prop('checked', true) }, 0);
    }

    $(event.target).blur();

    console.log(options);
    return false;
});

$('#folowNature').on('show.bs.collapse', function () {
    $('#icon-up-down-nature').addClass('fa-chevron-up');
    $('#icon-up-down-nature').removeClass('fa-chevron-down');
});

$('#folowNature').on('hide.bs.collapse', function () {
    $('#icon-up-down-nature').removeClass('fa-chevron-up');
    $('#icon-up-down-nature').addClass('fa-chevron-down');
});
//Get the button
var mybutton = document.getElementById("myBtnScrollTop");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}


$(document).ready(function () {
    $("#order-type").click(function () {
        let data = $('#icon-collapse-order-type').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-order-type").addClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type").removeClass('fa-chevron-circle-up');
            $("#content-order-type").removeClass('d-none');
        } else {
            $("#content-order-type").addClass('d-none');
            $("#icon-collapse-order-type").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type").addClass('fa-chevron-circle-up');
        }
    });

    $("#category-pro").click(function () {
        let data = $('#icon-collapse-category-pro').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-category-pro").addClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro").removeClass('fa-chevron-circle-up');
            $("#content-category-pro").removeClass('d-none');
        } else {
            $("#content-category-pro").addClass('d-none');
            $("#icon-collapse-category-pro").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro").addClass('fa-chevron-circle-up');
        }
    });

    $("#group-pro").click(function () {
        let data = $('#icon-collapse-group-pro').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-group-pro").addClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro").removeClass('fa-chevron-circle-up');
            $("#content-group-pro").removeClass('d-none');
        } else {
            $("#content-group-pro").addClass('d-none');
            $("#icon-collapse-group-pro").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro").addClass('fa-chevron-circle-up');
        }
    });

    $("#inventory-pro").click(function () {
        let data = $('#icon-collapse-inventory-pro').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-inventory-pro").addClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro").removeClass('fa-chevron-circle-up');
            $("#content-inventory-pro").removeClass('d-none');
        } else {
            $("#content-inventory-pro").addClass('d-none');
            $("#icon-collapse-inventory-pro").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro").addClass('fa-chevron-circle-up');
        }
    });
    // script handle colappse for sidebar responsive
    $("#order-type-res").click(function () {
        let data = $('#icon-collapse-order-type-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-order-type-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type-res").removeClass('fa-chevron-circle-up');
            $("#content-order-type-res").removeClass('d-none');
        } else {
            $("#content-order-type-res").addClass('d-none');
            $("#icon-collapse-order-type-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type-res").addClass('fa-chevron-circle-up');
        }
    });

    $("#category-pro-res").click(function () {
        let data = $('#icon-collapse-category-pro-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-category-pro-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro-res").removeClass('fa-chevron-circle-up');
            $("#content-category-pro-res").removeClass('d-none');
        } else {
            $("#content-category-pro-res").addClass('d-none');
            $("#icon-collapse-category-pro-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro-res").addClass('fa-chevron-circle-up');
        }
    });

    $("#group-pro-res").click(function () {
        let data = $('#icon-collapse-group-pro-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-group-pro-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro-res").removeClass('fa-chevron-circle-up');
            $("#content-group-pro-res").removeClass('d-none');
        } else {
            $("#content-group-pro-res").addClass('d-none');
            $("#icon-collapse-group-pro-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro-res").addClass('fa-chevron-circle-up');
        }
    });

    $("#inventory-pro-res").click(function () {
        let data = $('#icon-collapse-inventory-pro-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-inventory-pro-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro-res").removeClass('fa-chevron-circle-up');
            $("#content-inventory-pro-res").removeClass('d-none');
        } else {
            $("#content-inventory-pro-res").addClass('d-none');
            $("#icon-collapse-inventory-pro-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro-res").addClass('fa-chevron-circle-up');
        }
    });

});
/* Set the width of the side navigation to 250px and the left margin of the page content to 250px and add a black background color to body */
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("mySidenav").style.backgroundColor = "#fff";
    document.getElementById("main").style.marginLeft = "250px";
    document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.body.style.backgroundColor = "white";
}
