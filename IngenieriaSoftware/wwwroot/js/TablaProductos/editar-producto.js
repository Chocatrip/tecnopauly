function editarbegin() {
    console.log(">>editarBegin()")
    $('#editar-productos-tab-form').addClass('sk-loading')
}
async function editarsuccess() {
    console.log(">>editarSuccess()")
    $('#editar-productos-tab-form').removeClass('sk-loading')
    c
    await sleep(2000)
    window.location.reload()
}