
$(document).ready(function () {
    $('#recentresultseble').DataTable({
        "ajax": {
            url: '/student/home/getall',
            dataSrc: ''
        },
        "columns": [
            { data: 'exam.examName', className: 'text-center' },
            { data: 'score', className: 'text-center' },
            { data: 'status', className: 'text-center' },
            { data: 'result', className: 'text-center' }
        ]
    });
});

$(document).ready(function () {
    $('#availableexamstable').DataTable({
        "ajax": {
            url: '/student/home/getallExams',
            dataSrc: ''
        },
        "columns": [
            { data: 'examName', className: 'text-center' },
            { data: 'category.categoryName', className: 'text-center' },
            { data: 'duration', className: 'text-center' },

            {
                data: 'id',
                render: function (data, type, row) {

                    return `<a class="btn btn-primary" href="/Student/TakeExam/Index/${data}">Start Exam</a>`;
                },
                className: 'text-center',
                searchable: 'false',
                orderable:'false'
               
            }
        ]
    });
});