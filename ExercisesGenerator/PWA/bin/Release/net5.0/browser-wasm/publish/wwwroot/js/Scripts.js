function BlazorSetLocalStorage(key, value) {
    localStorage.setItem(key, value);
}

function BlazorGetLocalStorage(key) {
    return localStorage.getItem(key);
}

function BlazorRegisterStorageEvent(component) {
    window.addEventListener("storage", async e => {
        await component.invokeMethodAsync("OnStorageUpdated", e.key);
    });
}
function ChangeDisplay(ID, ColumnNumber, style) {
    var needHide = document.getElementById(ID).rows;
    for (var i = 0, len = needHide.length; i < len; i++) {
        var cell = needHide[i].cells[ColumnNumber];
        cell.style.display = style;
    }
}
function PrintDiv(ID) {
    var exercises = document.getElementById(ID);
    var win = window.open("");
    win.document.write('<html><head><link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />'
        + '<link href="css/app.css" rel="stylesheet" />'
        + '<link href="PWA.styles.css" rel="stylesheet" /></head><body>'
        + exercises.outerHTML + '</body>'
        + '<script>window.print();window.close();</script></html>');
}
function ShowMessage(text) {
    alert(text);
}
function Save(text, fileName) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', fileName);
    element.style.display = 'none';
    element.click();
}