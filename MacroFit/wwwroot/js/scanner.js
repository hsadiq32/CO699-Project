const html5QrCode = new Html5Qrcode("reader");
let qrboxFunction = function (viewfinderWidth, viewfinderHeight) {
    let minEdgePercentage;
    let viewportWidth = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0);

    // Adjust minimum edge percentage based on viewport width
    if (viewportWidth < 600) { // Mobile
        minEdgePercentage = 0.4; // 60%
    } else { // Desktop
        minEdgePercentage = 0.2; // 40%
    }

    let viewportHeight = Math.max(document.documentElement.clientHeight || 0, window.innerHeight || 0);
    let barcodeAspectRatio = 1.5; // Example aspect ratio for a food barcode
    let qrboxWidth = Math.floor(viewportWidth * minEdgePercentage);
    let qrboxHeight = Math.floor(qrboxWidth / barcodeAspectRatio);
    return {
        width: qrboxWidth,
        height: qrboxHeight
    };
};
const config = { fps: 30, qrbox: qrboxFunction };
let scannedBarcodes = [];
const urlParams = new URLSearchParams(window.location.search);

function redirectLinkMaker(foodID, barcode) {
    // Clear previous URL parameters
    urlParams.delete('FoodId');
    urlParams.delete('Barcode');

    // Get URL parameters
    if (foodID) {
        urlParams.append('FoodId', foodID);
    }
    else {
        urlParams.append('Barcode', barcode);
    }
    // Construct new URL with added parameters
    return '/Log/Create?' + urlParams.toString();
}

function isFoodBarcode(barcode){
    return /^[a-zA-Z0-9]+$/.test(barcode);
}


