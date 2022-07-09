$(() => {
    var postcategory = [];
    var fullName = [];
    function getCategoryName(post) {
        if (post.PostCategories["$ref"] == null) {
            return post.PostCategories.CategoryName
        }
       return postcategory.find(category => {
            var ref = post.PostCategories["$ref"]
            return category["$id"] === ref
        })?.CategoryName
    }
    function getAppUserName(post) {
        if (post.AppUsers["$ref"] == null) {
            return post.AppUsers.FullName
        }
        return fullName.find(fullname => {
            var ref = post.AppUsers["$ref"]
            return fullname["$id"] === ref
        })?.FullName
    }
    function CreateRowList(post) {
        if (post.PostCategories["$ref"] == null) {           
            postcategory.push(post.PostCategories)
        }
        if (post.AppUsers["$ref"] == null) {
            fullName.push(post.AppUsers)
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
              ${getAppUserName(post)}
            </td>
            <td>
                <a hidden class="btnEditAdmin" href='../Posts/Edit?id=${post.PostID}' class='btn btn-sm btn-info'>Edit</a>
                <span hidden class="btnDelete"> | </span>
                <a hidden class="btnDelete" href='../Posts/Delete?id=${post.PostID}' class="btn btn-sm btn-danger">Delete</a>
            </td>
            <td>
                <a hidden class="btnEdit${getAppUserName(post)}" href='../Posts/EditUser?id=${post.PostID}' class='btn btn-sm btn-info'>Edit</a>
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
               

                let isMember = $('[name=isMember]').val();
                let UserNameLogin = $('[name=UserNameLogin]').val();
             

                if (isMember == "True") {
                    
                    $("#id").$values = "tableBody" + UserNameLogin;
                    $("#tableBody").attr("id", "tableBody" + UserNameLogin);
                    $("#tableBody" + UserNameLogin).html(listRow);
                    $('.btnEdit' + UserNameLogin).removeAttr("hidden");
                } else {
                    $("#tableBodyAdmin").html(listRow);
                    $('.btnEditAdmin').removeAttr("hidden");
                    $('.btnDelete').removeAttr("hidden");
                }
            },

            error: (error) => {
                console.log(error)
            }
        });
    }
})