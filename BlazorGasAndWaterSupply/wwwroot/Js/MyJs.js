function openForm() {
    document.getElementById("myForm").style.display = "block";
    document.querySelector('#BtnOpenForm').style.display = "none";
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
    document.querySelector('#BtnOpenForm').style.display = "block";
}

function disabledBtn() {
    document.querySelector('#btnSend').disabled = true;
}

function enabledBtn() {
    document.querySelector('#btnSend').disabled = false;
}

function MessageError() {
    alert("Invalid value of an empty string");
}
