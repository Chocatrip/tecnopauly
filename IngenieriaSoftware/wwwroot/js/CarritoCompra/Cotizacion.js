const guardarCotizacion = async element => {
    console.log(">>guardarCotizacion()")
    var idCliente = element.dataset.idcliente
    var idCarrito = element.dataset.idcarrito
    console.log("id cliente: " + idCliente)
    console.log("id carrito: " + idCarrito)

    await fetch(`/api/guardar-cotizacion/${idCarrito}`, {
        method: 'POST'
    }).then(successData => handleGuardarCotizacionSuccess(successData, idCarrito))
}

async function handleGuardarCotizacionSuccess(successData, idCarrito) {
    console.log('handleGuardarCotizacionSuccess()')
    console.log(successData)
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Numero Cotizacion: '+ idCarrito,
        text: 'Por favor guarde este numero para solicitar su compra!'
    })
}