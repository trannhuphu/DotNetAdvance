$(() => {
    var postcategory = [];
    function getCategoryName(post) {
        if (post.PostCategories["$ref"] == null) {
            return post.PostCategories.CategoryName
        }
       return postcategory.find(category => {
            var ref = post.PostCategories["$ref"]
            return category["$id"] === ref
        })?.CategoryName
    }
    function CreateRowList(post) {
        if (post.PostCategories["$ref"] == null) {           
            postcategory.push(post.PostCategories)
        }
        
        var tr = "";
        tr += `<tr>
            <td>
               ${post.CreatedDate}
            </td>
            <td>
              ${post.UpdatedDate}
            </td>
            <td>
              ${post.Title}
            </td>
            <td>
              ${post.Content}
            </td>
            <td>
              ${post.PublishStatus}
            </td>
            <td>
              ${getCategoryName(post)}
            </td>
            <td>
                <a href='../Posts/Edit?id=${post.PostID}'>Edit</a> |
                <a href='../Posts/Delete?id=${post.PostID}'>Delete</a>
            </td>
        </tr>`
        return tr;


    }
 //   LoadPosData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/SignalrServer").build();
    connection.start();

    connection.on("LoadPosts", function () {
        LoadPosData();
    })

    LoadPosData();

    function LoadPosData() {
        postcategory = [];
        var listRow = ""
        

        $.ajax({
            url: '/Posts/GetPostList',
            method: 'GET',

            success: (result) => {
                
                var postList = result['$values']
                if (!postList) {                
                    return
                    
                }
                listRow = postList.map(CreateRowList).join("");
                $("#tableBody").html(listRow);
                
            },

            error: (error) => {
                console.log(error)
            }
        });
    }
})