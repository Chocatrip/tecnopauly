const agregarProductoVenta = async element => {
    console.log(">> agregarProductoVenta")

    var idProducto = element.dataset.idproducto
    var idDiv = idProducto + '-agregar-venta-form'
    console.log("Div a buscar: " + 'inputAddVentaItem_Q_' + idProducto)
    var cantidadProducto = document.getElementById('inputAddVentaItem_Q_' + idProducto).value
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

    await fetch(`/api/agregar-item-venta/${idProducto}/${cantidadProducto}`, {
        method: 'POST'
    }).then(successData => handleAgregarProductoVentaSuccess(successData, idDiv))
}

async function handleAgregarProductoVentaSuccess(successData, idDiv) {
    console.log('handleGuardarCotizacionSuccess()')
    console.log(successData)
    $(`#${idDiv}`).removeClass('sk-loading')
    $('.modal').modal('hide')

    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Producto agregado a seccion venta!',
        text: 'Por favor revise la lista de productos a vender en venta.'
    })
}