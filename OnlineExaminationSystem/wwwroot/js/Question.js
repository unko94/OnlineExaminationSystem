/*const { type } = require("jquery");*/

var table;
 
if (typeof selected == "string") {
    selected = JSON.parse(selected);
}
$(document).ready(function () {
   table =  $('#questiontable').DataTable({
        "ajax": {
            url: '/admin/question/getall',
           dataSrc: 'questions',
           data: function (d) {
               //d.SelectedQ = JSON.stringify(selected);
               d.SelectedQ = selected;
           }
        },
        "columns": [
            { data: 'questionName', "width": "25%" },
            { data: 'questionText', "width": "15%" },
            { data: 'score', "width": "10%" },
            { data: 'difficulty', "width": "20%" },
            { data: 'category.categoryName', "width": "15%" },
            {
                data: "id",
                render: function (data, type, row) {
                    const checked = selected.includes(data) ? "checked" : "";

                    return `<div class=" m-75 btn-group" role=group>
                        <a class="btn btn-primary" href="/Admin/managUsers/LockOut/${data}">
                        <i class="bi bi-pencil-square"></i></a>

                         <input type="checkbox" name="id"  value="${data}" ${checked}/>    
                           <input type="text" name="category" hidden  value="${row.category.id}" />   
                            </div>
                            `;
                }
            }        
        ]
   });
    EditAssignedQ();
});

function AssginToExamjs() {
    document.getElementById('assgintoexam').addEventListener('click', function (e) {
        e.preventDefault();
        var allCheckedRows = $(table.rows().nodes()).find('input[name="id"]:checked');
        var ides = allCheckedRows.map(function () {
            return this.value;
        }).get();
        if (ides.length <5) {
            alert("exam should be at least 5 questions");
            return;
        }

        var examname = $('input[name="examname"]').val();
        var duration = $('input[name="duration"]').val();
       
        var category = CheckCategries()
        if (!category) {
            return;
        }
        $.ajax({
            type: "POST",
            url: '/admin/question/AssginToExam',
            traditional: true,
            dataType: 'json',
            data: { ides: ides, examname: examname, category: category, duration: duration },
            success: function (response) {
                if (response.success) {
                    table.ajax.reload(null, false);
                    alert("exam add successfully");
                } else {
                    alert("Failed Actions " + response.message.join("\n"));
                }
            },
            error: function (xhr, status, error) {
                alert("An unexpected error occurred: " + error);
            }
        });
    });
}
AssginToExamjs()

function CheckCategries() {
    var allCheckedRows = $(table.rows().nodes()).find('input[name="id"]:checked');
    var categories = allCheckedRows.map(function () {
        return $(this).siblings('input[name="category"]').val();
    }).get();
    var allsame = new  Set(categories).size === 1;
    if (!allsame) {
        alert("All questions must be from the same category!");
        return;
    }
    return categories[0];
}
 
function EditAssignedQ() {
    console.log("EditAssignedQ Hitted");
    console.log(table);

    document.getElementById('editAssignedQ').addEventListener('click', function (e) {
        e.preventDefault();
        var checkedQ = $(table.rows().nodes()).find('input[name="id"]:checked');
        var ides = checkedQ.map(function () {
            return this.value;
        }).get();

        console.log(ides.length);
        if (ides.length < 5) {
            alert("exam should be at least 5 questions");
            return;
        }
        var categoryides = CheckCategries();
        if (!categoryides) {
            alert("All questions should have the same category");
            return;
        }
 
        var examname = $('input[name="examname"]').val();
        var duration = $('input[name="duration"]').val();
        var examid = $('#examid').val();
        var category = $('#categoryid').val();
        $.ajax({
            type: "POST",
            url: 'Admin/Question/EditAssignedQPost',
            traditional: true,
            dataType: 'json',
            data: {
                examid: examid,
                ides: ides,
                examname: examname,
                categoryid: category,
                duration: duration,
               
            },
            success: function (response) {
                if (response.success) {
                    table.ajax.reload(null, false);
                    alert("exam add successfully");
                } else {
                    alert("Failed Actions " + response.message.join("\n"));
                }
            },
            error: function (xhr, status, error) {
                alert("An unexpected error occurred: " + error);
            }

        });
    });
}
 
 
 
 