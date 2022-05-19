async function iniciarSessionfailure(xhr, err, a) {
    var error = check_cookie_name("errorLogin")
    error = error.replaceAll('-', ' ')
    $('#login-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'error',
        title: 'Error',
        text: error 
    })
}
async function iniciarSessionsuccess() {
    console.log("Trigger success");
    $('#login-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Inicio de sesion correcto',
        text: 'Se recargará la página en instantes'
    })
    await sleep(2000)
    window.location.reload()

}

function iniciarSessionbegin() {
    console.log("Begin ingresar");
    $('#login-tab-form').addClass('sk-loading')

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