function displayProductDetails(productDetails) {
    // Remove the "No food items scanned yet" prompt if it exists
    const noItemsPrompt = document.getElementById('no-items-prompt');
    if (noItemsPrompt) {
        noItemsPrompt.remove();
    }

    // Create a card for the product
    const productCard = document.createElement('div');
    productCard.className = "card";
    productCard.style.marginTop = "20px";

    // Create a card body
    const cardBody = document.createElement('div');
    cardBody.className = "card-body";
    productCard.appendChild(cardBody);

    if (productDetails.foodResponseDetails) {
        // Add the product name
        const productName = document.createElement('h4');
        productName.className = "card-title";
        productName.textContent = productDetails.foodResponseDetails.name;
        cardBody.appendChild(productName);

        // Add the product barcode
        const productBarcode = document.createElement('h6');
        productBarcode.className = "text-muted card-subtitle mb-2";
        productBarcode.textContent = productDetails.barcode;
        cardBody.appendChild(productBarcode);

        // Add the product unit
        const productUnit = document.createElement('h6');
        productUnit.className = "text-muted card-subtitle mb-2";
        productUnit.style.fontStyle = "italic";
        productUnit.style.padding = "2px";
        productUnit.textContent = "per 100g";
        cardBody.appendChild(productUnit);

        // Create a div to hold macro values
        const macrosDiv = document.createElement('div');
        macrosDiv.className = "d-flex";
        macrosDiv.style.marginRight = "-5px";
        macrosDiv.style.marginLeft = "-5px";
        cardBody.appendChild(macrosDiv);

        // Add macro information
        const macroInfo = [
            { value: productDetails.foodResponseDetails.calories + " kcal", name: "Calories" },
            { value: productDetails.foodResponseDetails.carbohydrates + "g", name: "Carbs" },
            { value: productDetails.foodResponseDetails.fat + "g", name: "Fat" },
            { value: productDetails.foodResponseDetails.protein + "g", name: "Protein" },
        ];

        macroInfo.forEach(macro => {
            const macroContainer = document.createElement('div');
            macroContainer.className = "d-flex align-items-start flex-column macro-container";
            macroContainer.style.color = "var(--text-primary-color)";
            macroContainer.style.border = "2px solid var(--text-primary-color)";
            macrosDiv.appendChild(macroContainer);

            const macroValue = document.createElement('p');
            macroValue.className = "text-center macro-value";
            macroValue.textContent = macro.value;
            macroContainer.appendChild(macroValue);

            const macroName = document.createElement('p');
            macroName.className = "text-center macro-name";
            macroName.textContent = macro.name;
            macroContainer.appendChild(macroName);
        });

        // Add the log button
        const buttonWrapper = document.createElement('a');
        buttonWrapper.href = redirectLinkMaker(productDetails.foodResponseDetails.id, productDetails.barcode);
        cardBody.appendChild(buttonWrapper);

        const button = document.createElement('button');
        button.className = "btn btn-success dynamic-btn themed-btn";
        button.type = "button";
        button.style.width = "50px";
        button.style.height = "50px";
        button.style.position = "absolute";
        button.style.top = "15px";
        button.style.right = "15px";
        buttonWrapper.appendChild(button);

        const svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
        svg.setAttribute("xmlns", "http://www.w3.org/2000/svg");
        svg.setAttribute("viewBox", "-32 0 512 512");
        svg.setAttribute("width", "1em");
        svg.setAttribute("height", "1em");
        svg.setAttribute("fill", "currentColor");
        button.appendChild(svg);

        const path = document.createElementNS("http://www.w3.org/2000/svg", "path");
        path.setAttribute("d", "M432 256c0 17.69-14.33 32.01-32 32.01H256v144c0 17.69-14.33 31.99-32 31.99s-32-14.3-32-31.99v-144H48c-17.67 0-32-14.32-32-32.01s14.33-31.99 32-31.99H192v-144c0-17.69 14.33-32.01 32-32.01s32 14.32 32 32.01v144h144C417.7 224 432 238.3 432 256z");
        svg.appendChild(path);
    }
    else {
        // Add the product name
        const productName = document.createElement('h4');
        productName.className = "card-title";
        productName.textContent = "Unknown Product";
        cardBody.appendChild(productName);

        // Add the product barcode
        const productBarcode = document.createElement('h6');
        productBarcode.className = "text-muted card-subtitle mb-2";
        productBarcode.textContent = productDetails.barcode;
        cardBody.appendChild(productBarcode);

        // Add the log button
        const buttonWrapper = document.createElement('a');
        buttonWrapper.href = redirectLinkMaker(null, productDetails.barcode);
        cardBody.appendChild(buttonWrapper);

        const button = document.createElement('button');
        button.className = "btn btn-success dynamic-btn themed-btn";
        button.type = "button";
        button.style.width = "50px";
        button.style.height = "50px";
        button.style.position = "absolute";
        button.style.top = "15px";
        button.style.right = "15px";
        buttonWrapper.appendChild(button);

        const svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
        svg.setAttribute("xmlns", "http://www.w3.org/2000/svg");
        svg.setAttribute("viewBox", "-32 0 512 512");
        svg.setAttribute("width", "1em");
        svg.setAttribute("height", "1em");
        svg.setAttribute("fill", "currentColor");
        button.appendChild(svg);

        const path = document.createElementNS("http://www.w3.org/2000/svg", "path");
        path.setAttribute("d", "M432 256c0 17.69-14.33 32.01-32 32.01H256v144c0 17.69-14.33 31.99-32 31.99s-32-14.3-32-31.99v-144H48c-17.67 0-32-14.32-32-32.01s14.33-31.99 32-31.99H192v-144c0-17.69 14.33-32.01 32-32.01s32 14.32 32 32.01v144h144C417.7 224 432 238.3 432 256z");
        svg.appendChild(path);
    }

    // Add the product card to the products container
    const productsContainer = document.getElementById('products');
    productsContainer.insertBefore(productCard, productsContainer.firstChild);
}

function displayNoItemsPrompt() {
    const productsContainer = document.getElementById('products');
    const noItemsPrompt = document.createElement('p');
    noItemsPrompt.id = 'no-items-prompt';
    noItemsPrompt.textContent = 'No food items scanned yet.';
    productsContainer.appendChild(noItemsPrompt);
}

const qrCodeSuccessCallback = async (decodedText, decodedResult) => {
    // Check if the barcode has already been scanned
    if (scannedBarcodes.includes(decodedText)) {
        return;
    }
    // Add the barcode to the scanned barcodes
    scannedBarcodes.push(decodedText);
    if (isFoodBarcode(decodedText)) {
        try {
            // Call the API to get the product details
            const response = await fetch(`http://localhost:44391/api/scan/${decodedText}`);
            const data = await response.json();

            // Check if the barcode was found
            if (data.status === 200 || data.status === 400) {
                // Display the product details
                displayProductDetails(data);
            }
        } catch (error) {
            console.error(error);
        }
    }

};

