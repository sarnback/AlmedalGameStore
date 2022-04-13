let cart = [];

let userPath = window.location.pathname.split('/');

function AddProductToCartPerProductId(id) {
    let quantity = document.getElementById('item-count').value;

    let cartItem = { "productId": id, "quantity": quantity }
    cart.push(cartItem);

    let title = document.getElementById("itemTitle").innerHTML.toString();

    document.getElementById("cartOutput").innerHTML += "Artikeltitel: " + title + "<br>Antal: " + quantity + "<br><br>";
    /*alert("Added product with ID " + id + " to cart. Quantity: " + quantity + title);*/
}