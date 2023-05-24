$(document).ready(function () {
    
})
function DeleteProduct(productid) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'Post',
                url: '/Home/DeleteProduct',
                data: { 'productid': productid },
                success: function (response) {
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Product deleted successfully',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    setTimeout(function () {
                        location.reload();
                    }, 1600)
                }
            })
        }
    })
    
}