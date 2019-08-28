
var GamayunSectionCreate = function () {
    var topicLock = false;
    var semesterLock = false;
    var students = []

    function _topicAction(item) {
        $("#topicName").val(item.name)
        $("#topicId").val(item.id)
        topicLock = true;

        if (topicLock && semesterLock) {
            $("#create-section").prop("disabled", false);
        }
    }

    function _semesterAction(item) {
        if (!item.isActive) {
            alert("Please choose active semester");
            return;
        }
        $("#semesterName").val(item.major)
        $("#semesterId").val(item.id)
        semesterLock = true;

        if (topicLock && semesterLock) {
            $("#create-section").prop("disabled", false);
        }
    }

    function _studentAction(item) {
        console.log(students);
        var studentId = item.id
        if ($.inArray(studentId, students) != -1) {
            return;
        }

        students.push(studentId);
        var rowId = "row_" + item.id;
        var row = $("<tr id='" + rowId + "'>" + "<td>" + item.firstName + " " + item.lastName + "<td><td><button type='button' class='btn btn-danger'>Remove</button></td>" + "</tr>");
        $("#student-container").append(row);

        $("#" + rowId + " button").click(function () {
            students = students.filter(x => x !== studentId);
            $("#" + rowId).remove();
        })

        $("[name='students']").val(students.toString());
    }

    return {
        topicAction: _topicAction,
        semesterAction: _semesterAction,
        studentAction: _studentAction,
    }
}