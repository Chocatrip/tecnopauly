const agregarProductoCarrito = async element => {
    console.log(">>agregarProductoCarrito")

    var idUsuario = check_cookie_name("userId")
    var idProducto = element.dataset.idproducto
    var idDiv = idProducto + '-agregar-carrito-form'
    $(`#${idDiv}`).addClass('sk-loading')

    if (idUsuario == -1) {
        Swal.fire({
            customClass: {
                container: 'my-swal'
            },
            icon: 'info',
            title: 'Por favor, ingrese sesion',
            text: 'Para poder crear una cotizacion debe iniciar sesion.'
        })
        $(`#${idDiv}`).removeClass('sk-loading')
        $('.modal').modal('hide')
        return
    }

    console.log("Id producto: " + idProducto)
    var cantidadProducto = document.getElementById('inputAddCartItem_Q_' + idProducto).value

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