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
                var postId = result['$id']
                var data = result['$values']
                if (!data || !postId) {
                    tr += 'No data'
                    return
                }
                /*$.each(result, (k, v) => {
                    console.log(v)*/
                tr += `<tr>
            <td>
               ${data[0].CreatedDate}
            </td>
            <td>
              ${data[0].UpdatedDate}
            </td>
            <td>
              ${data[0].Title}
            </td>
            <td>
              ${data[0].Content}
            </td>
            <td>
              ${data[0].PublishStatus}
            </td>
            <td>
              ${data[0].PostCategories.CategoryName}
            </td>
            <td>
                <a href='../Posts/Edit?id=${postId}'>Edit</a> |             
                <a href='../Posts/Delete?id=${postId}'>Delete</a>
            </td>
        </tr>`

                $("#tableBody").html(tr);
            },

            error: (error) => {
                console.log(error)
            }
        });
    }
})