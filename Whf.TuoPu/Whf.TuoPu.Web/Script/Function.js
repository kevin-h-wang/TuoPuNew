function showdialog(url, width, height) {
    var owidth = window.screen.width;
    var oheight = window.screen.height;
    var left = (screen.availWidth - width) / 2;
    return window.showModalDialog(url, "", 'dialogWidth:' + width + 'px;dialogLeft:' + left + ';dialogTop:100;dialogHeight:' + height + 'px;scroll:yes;help:0;status:no;');
}

function winclose(val) {
    window.returnValue = val;
    window.close();
}

function CloseWindow() {
    window.close();
}

function ConfirmDelete(gridview, msgSelect, msgConfirmDelete) {
    var items = document.getElementById(gridview);
    var retSelect = false;
    for (var i = 0; i < items.rows.length; i++) {
        var inputs = items.rows[i].cells[0].getElementsByTagName("INPUT");
        for (var j = 0; j < inputs.length; j++) {
            if (inputs[j].type == "checkbox" && inputs[j].checked == true) {
                retSelect = true;
                break;
            }
        }
    }
    if (!retSelect) {
        alert(msgSelect);
        return false;
    }
    else {
        return window.confirm(msgConfirmDelete);
    }
}

function SelectAllCheckbox(checkbox, gridview) {
    var items = document.getElementById(gridview);
    for (var i = 0; i < items.rows.length; i++) {
        var inputs = items.rows[i].cells[0].getElementsByTagName("INPUT");
        for (var j = 0; j < inputs.length; j++) {
            if (inputs[j].type == "checkbox") {
                inputs[j].checked = checkbox.checked;
            }
        }
    }
}