let cart = [];

let userPath = window.location.pathname.split('/');

// Pushes reference to cart array per product ID to resolve in cart view
function AddProductToCartPerProductId(id) {
    let quantity = document.getElementById('item-count').value;

    let cartItem = { "productId": id, "quantity": quantity }
    cart.push(cartItem);

    alert("Added product with ID " + id + " to cart. Quantity: " + quantity);
}

// Boots resolve function (mock-up)
//function ResolveCartItems() {
//    for (let i = 0; i < cart.length; ++i) {
//        ResolveProductPerProductId(cart[i].productId);
//    }
//    //... And so on and so forth
//}

// Resolves product object per id. Intended to be called from
// within the cart view (mock-up)
function ResolveProductPerProductId(requestedId) {
    let object = _unitOfWork.Product.GetFirstOrDefault(x => x.requestedId === Id);
    alert(JSON.stringify(object));

    // Fulhaxx GET JSON
    //var targetUrl = <%= (new JavaScriptSerializer()).Serialize("http://url/") %>;
}


//C# method
//public IActionResult Details(int id)
//{
//    Cart cartObj = new ()
//    {
//        Count = 1,
//            //för att kunna lägga till en produkt i kundkorg
//            Product = _unitOfWork.Product.GetFirstOrDefault
//                (u => u.Id == id, includeProperties: "Genre")
//    };
//    //returna cartObjektet till vyn
//    return View(cartObj);
//}