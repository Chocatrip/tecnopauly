const eliminarProductoCarrito = async element => {
    var codproducto = element.dataset.codproducto
    $('#carrito-compra-tab').addClass('sk-loading')
    var idUsuario = check_cookie_name("userId")
    console.log(codproducto);
    console.log("user "+ idUsuario)
    await fetch(`/api/eliminar-producto-carrito/${codproducto}/${idUsuario}`, {
        method: 'POST'
    }).then(successData => handleEliminarProductoCarritoSuccess(successData, idUsuario))
}

async function handleEliminarProductoCarritoSuccess(successData, idCarrito) {
    console.log('handleGuardarCotizacionSuccess()')
    console.log(successData)
    $('#carrito-compra-tab').removeClass('sk-loading')

    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Producto eliminado!',
        text: 'La página se recargará al presionar Ok.'
    }).then(function () {
        window.location.reload()
    });
}

function check_cookie_name_eliminarprod(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) {
        return match[2]
    }
    else {
        console.log('--something went wrong---');
    }
}