function setCookie(name, value, days) {
    let expires = "";
    if (days) {
        let date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    let nameEQ = name + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function sendBasketToServer(product) {
    fetch("/Order/Create", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(product),
    })
        .then((response) => response.json())
        .then((data) => {
            // Ýstek baþarýlý olduðunda yapýlacak iþlemler
            console.log("Ýstek baþarýlý", data);
        })
        .catch((error) => {
            // Hata durumunda yapýlacak iþlemler
            console.error("Ýstek hatasý", error);
        });
}

let basketHTML = document.querySelector(".basket-menu .producct");

document.addEventListener("DOMContentLoaded", function () {
    let basketStr = getCookie("basket");
    let basket = JSON.parse(basketStr);

    if (!basket || !basket.length) {
        setCookie("basket", JSON.stringify([]));
    } else {
        ShowProductCount(basket);

        basket.forEach((product) => {
            AddToBasket(product);
        });
        ShowTotalPrice(basket);
    }
});

let buttons = document.querySelectorAll(".buy");

buttons.forEach((button) => {
    button.addEventListener("click", function () {
        let basket = JSON.parse(getCookie("basket"));
        if (!basket) {
            setCookie("basket", JSON.stringify([]));
            basket = JSON.parse(getCookie("basket"));
        }
        let product = GetProductDatas(this);

        let existedProduct = basket.find((pro) => {
            return pro.id == product.id;
        });
        if (!existedProduct) {
            basket.push(product);
        } else {
            existedProduct.count++;
        }
        ShowProductCount(basket);

        AddToBasket(product);

        let basketStr = JSON.stringify(basket);
        setCookie("basket", basketStr);
        sendBasketToServer(product);
    });
});

function ShowProductCount(basket) {
    let basketCount = document.querySelectorAll(".basket-count");
    basketCount.forEach((count) => {
        count.innerText = basket.reduce((total, product) => {
            return (total += product.count);
        }, 0);
    });
}

function ShowTotalPrice(basket) {
    let total = document.querySelectorAll(".price");
    total.forEach((tt) => {
        tt.innerText = basket.reduce((total, product) => {
            return (total += parseInt(product.price) * product.count);
        }, 0);
    });
}

function GetProductDatas(product) {
    let parent = product.parentElement.parentElement;
    let id = parent.getAttribute("data-id");
    let title = parent.querySelector(".title").innerText;
    let price = parent.querySelector(".prices").innerText;

    let src = parent.querySelector("img").src;
    let result = { id, title, price, src, count: 1 };
    return result;
}

function AddToBasket(product) {
    let id = product.id;
    let basketItem = basketHTML.querySelector(`[data-id="${id}"]`);

    if (basketItem) {
        let count = basketItem.getAttribute("data-count");
        count++;
        basketItem.setAttribute("data-count", count);
        basketItem.querySelector(".count").innerText = count;
    } else {
        let div = document.createElement("div");
        let image = document.createElement("img");
        let contentDiv = document.createElement("div");
        let title = document.createElement("h5");
        let price = document.createElement("span");
        let count = document.createElement("p");
        let closeBtn = document.createElement("i");

        closeBtn.className = "btn btn-danger";
        closeBtn.className = "fa-regular fa-trash-can";
        closeBtn.style.position = "absolute";
        closeBtn.style.right = "10px";
        div.className = "basket-item";
        div.style.position = "relative";
        div.setAttribute("data-id", product.id);
        div.setAttribute("data-count", 1);
        div.style.width = "398px";
        div.style.marginBottom = "10px";
        div.style.alignItems = 'center';
        div.style.padding = '20px 0';
        div.style.border = '1px solid grey';
        div.style.display = 'flex'

        image.src = product.src;
        image.style.width = "100px";
        title.innerText = product.title;
        price.innerText = product.price;
        count.innerText = 1;
        count.className = "count";

        contentDiv.append(title, price, count);
        div.append(image, contentDiv, closeBtn);
        basketHTML.append(div);

        closeBtn.addEventListener("click", function () {
            let basket = JSON.parse(getCookie("basket"));
            if (!basket) {
                setCookie("basket", JSON.stringify([]));
                basket = JSON.parse(getCookie("basket"));
            }
            let id = this.parentElement.getAttribute("data-id");
            let index = basket.findIndex((element) => {
                return element.id == id;
            });
            delete basket[index];
            basket = basket.filter(Boolean);
            ShowProductCount(basket);

            let basketStr = JSON.stringify(basket);
            setCookie("basket", basketStr);
            this.parentElement.remove();
            ShowTotalPrice(basket);
        });
    }
} 