const pnPreventSubmitOnEnter = (e) => {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
}
function ingresarbegin(){
    console.log("Begin ingresar");
}
function ingresarsuccess(){
    console.log("Trigger success");
    $('.modal').modal('hide');
}