$(() => {
    LoadPosData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/SignalrServer").build();
    connection.start();

    connection.on("LoadPosts", function () {
        LoadPosData();
    })

    LoadPosData();
   
    function LoadPosData() {
        var tr = '';
        $.ajax({
            url: '/Posts/GetPostList',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
            <td>
               ${v[0].CreatedDate}
            </td>
            <td>
              ${v[0].PostCategories.CategoryName}
            </td>
            <td>
              
            </td>
            <td>
                <a href='../Posts/Edit?id=${v.PostID}'>Edit</a> |
                <a href='../Posts/Details?id=${v.PostID}'>Details</a> |
                <a href='../Posts/Delete?id=${v.PostID}'>Delete</a>
            </td>
        </tr>`
                })
                $("#tableBody").html(tr);
            },

            error: (error) => {
                console.log(error)
            }
        });
    }
})