$(document).ready(function () {
    $('#examtable').DataTable({
        "ajax": {
            url: '/admin/exam/getall',
            dataSrc: ''
        },
        "columns": [
            { data: 'examName'},
            { data: 'examScore' },
            { data: 'totalQuestions' },
            { data: 'category' },
            { data: 'createdBy' },
            { data: 'createdAt' },
            {
                data: "examId",
                render: function (data, type, row) {
                    return `<div class=" m-75 btn-group" role=group><a class="btn btn-primary" href="/Admin/Exam/Edit/${data}">
                    Edit <i class="bi bi-pencil-square"></i></a>
                                   <form method="post" action="/Admin/Exam/Delete/${data}" style="display:inline;">
                                      <button class="btn btn-secondary">
                                      <i class="bi bi-trash3-fill"></i>Delete
                                      </button>
                                       </form>
                            </div>
                            `;
                }
            }
        ]
    });
});


 

               