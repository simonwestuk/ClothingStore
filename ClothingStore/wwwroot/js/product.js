var dataTable;

$(document).ready(function () {
    loadTableData();
});



function loadTableData() {
     dataTable = $('#prod-data').DataTable({
        'ajax': {
            "url": '/Admin/Product/GetAll'
        },
        'columns': [
            { 'data': 'description', 'width': '25%' },
            { 'data': 'price', 'width': '25%' },
            { 'data': 'category.name', 'width': '25%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                            <div class="text-center"> 
                                <a href="/Admin/Product/Upsert/${data}" class="btn btn-success text-white">Edit</a>
                                <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger text-white">Delete</a>
                            </div>
                           `;
                },
                'width': '40%'
            }
        ]

    });
}

function Delete(url)
{
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
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
            Swal.fire(
                'Deleted!',
                'Your product has been deleted.',
                'success'
            )
        }
    })


    
}