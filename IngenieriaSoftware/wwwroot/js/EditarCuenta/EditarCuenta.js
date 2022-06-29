async function editarCuentaBegin() {
    console.log(">>editarCuentaBegin()")
    $('#editar-cuenta-form-wrapper').addClass('sk-loading')
}

async function editarCuentaSuccess() {
    $('#editar-cuenta-form-wrapper').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Cuenta editada correctamente',
        text: 'Se recargará la página al presionar Ok'
    }).then(function () {
        window.location.reload()
    });
}

async function editarCuentaFailure() {
    var error = check_cookie_name_editar_cuenta("errorEditarCuenta")
    error = error.replaceAll('-', ' ')
    $('#editar-cuenta-form-wrapper').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'error',
        title: 'Error',
        text: error
    })
}

function check_cookie_name_editar_cuenta(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) {
        return match[2]
    }
    else {
        console.log('--something went wrong---');
    }
}