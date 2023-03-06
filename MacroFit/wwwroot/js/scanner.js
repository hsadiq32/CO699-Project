// Create a new Html5Qrcode object
const html5QrCode = new Html5Qrcode("reader");

// Define the success callback function
const qrCodeSuccessCallback = (decodedText, decodedResult) => {
    // Check if the decoded text is a food barcode
    let barcodeRegex = /^5449\d{9}$/;
    if (barcodeRegex.test(decodedText)) {
        // Check if the barcode is already scanned
        if (scannedBarcodes.has(decodedText)) {
            // Add the barcode to the scanned set
            scannedBarcodes.add(decodedText);
            // Make a request to the API
            fetch('https://localhost:7264/api/scan/' + decodedText)
                .then(response => response.json())
                .then(data => {
                    // Check the status of the response
                    if (data.status == 200) {
                        // Barcode found, get the product details
                        let product = data.foodResponseDetails;
                        // Create a bootstrap card element for the product
                        let productHtml = `
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">${product.name}</h5>
                                    <p class="card-text">Carbohydrates: ${product.carbohydrates} g</p>
                                    <p class="card-text">Protein: ${product.protein} g</p>
                                    <p class="card-text">Fat: ${product.fat} g</p>
                                    <button class="btn btn-primary">Log data</button>
                                </div>
                            </div>
                        `;
                        // Append the product element to the container
                        document.getElementById('products').innerHTML += productHtml;
                    } else {
                        // Barcode not found, create a bootstrap alert element
                        let productHtml = `
                            <div class="alert alert-warning">
                                <p>Barcode not found: ${decodedText}</p>
                                <button class="btn btn-secondary">Add manually</button>
                            </div>
                        `;
                        // Append the alert element to the container
                        document.getElementById('products').innerHTML += productHtml;
                    }
                })
                .catch(error => {
                    // Handle the error
                    console.error(error);
                });
        }
    }
};

// Define the config object
const config = {
    fps: 30,
    qrbox: {
        width: 250, height: 250
    }
};

// Create a set of scanned barcodes
let scannedBarcodes = new Set();

// Start the camera and the QR code scanning
html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);