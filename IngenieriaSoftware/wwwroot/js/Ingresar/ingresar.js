
async function ingresarsuccess() {
    console.log("Trigger success");
    Swal.fire({
        icon: 'success',
        title: 'Producto Agregado',
        text: 'Se recargará la página en instantes'
    })
    await sleep(2000)
    window.location.reload()

}

const pnPreventSubmitOnEnter = (e) => {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
}
function ingresarbegin(){
    console.log("Begin ingresar");
}
