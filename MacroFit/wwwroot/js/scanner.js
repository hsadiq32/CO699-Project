const html5QrCode = new Html5Qrcode("reader");
const qrCodeSuccessCallback = (decodedText, decodedResult) => {
    /* handle success */
};


const config = { fps: 30, qrbox: { width: 250, height: 250 } };

// If you want to prefer back camera
html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);