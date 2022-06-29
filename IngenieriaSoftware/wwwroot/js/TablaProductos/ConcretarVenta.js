const concretarVenta = async element => {
    console.log(">>concretarVenta()")
    var idVenta = element.dataset.idventa


    await fetch(`/api/concretar-venta/${idVenta}`, {
        method: 'POST'
    }).then(successData => handleConcretarVentaSuccess(successData, idVenta))
}

async function handleConcretarVentaSuccess(successData, idVenta) {
    console.log('handleConcretarVentaSuccess()')
    console.log(successData)
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Cotización vendida',
        text: 'La página se recargará al presionar Ok'
    }).then(function () {
        window.location.reload()
    });
}