var interval;
var AttempID = document.getElementById("AttempID");
if (AttempID == null) {

    alert("Error  the Timer not working");
    ////
    document.querySelectorAll("button, a, input").forEach(el => {
        el.disabled = true;
        el.style.pointerEvents = "none";
        el.style.opacity = "0.5";
    });

    throw new Error("Invalid timer time");

}
var key = "RemainingSecondes" + AttempID.value;
var Secondes = localStorage.getItem(key);
console.log("Hello Timer");
if (Secondes === null) {

    //Secondes = @timeRemaining;
    Secondes = parseInt(document.getElementById("timeRemaining").value);

} else {
    Secondes = parseInt(Secondes);
}
function UpdateTimer() {

    var SecondesCounter = Secondes % 60;

    document.getElementById('Secondes').innerText = Formte(parseInt(SecondesCounter));
    document.getElementById('Minutes').innerText = Formte(parseInt(Secondes / 60));
}
 
function startTick() {
    UpdateTimer();
    interval = setInterval(function () {
        console.log("Timer Started");

        if (Secondes > 0) {
            Secondes--;
            UpdateTimer();

            localStorage.setItem(key, Secondes);
            if (Secondes == 0) {
                // Minutes--;
                SecondesCounter = 0;

            }
        }
        else {
            localStorage.removeItem(key);
            clearInterval(interval);
           
         
            Submit();
        }

    }, 1000)
}
startTick();

function Formte(value) {
    if (value < 10) {
        return '0' + value;
    }
    return '' + value;
}
function Submit() {
    var id = document.getElementById("ExamId");
    if (id == null) {
        alert("Unable To Submit the Exam");
        return;
    }
    var ExamId = id.value
    $.ajax({
        url: '/Student/TakeExam/SubmitExamPost',
        method: 'POST',
        data: { ExamId: ExamId },
        success: function (data) {
            toastr.success(data.message);
            if (data.redirectUrl) {
                window.location.href = data.redirectUrl;  // redirect to home
                return;
            }
           
        },
        error: function (xhr) {
            console.error("Submit failed:", xhr.responseText);
            alert("Submit failed");
        }
    });
} 