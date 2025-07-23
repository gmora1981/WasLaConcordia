window.downloadFileFromByteArray = ({ byteArray, fileName, contentType }) => {
    const blob = new Blob([new Uint8Array(byteArray)], { type: contentType });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement("a");
    anchor.href = url;
    anchor.download = fileName ?? "file";
    anchor.click();
    URL.revokeObjectURL(url);
};
