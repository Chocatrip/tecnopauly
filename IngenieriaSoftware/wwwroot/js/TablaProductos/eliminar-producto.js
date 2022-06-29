const sleep = ms => new Promise(r => setTimeout(r, ms));

async function handleEliminarProductoSuccess(successData, idProducto) {
    console.log('handleEliminarDestinatarioSuccess()')
    console.log(successData)
    $('#editar-productos-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Producto Eliminado',
        text: 'Se recargará la página en instantes'
    })
    await sleep(2000)
    window.location.reload()
}

const eliminarProducto = async element => {
    console.log(">>eliminarProducto")
    $(`#${element.dataset.idproductodiv}`).addClass('sk-loading')

    var idProducto = element.dataset.idproducto
    console.log(element.dataset.idproducto)
    await fetch(`/api/eliminar-producto/${idProducto}`, {
        method: 'POST'
    }).then(successData => handleEliminarProductoSuccess(successData, idProducto))
}

animacionEditarProducto = async element => {
    console.log(">>animacionEditarProducto")
    console.log(element.dataset.idproductodiv)
    $(`#${element.dataset.idproductodiv}`).addClass('sk-loading')
}