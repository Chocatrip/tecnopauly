cerrarSesion = async element => {
    console.log(">>cerrarSesion()")
    console.log(element.dataset)
    Swal.fire({
        customClass: {
            container: 'my-swal'
        },
        icon: 'info',
        title: 'Se cerrará la sesión de la cuenta',
        text: 'Gracias por utilizar nuestra página!'
    })
    document.cookie = "userInfo= ; expires = Thu, 01 Jan 1970 00:00:00 GMT"
    await sleep(2000)
    var url = element.dataset.urlindex;
    location.href = url;
}
