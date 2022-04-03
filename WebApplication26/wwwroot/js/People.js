$(() => {
    loadPeople();

    function loadPeople() {
        $.get('/home/GetAllPeople', function (people) {
            $("#people-table tr:gt(0)").remove();
            people.forEach(person => {
                $("#people-table tbody").append(`<tr>
                                                    <td>${person.firstName}</td>
                                                    <td>${person.lastName}</td>
                                                    <td>${person.age}</td>
                                                    <td><button class="btn btn-info" id="edit-btn" data-first-name=${person.firstName} data-last-name=${person.lastName}
                                                        data-age=${person.age} data-id=${person.id}>Edit</button></td>
                                                    <td><button class="btn btn-danger" id="delete-btn" data-id=${person.id}>Delete</button></td>
                                                 </tr>`);
            });
        });
    }
    $("#add-person-btn").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = parseInt($("#age").val());

        $.post('/home/AddPerson', { firstName, lastName, age }, function () {
            loadPeople();
            $("#first-name").val('');
            $("#last-name").val('');
            $("#age").val('');
        })
    })
    $("#people-table").on('click', '#edit-btn', function () {
        $("#first-name-modal").val($(this).data('first-name'));
        $("#last-name-modal").val($(this).data('last-name'));
        $("#age-modal").val($(this).data('age'));
        $("#id-modal").val($(this).data('id'));

        $("#EditModal").modal();
    })

    $("#EditModal").on('click', '#save-btn', function () {
        const firstName = $("#first-name-modal").val();
        const lastName = $("#last-name-modal").val();
        const age = $("#age-modal").val();
        const id = $("#id-modal").val();

        $.post('/home/EditPerson', { id, firstName, lastName, age }, function () {
            $("#EditModal").modal('hide');
            loadPeople();
        })
    })

    $("#people-table").on('click', '#delete-btn', function () {
        const id = $(this).data('id');

        $.post('/home/DeletePerson', { id }, function () {
            $("#EditModal").modal('hide');
            loadPeople();
        })
    })
});