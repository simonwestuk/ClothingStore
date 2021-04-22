$(document).ready(function () {
    loadTableData();
});


function loadTableData() {
    var dataTable = $('#cat-data').DataTable({
        'ajax': {
            "url" : '/Admin/Category/GetAll'
        },
        'columns': [
            { 'data': 'name', 'width' : '60%' },
            { 'data': 'id', 'width': '40%' }
            //Add in button to edit and delete
        ]
        

    });
}