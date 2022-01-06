


var shoppingCart = (function () {
    // =============================
    // Private methods and propeties
    // =============================
    cart = [];

    // Constructor
    function Item(id, name, price, count) {
        this.name = name;
        this.price = price;
        this.count = count;
        this.id = id;
       
    }

    // Save cart
    function saveCart() {
        sessionStorage.setItem('shoppingCart', JSON.stringify(cart));
    }

    // Load cart
    function loadCart() {
        cart = JSON.parse(sessionStorage.getItem('shoppingCart'));
    }
    if (sessionStorage.getItem("shoppingCart") != null) {
        loadCart();
    }


    // =============================
    // Public methods and propeties
    // =============================
    var obj = {};

    // Add to cart
    obj.addItemToCart = function (id, name, price, count) {
        name = name.replace(/ .*/, '')
       
        for (var item in cart) {
            name.replace(/ .*/, '')
            if (cart[item].name === name) {
                cart[item].count++;
                saveCart();
                return;
            }
        }
        var item = new Item(id, name, price, count);
        cart.push(item);
        saveCart();
    }
    // Set count from item
    obj.setCountForItem = function (name, count) {
        for (var i in cart) {
            if (cart[i].name === name) {
                cart[i].count = count;
                break;
            }
        }
    };
    // Remove item from cart
    obj.removeItemFromCart = function (name) {
        for (var item in cart) {
            if (cart[item].name === name) {
                cart[item].count--;
                if (cart[item].count === 0) {
                    cart.splice(item, 1);
                }
                break;
            }
        }
        saveCart();
    }

    // Remove all items from cart
    obj.removeItemFromCartAll = function (name) {
        for (var item in cart) {
            if (cart[item].name === name) {
                cart.splice(item, 1);
                break;
            }
        }
        saveCart();
    }

    // Clear cart
    obj.clearCart = function () {
        cart = [];
        saveCart();
    }

    // Count cart 
    obj.totalCount = function () {
        var totalCount = 0;
        for (var item in cart) {
            totalCount += cart[item].count;
        }
        return totalCount;
    }

    // Total cart
    obj.totalCart = function () {
        var totalCart = 0;
        for (var item in cart) {
           
            totalCart += cart[item].price * cart[item].count;
        }
        return Number(totalCart.toFixed(2));
    }

    // List cart
    obj.listCart = function () {
      
        var cartCopy = [];
        for (i in cart) {
            item = cart[i];
            itemCopy = {};
            for (p in item) {
                itemCopy[p] = item[p];

            }
            
            itemCopy.total = Number(item.price * item.count).toFixed(2);
            cartCopy.push(itemCopy)
        }
        return cartCopy;
    }

    // cart : Array
    // Item : Object/Class
    // addItemToCart : Function
    // removeItemFromCart : Function
    // removeItemFromCartAll : Function
    // clearCart : Function
    // countCart : Function
    // totalCart : Function
    // listCart : Function
    // saveCart : Function
    // loadCart : Function
    return obj;
})();


// *****************************************
// Triggers / Events
// ***************************************** 
// Add item
$('.add-to-cart').click(function (event) {
    event.preventDefault();
  
    var name = $(this).data('name');
    var price = Number($(this).data('price'));
    var id = $(this).data('id');
    shoppingCart.addItemToCart(id, name, price, 1);
    displayCart();
});

// Clear items
$('.clear-cart').click(function () {
    shoppingCart.clearCart();
    displayCart();
});


function displayCart() {
    var cartArray = shoppingCart.listCart();
    var output = "";
    for (var i in cartArray) {
       
        output += "<tr>"
            + "<td>" + cartArray[i].name + "</td>"
            + "<td>" + "R " + cartArray[i].price + "</td>"
            + "<td><input type='text' pattern='[0 - 9.,] +'  class='item-count form-control  ' data-name='" + cartArray[i].name + "' value='" + cartArray[i].count + "'></td>"
            //+ "<td><div class='input-group'><button type='hidden' class='minus-item input-group-addon btn btn-primary' data-name=" + cartArray[i].name + ">-</button>"
            
            //+ "<button type='hidden' class='plus-item btn btn-primary  input-group-addon '  data-name=" + cartArray[i].name + ">+</button></div></td>"
            
            + " = "
            + "<td>" + "R " + cartArray[i].total + "</td>"
            + "<td><button class='delete-item btn btn-danger' data-name=" + cartArray[i].name + ">X</button></td>"
            + "</tr>"
;
    }
    $('.show-cart').html(output);
    $('.total-cart').html("R " + shoppingCart.totalCart());
    $('.total-count').html(shoppingCart.totalCount());
}

// Delete item button

$('.show-cart').on("click", ".delete-item", function (event) {
    var name = $(this).data('name')
    shoppingCart.removeItemFromCartAll(name);
    displayCart();
})


// -1
$('.show-cart').on("click", ".minus-item", function (event) {
    var name = $(this).data('name')
    shoppingCart.removeItemFromCart(name);
    displayCart();
})
// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    var name = $(this).data('name')
    shoppingCart.addItemToCart(name);
    displayCart();
})

// Item count input
$('.show-cart').on("change", ".item-count", function (event) {
    
    var name = $(this).data('name');
    var count = Number($(this).val());
    shoppingCart.setCountForItem(name, count);
    displayCart();
});

displayCart();


var elements = document.querySelectorAll('.ripple-effect');

for (var i = 0; i < elements.length; i++) {
    elements[i].addEventListener("click", function (e) {

        e.preventDefault;
        var elm = document.querySelector('.wrapper');

        if (elm.className !== 'marked')
            elm.classList.add('marked');


        elm.classList.remove("active");
        void elm.offsetWidth;
        elm.classList.add("active");
    });
}

/* Slideshow JavaScript */
var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length };
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].classList.remove("active");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].classList.add("active");
}

function placeOrder() {

    //$.post("/Product/PlaceOrder", { Name: "Bulb", Price: 200, Quantity: 4 }, function (data) {
    //    $(".show-msg").html("<h1>"+data.Message+"!</h1>");    
    //});

    var items = [];

    var cartArray = shoppingCart.listCart();
    
    for (var i in cartArray) {

        

        items.push({
            ProductId : cartArray[i].id,
            Name: cartArray[i].name,
            Price: cartArray[i].price,
            Quantity: cartArray[i].count
        });
    }

    $.ajax({
        type: "GET",
        url: "Product/PlaceOrder",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { model: JSON.stringify(items) }, //JSON.stringify(usersRoles),
        success: function (data) {

            //data = JSON.parse(data);
            console.log(data);
            $("#cart").removeClass("fade").hide();
            $('.modal-backdrop').remove();

            $(".show-msg").html("<h1>" + data.Message + "!</h1>");

            $("#msg").removeClass("fade").modal("show").addClass("fade");

            shoppingCart.clearCart();
            displayCart();
        },
        error: function (errMsg) {    

            //const obj = JSON.parse(errMsg);
            console.log(errMsg);
            $("#cart").removeClass("fade").hide();
            $('.modal-backdrop').remove();

            $(".show-msg").html("<h1>" + errMsg.Message + "!</h1>");

            $("#msg").removeClass("fade").modal("show").addClass("fade");

            //shoppingCart.clearCart();
            //displayCart();
        }
    });

   
    //$("#cart").removeClass("fade").hide();
    //$('.modal-backdrop').remove();
    
    //$(".show-msg").html("<h1>Successful!</h1>");    
    //$("#msg").removeClass("fade").modal("show").addClass("fade");

    //shoppingCart.clearCart();
    //displayCart();
    
    
}
/* Slideshow JavaScript */

