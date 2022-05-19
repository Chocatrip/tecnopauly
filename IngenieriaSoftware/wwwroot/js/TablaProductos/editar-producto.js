function editarbegin() {
    console.log(">>editarBegin()")
}
async function editarsuccess() {
    console.log(">>editarSuccess()")
    Swal.fire({
        icon: 'success',
        title: 'Producto Editado',
        text: 'Se recargará la página en instantes'
    })
    await sleep(2000)
    window.location.reload()
}