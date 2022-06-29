const agregarProductoCarrito = async element => {
    console.log(">>agregarProductoCarrito")

    var idUsuario = check_cookie_name("userId")
    var idProducto = element.dataset.idproducto
    var idDiv = idProducto + '-agregar-carrito-form'
    var cantidadProducto = document.getElementById('inputAddCartItem_Q_' + idProducto).value
    $(`#${idDiv}`).addClass('sk-loading')

    if (cantidadProducto <= 0) {
        Swal.fire({
            customClass: {
                container: 'my-swal'
            },
            icon: 'Error',
            title: 'Cantidad de producto mínima.',
            text: 'Por favor, ingrese una cantidad mayor o igual a 1 producto.'
        })
        $(`#${idDiv}`).removeClass('sk-loading')
        return
    }

    if (idUsuario == -1) {
        Swal.fire({
            customClass: {
                container: 'my-swal'
            },
            icon: 'warning',
            title: 'Por favor, inicie sesión',
            text: 'Para poder crear una cotizacion debe iniciar sesión.'
        })
        $(`#${idDiv}`).removeClass('sk-loading')
        $('.modal').modal('hide')
        return
    }

    console.log("Id producto: " + idProducto)
    

    await fetch(`/api/agregar-item-carrito/${idProducto}/${cantidadProducto}`, {
        method: 'POST'
    }).then(successData => handleGuardarCotizacionSuccess(successData, idDiv))
}

async function handleGuardarCotizacionSuccess(successData, idDiv) {
    console.log('handleGuardarCotizacionSuccess()')
    console.log(successData)
    $(`#${idDiv}`).removeClass('sk-loading')
    $('.modal').modal('hide')

    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Producto agregado al carrito!',
        text: 'Por favor revise su producto en la sección de carrito de cotización.'
    })
}

function check_cookie_name(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) {
        return match[2]
    }
    else {
        return -1;
    }
}