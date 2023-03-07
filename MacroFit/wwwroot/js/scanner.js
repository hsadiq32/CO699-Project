const html5QrCode = new Html5Qrcode("reader");
let scannedBarcodes = [];

let qrboxFunction = function (viewfinderWidth, viewfinderHeight) {
    let minEdgePercentage = 0.5; // 70%
    let minEdgeSize = Math.min(viewfinderWidth, viewfinderHeight);
    let qrboxSize = Math.floor(minEdgeSize * minEdgePercentage);
    return {
        width: qrboxSize,
        height: qrboxSize
    };
};

const displayProductDetails = (productDetails) => {
    // Create a card for the product
    const productCard = document.createElement('div');
    productCard.classList.add('card', 'mb-3');

    // Create a card body
    const cardBody = document.createElement('div');
    cardBody.classList.add('card-body');
    productCard.appendChild(cardBody);

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

    // Add the product card to the products container
    const productsContainer = document.getElementById('products');
    productsContainer.insertBefore(productCard, productsContainer.firstChild);


};

const qrCodeSuccessCallback = async (decodedText, decodedResult) => {
    // Check if the barcode has already been scanned
    if (scannedBarcodes.includes(decodedText)) {
        return;
    }

    // Add the barcode to the scanned barcodes
    scannedBarcodes.push(decodedText);

    try {
        // Call the API to get the product details
        const response = await fetch(`https://localhost:7264/api/scan/${decodedText}`);
        const data = await response.json();

        // Check if the barcode was found
        if (data.status === 200) {
            // Display the product details
            displayProductDetails(data);
        }
    } catch (error) {
        console.error(error);
    }
};

const config = { fps: 10, qrbox: qrboxFunction };


// Start the barcode scanner
const startBarcodeScanner = () => {
    html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);
};

// Call the startBarcodeScanner function to start the scanning process
startBarcodeScanner();
