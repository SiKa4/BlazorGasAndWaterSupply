function openForm() {
    document.getElementById("myForm").style.display = "block";
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
}

function disabledBtn() {
    document.querySelector('#btnSend').disabled = true;
}

function enabledBtn() {
    document.querySelector('#btnSend').disabled = false;
}

