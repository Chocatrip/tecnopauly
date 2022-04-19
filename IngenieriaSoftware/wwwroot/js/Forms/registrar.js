console.log("Script")
function beginRegistrar() {
    console.log('>> beginRegistrar')
}

function handleSuccessRegistrar(data) {
    swal("Completado", "Se ha registrado correctamente su cuenta", "success");
}

async function handleFailureRegistrar(xhr, status, error) {
    console.log("Hande failure " + error)
    await swal("Error", "Error en formulario", "error");

}