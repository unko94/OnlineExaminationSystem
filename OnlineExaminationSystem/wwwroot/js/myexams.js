$(document).ready(function () {
    $('#myexamstable').DataTable({
        "ajax": {
            url: '/admin/exam/getmyexams',
            dataSrc: ''
        },
        "columns": [
            { data: 'examName' },
            { data: 'createdAt' },
            { data: 'duration' },
            { data: 'category.categoryName' },
           
            {
                data: "examId",
                render: function (data, type, row) {
                    return `<div class=" m-75 btn-group" role=group>
                                <a class="btn btn-primary" href="/Admin/Exam/Edit/${data}">
                                Edit <i class="bi bi-pencil-square"></i></a>

                                <button type="button" class="btn btn-primary" data-bs-toggale="modal" data-bs-target="deletemyexam">
                                    <i class="bi bi-trash"></i>
                                </button>
                                   
                            </div>
                            `;
                }
            }
        ]
    });
});