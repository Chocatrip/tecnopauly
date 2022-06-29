async function importarCotizacionHandler(numeroCotizacion) {
    var results = await fetch(`/api/importar-cotizacion/${numeroCotizacion}`, {
        method: 'POST'
    })
    console.log(results.status)
    if (results.status > 200) {
        Swal.fire({
            customClass: {
                container: 'my-swal'
            },
            icon: 'error',
            title: 'Cotizacion no existe',
            text: 'Ingrese un número de cotización que exista.'
        })
        $('#tabla-venta-cotizacion').removeClass('sk-loading')
    } else {
        Swal.fire({
            customClass: {
                container: 'my-swal'
            },
            icon: 'success',
            title: 'Cotizacion importada',
            text: 'La pagina se recargará al presionar Ok'
        }).then(function () {
            window.location.reload()
        });
    }
}
const importarCotizacion = async element => {
    console.log(">>importarCotizacion()")
    var numeroCotizacion = document.getElementById('importar-cotizacion-venta').value
    if (numeroCotizacion < 1) {
        Swal.fire({
            customClass: {
                container: 'my-swal'
            },
            icon: 'error',
            title: 'Error número cotización',
            text: 'Ingrese un valor válido de cotización'
        })
    } else {
        Swal.fire({
            customClass: {
                container: 'my-swal'
            },
            icon: 'question',
            showDenyButton: true,
            title: 'Cotización número: ' + numeroCotizacion,
            text: '¿Desea agregar los productos de esta cotización a la venta actual?',
            confirmButtonText: 'Si',
            denyButtonText: `No`,
        }).then((result) => {
            if (result.isConfirmed) {
                $('#tabla-venta-cotizacion').addClass('sk-loading')
                importarCotizacionHandler(numeroCotizacion)
            } 
        })
    }
}