// Start the barcode scanner
const startBarcodeScanner = () => {
    html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);
};

// Call the displayNoItemsPrompt function to display the prompt when no food items have been scanned
displayNoItemsPrompt();

let isScannerRunning = false;

const cameraToggle = document.getElementById('camera-toggle');
const cameraContainer = document.querySelector('.camera-container');
const cameraContainerHeight = '40vh';

// Set the initial state of the camera container and button
cameraContainer.style.height = '0vh';
cameraToggle.classList.remove('btn-success');
cameraToggle.classList.add('btn-secondary');

// Add a click event listener to the camera toggle button
cameraToggle.addEventListener('click', () => {
    // Toggle the height of the camera container
    cameraContainer.style.height = cameraContainer.style.height === '0vh' ? cameraContainerHeight : '0vh';

    // Toggle the button color and selected state
    cameraToggle.classList.toggle('btn-secondary');
    cameraToggle.classList.toggle('btn-success');

    // Toggle the scanner state
    if (!isScannerRunning) {
        startBarcodeScanner();
        isScannerRunning = true;
    } else {nutella
        html5QrCode.stop();
        isScannerRunning = false;
    }
});


// Display search results
function displaySearchResults(products) {
    let productDetailsList = document.querySelector('#products');
    productDetailsList.innerHTML = '';

    if (products.length > 0) {
        products.forEach(product => {
            // Convert the search item JSON to scanner JSON
            let scannerJson = {
                status: 200,
                message: "Barcode Found",
                barcode: product.barcode,
                foodResponseDetails: {
                    id: product.id,
                    name: product.name,
                    calories: product.calories,
                    carbohydrates: product.carbohydrates,
                    protein: product.protein,
                    fat: product.fat
                }
            };
            // Display the product details
            displayProductDetails(scannerJson);
        });
    } else {
        productDetailsList.innerHTML = '<div class="alert alert-warning" role="alert">No food products found.</div>';
    }
}



$(document).ready(function () {
    var searchInput = $('#search-input');
    var searchBtn = $('#search-btn');
    var searchResults = $('#products');
    var apiUrl = 'http://localhost:44391/api/search/list/';
    var lastSearchTerm = '';

    var minimumSearchLength = 2;
    var foodNameRegex = /^[a-zA-Z0-9\s\-_()&',./]+$/;

    searchInput.keyup(function (event) {
        if (event.keyCode === 13) {
            searchBtn.click();
        }
    });

    searchBtn.on('click', function () {
        var searchTerm = searchInput.val().trim();
        if (searchTerm.length > minimumSearchLength && searchTerm !== lastSearchTerm && foodNameRegex.test(searchTerm)) {
            lastSearchTerm = searchTerm;
            searchFoodItems(searchTerm);
        } else if (searchTerm.length <= minimumSearchLength) {
            searchResults.html('<div class="alert alert-danger" role="alert">Please enter at least ' + (minimumSearchLength + 1) + ' characters.</div>');
        } else if (!foodNameRegex.test(searchTerm)) {
            searchResults.html('<div class="alert alert-danger" role="alert">Invalid search term. Only alphanumeric characters and limited special characters are allowed.</div>');
        }
    });

    function searchFoodItems(searchTerm) {
        searchResults.html('<div class="d-flex justify-content-center my-3"><div class="spinner-border text-primary" role="status"><span class="visually-hidden">Loading...</span></div></div>');

        $.ajax({
            url: apiUrl + searchTerm,
            type: 'GET',
            success: function (data) {
                displaySearchResults(data);
            },
            error: function (xhr) {
                if (xhr.status === 404) {
                    // No food items found
                    searchResults.html('<div class="alert alert-warning" role="alert">No food products found.</div>');
                } else if (xhr.status >= 400 && xhr.status < 500) {
                    // Error retrieving search results: Bad connection
                    searchResults.html('<div class="alert alert-danger" role="alert">Error retrieving search results: Bad connection.</div>');
                } else {
                    // Error retrieving search results
                    searchResults.html('<div class="alert alert-danger" role="alert">Error retrieving search results.</div>');
                }
            }
        });
    }
});