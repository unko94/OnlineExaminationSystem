 

var table;
$(document).ready(function () {
      table =  $('#UsersTable').DataTable({
        "ajax": {
            url: '/admin/managUsers/getall',
              dataSrc: '',
              data: function (d) {
                  d.role =$('#rolefilter').val();
              }
        },
          "columns": [
          
            { data: 'fullName' },
            { data: 'userName'},
            { data: 'emailConfirmed' },
            { data: 'email' },
            { data: 'accessFailedCount' },
            { data: 'lockoutEnabled'},
            { data: 'lockoutEnd' },
            { data: 'role' },
            {
                data: "id",
                render: function (data, type, row) {
                    let lockoutend = null;
                    if (row.lockoutEnd) {
                        // Replace space with 'T' and fix format
                        let isoDateStr = row.lockoutEnd.replace(' ', 'T');
                        lockoutend = new Date(isoDateStr);
                    }

                    let datenow = new Date();
                    let isLocked = row.lockoutEnabled && lockoutend && datenow < lockoutend;

                    // Decide which button to show
                    let lockButtonHtml = '';
                    if (isLocked) {
                        // User is locked, show Unlock button
                        lockButtonHtml = `<a class="btn btn-primary" onClick="Unlock('${data}')"> <i class="bi bi-unlock"></i>
                        </a>`;
                    } else {
                        // User is not locked, show Lockout button
                        lockButtonHtml = `<a onClick="Lockout('${data}')" class="btn btn-danger mx-2">
                        <i class="bi bi-lock"></i></a>`;
                    }

                    return `
                   <div class="btn-group" role="group">
                    <a class="btn btn-primary" href="/Admin/ManagUsers/Edit/${data}">
                     <i class="bi bi-pencil-square"></i>
                    </a>
                       <a onClick=Delete('/Admin/ManagUsers/Delete/${data}') class="btn btn-danger mx-2"> 
                       <i class="bi bi-trash3"></i>
                       </a>
                         ${lockButtonHtml}
                   </div>
                    `;
    
                    
                }
            }
          ]
         
    });
});
$('#rolefilter').on('change', function () {
    table.ajax.reload();
})

function Unlock(id) {
    
    $.ajax({
        url: '/Admin/ManagUsers/Unlock',
        method: 'POST',
        data: { id: id },
        seccuss: function (data) {
            table.ajax.reload()
            toastr.success(data.message)
        }


    });
}
function Lockout(id) {
    Swal.fire({
        title: "Lockout user  ? ",
        html: '<input type="datetime-local" id="lockoutEndInput" class="swal2-input"/>',
        text: "you won't be able to revert this !",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "yes,Lockout it!",
        preConfirm: () => {
            const value = document.getElementById('lockoutEndInput').value;
            if (!value) {
                Swal.showValidationMessage("Please pick a lockout end date");
            }
            return value;
        }
        
    }).then((result) => {
        let lockoutEnd = result.value;
        if (lockoutEnd <= new Date()) {
            alret("the date is invalid");
            return;

        }
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/ManagUsers/LockOut' ,
                type: 'POST',
                data: {
                    id: id,
                    lockoutEnd: lockoutEnd
                },
                success: function (data) {
                    table.ajax.reload()
                    toastr.success(data.message)
                }
            })
        }
    });
}

function Delete(url) {

    Swal.fire({
        title: "are you sure ? ",
        text: "you won't be able to revert this !",
        icon: "Warning",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "yes,Delete it",
    }).then((result) => {
        if (result.isConfirmed) {
            let lockoutend = result.value;
            $.ajax({
                url:'/Admin/ManagUsers/Delete?id=${data}',
                type: 'POST',
                success: function (data) {
                    table.ajax.reload()
                    toastr.success(data.message)
                }
            })
        }
    });
}
 


 