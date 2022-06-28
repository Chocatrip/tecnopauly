async function ingresarfailure(xhr, err, a, b) {
    var error = check_cookie_name("errorIngresarProducto")
    error = error.replaceAll('-', ' ')
    $('#ingresar-productos-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'error',
        title: 'Error',
        text: error
    })
}
async function ingresarsuccess() {
    console.log("Trigger success");
    $('#ingresar-productos-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
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
    $('#ingresar-productos-tab-form').addClass('sk-loading')
}

function check_cookie_name(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) {
        return match[2]
    }
    else {
        console.log('--something went wrong---');
    }
}
