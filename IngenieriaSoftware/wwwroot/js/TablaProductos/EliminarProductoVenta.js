const eliminarProductoVenta = async element => {
    console.log(">>concretarVenta()")
    var idproducto = element.dataset.idproducto

            $('#tabla-venta-cotizacion').addClass('sk-loading')
            await fetch(`/api/eliminar-producto-venta/${idproducto}`, {
                method: 'POST'
            }).then(successData => handleEliminarProductoVentaSuccess(successData, idproducto))   
}

async function handleEliminarProductoVentaSuccess(successData, idproducto) {
    console.log('handleEliminarProductoVentaSuccess()')
    console.log(successData)
    $('#tabla-venta-cotizacion').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Producto Eliminado',
        text: 'La página se recargará al presionar Ok'
    }).then(function () {
        window.location.reload()
    });
}