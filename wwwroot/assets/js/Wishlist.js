const wishlistBtns = document.querySelectorAll('.wishlist-button');
wishlistBtns.forEach((btn) => {
  btn.addEventListener('click', addToWishlist);
});

document.addEventListener('DOMContentLoaded', () => {
  ShowTotalCount();
  showWishlist();
});

function createClearAndShopButtons() {
  const wishlistContainer = document.getElementById('wishlist-container');

  const existingWishlist = getCookie('wishlist');

  const buttonsDiv = document.createElement('div');
  buttonsDiv.style.textAlign = 'center';
  buttonsDiv.style.marginTop = '50px';

  const clearWishlistBtn = document.createElement('button');
  clearWishlistBtn.textContent = 'Clear Wishlist';
  clearWishlistBtn.addEventListener('click', clearWishlist);
  clearWishlistBtn.className = 'clear-wishlist-button col-12';
  clearWishlistBtn.style.marginRight = '10px';
  clearWishlistBtn.style.background =
    'linear-gradient(90deg, rgba(19, 111, 250, 0.418) 0%, rgb(42, 26, 184) 35%, rgb(111, 0, 119) 100%)';
  clearWishlistBtn.style.width = '200px';
  clearWishlistBtn.style.height = '50px';
  clearWishlistBtn.style.border = '1px solid grey';
  clearWishlistBtn.style.borderBottomLeftRadius = '20px';
  clearWishlistBtn.style.borderTopRightRadius = '20px';
  clearWishlistBtn.style.color = 'white';

  const shopBtn = document.createElement('button');
  shopBtn.textContent = 'Continue Shopping';
  shopBtn.addEventListener('click', goToShop);
  shopBtn.id = 'shop-button';
  shopBtn.style.background =
    'linear-gradient(90deg, rgba(19, 111, 250, 0.418) 0%, rgb(42, 26, 184) 35%, rgb(111, 0, 119) 100%)';
  shopBtn.style.width = '200px';
  shopBtn.style.height = '50px';
  shopBtn.style.border = '1px solid grey';
  shopBtn.style.borderBottomLeftRadius = '20px';
  shopBtn.style.borderTopRightRadius = '20px';
  shopBtn.style.color = 'white';

  if (existingWishlist && JSON.parse(existingWishlist).length > 0) {
    buttonsDiv.appendChild(clearWishlistBtn);
    buttonsDiv.appendChild(shopBtn);
  }

  wishlistContainer.appendChild(buttonsDiv);
}

function clearWishlist() {
  deleteCookie('wishlist');
  ShowTotalCount();
  showWishlist();
  const clearWishlistBtn = document.getElementById('clear-wishlist-button');
  const shopBtn = document.getElementById('shop-button');
  if (clearWishlistBtn) {
    clearWishlistBtn.remove();
  }
  if (shopBtn) {
    shopBtn.remove();
  }
}

function goToShop() {
  window.location.href = './shop.html';
}

function ShowTotalCount() {
  const existingWishlist = getCookie('wishlist');
  const wishlistCountElements = document.getElementsByClassName('wishlist-count');

  if (existingWishlist && wishlistCountElements.length > 0) {
    const wishlist = JSON.parse(existingWishlist);
    for (let i = 0; i < wishlistCountElements.length; i++) {
      const wishlistCountElement = wishlistCountElements[i];
      wishlistCountElement.innerText = wishlist.length.toString();
    }
  }
}

function showWishlist() {
  const existingWishlist = getCookie('wishlist');
  const wishlistContainer = document.getElementById('wishlist-container');

  if (existingWishlist && wishlistContainer) {
    const wishlist = JSON.parse(existingWishlist);
    wishlistContainer.innerHTML = '';

    if (wishlist.length > 0) {
      for (let i = 0; i < wishlist.length; i++) {
        const product = wishlist[i];
        const productDiv = document.createElement('div');
        productDiv.classList.add('wishlist-product', 'col-lg-3', 'col-md-', 'col-12');

        const productImage = document.createElement('img');
        productImage.src = product.image;
        productImage.alt = product.name;

        const productName = document.createElement('h4');
        productName.textContent = product.name;

        const productPrice = document.createElement('span');
        productPrice.textContent = product.price + ' USD';

        const deleteButton = document.createElement('button');
        deleteButton.textContent = 'X';
        deleteButton.addEventListener('click', () => {
          removeFromWishlist(product.id);
          showWishlist();
        });

        productDiv.appendChild(productImage);
        productDiv.appendChild(productName);
        productDiv.appendChild(productPrice);
        productDiv.appendChild(deleteButton);

        wishlistContainer.appendChild(productDiv);
      }
      createClearAndShopButtons();
    } else {
      wishlistContainer.textContent = 'Your wishlist is currently empty!';
      wishlistContainer.style.fontWeight = 'bold';
      wishlistContainer.style.color = 'grey';
      wishlistContainer.style.display = 'flex';
      wishlistContainer.style.justifyContent = 'center';
    }
  } else if (wishlistContainer) {
    wishlistContainer.textContent = 'Your wishlist is currently empty!';

    wishlistContainer.style.fontWeight = 'bold';
    wishlistContainer.style.color = 'grey';
    wishlistContainer.style.display = 'flex';
    wishlistContainer.style.justifyContent = 'center';
  }
}

function removeFromWishlist(productId) {
  const existingWishlist = getCookie('wishlist');
  if (existingWishlist) {
    const wishlist = JSON.parse(existingWishlist);
    const updatedWishlist = wishlist.filter((product) => product.id !== productId);
    setCookie('wishlist', JSON.stringify(updatedWishlist));
  }
}

function addToWishlist(event) {
  event.preventDefault();
  const productElement = event.target.closest('.item');
  if (!productElement) {
    return; // Eğer item sınıfı bulunmayan bir elemente tıklanırsa işlemi durdur
  }

  const productId = productElement.dataset.id;
  const productName = productElement.querySelector('h4').textContent;
  const productPrice = productElement.querySelector('span').textContent;
  const productImage = productElement.querySelector('img').src;

  const product = {
    id: productId,
    name: productName,
    price: productPrice,
    image: productImage
  };

  const existingWishlist = getCookie('wishlist');
  if (!existingWishlist) {
    setCookie('wishlist', JSON.stringify([product]));
  } else {
    const wishlist = JSON.parse(existingWishlist);
    const productIndex = wishlist.findIndex((item) => item.id === productId);
    if (productIndex === -1) {
      wishlist.push(product);
      setCookie('wishlist', JSON.stringify(wishlist));
    } else {
      wishlist.splice(productIndex, 1);
      setCookie('wishlist', JSON.stringify(wishlist));
    }
  }

  ShowTotalCount();
  showWishlist();
  createClearAndShopButtons();
}



function setCookie(name, value, days = 7) {
  const expires = new Date(Date.now() + days * 24 * 60 * 60 * 1000).toUTCString();
  document.cookie = `${name}=${encodeURIComponent(value)}; expires=${expires}; path=/`;
}

function getCookie(name) {
  const cookieValue = document.cookie.match(`(^|;)\\s*${name}\\s*=\\s*([^;]+)`);
  return cookieValue ? decodeURIComponent(cookieValue.pop()) : '';
}

function deleteCookie(name) {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
}