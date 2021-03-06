var dataTable;

$(document).ready(function () {
    loadTableData();
});



function loadTableData() {
     dataTable = $('#type-data').DataTable({
        'ajax': {
            "url": '/Admin/Type/GetAll'
        },
        'columns': [
            { 'data': 'name', 'width': '60%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                            <div class="text-center"> 
                                <a href="/Admin/Type/Upsert/${data}" class="btn btn-success text-white">Edit</a>
                                <a onclick=Delete("/Admin/Type/Delete/${data}") class="btn btn-danger text-white">Delete</a>
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
                'Your file has been deleted.',
                'success'
            )
        }
    })


    
}