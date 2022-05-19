function editarbegin() {
    console.log(">>editarBegin()")
    $('#editar-productos-tab-form').addClass('sk-loading')
}
async function editarsuccess() {
    console.log(">>editarSuccess()")
    $('#editar-productos-tab-form').removeClass('sk-loading')
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'success',
        title: 'Producto Editado',
        text: 'Se recargará la página en instantes'
    })
    await sleep(2000)
    window.location.reload()
}