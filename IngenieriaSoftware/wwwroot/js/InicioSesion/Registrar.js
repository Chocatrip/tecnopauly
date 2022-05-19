async function registrarfailure(xhr, err, a, b) {
    var error = check_cookie_name("errorRegister")
    error = error.replaceAll('-', ' ')
    $('#register-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'error',
        title: 'Error',
        text: error
    })
}
async function registrarsuccess() {
    console.log("Trigger success");
    $('#register-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Registro correcto',
        text: 'Se recargará la página en instantes'
    })
    await sleep(2000)
    window.location.reload()

}

function registrarbegin() {
    console.log("Begin ingresar");
    $('#register-tab-form').addClass('sk-loading')
}
