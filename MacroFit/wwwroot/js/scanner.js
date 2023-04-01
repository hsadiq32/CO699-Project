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
    // Create a card for the product
    const productCard = document.createElement('div');
    productCard.classList.add('card', 'mb-3');

    // Create a card body
    const cardBody = document.createElement('div');
    cardBody.classList.add('card-body');
    productCard.appendChild(cardBody);

    if (productDetails.foodResponseDetails) {
        // Add the product name
        const productName = document.createElement('h5');
        productName.classList.add('card-title');
        productName.textContent = productDetails.foodResponseDetails.name;
        cardBody.appendChild(productName);


        // Add the product nutrients
        const productNutrients = document.createElement('p');
        productNutrients.classList.add('card-text');
        productNutrients.textContent = `Carbohydrates: ${productDetails.foodResponseDetails.carbohydrates} g, Protein: ${productDetails.foodResponseDetails.protein} g, Fat: ${productDetails.foodResponseDetails.fat} g`;
        cardBody.appendChild(productNutrients);

        const button = document.createElement('a');
        button.classList.add('btn');
        button.classList.add('btn-primary');
        button.textContent = "Log";
        button.href = redirectLinkMaker(productDetails.foodResponseDetails.id, productDetails.barcode);
        cardBody.appendChild(button);
    }
    else {
        // Add the product name
        const productName = document.createElement('h5');
        productName.classList.add('card-title');
        productName.textContent = productDetails.barcode;
        cardBody.appendChild(productName);

        const button = document.createElement('a');
        button.classList.add('btn');
        button.classList.add('btn-primary');
        button.textContent = "Log";
        button.href = redirectLinkMaker(null, productDetails.barcode);
        cardBody.appendChild(button);
    }

    
    
    // Add the product card to the products container
    const productsContainer = document.getElementById('products');
    productsContainer.insertBefore(productCard, productsContainer.firstChild);
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

// Call the startBarcodeScanner function to start the scanning process
startBarcodeScanner();