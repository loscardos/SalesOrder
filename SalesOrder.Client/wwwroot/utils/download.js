function saveAsFile(filename, bytesBase64) {
    let link = document.createElement('a');
    
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    
    link.click();
    document.body.removeChild(link);
}

function printPage() {
    window.print();
}