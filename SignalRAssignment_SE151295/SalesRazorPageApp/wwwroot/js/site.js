// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { error } = require("jquery");
const { signalR } = require("../microsoft/signalr/dist/browser/signalr");

// Write your JavaScript code.


$(() => {
    LoadPosData();

    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();

    connection.on("LoadPosts", function () {
        LoadPosData();
    })

    LoadPosData();

    function LoadPosData() {
        var tr = '';
        $.ajax({
            url: '/PostsPage/MainPost',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                            <td>${v.AuthorID}</td>
                            <td>${v.CreatedDate}</td>
                            <td>${v.UpdatedDate}</td>
                            <td>${v.Title}</td>
                            <td>${v.Content}</td>
                            <td>${v.PublishStatus}</td>
                            <td>${v.CategoryID}</td>
                    </tr>`
                })
                $("#tableBody").html(tr)
            },

            error: (error) => {
                console.log(error)
            }
        });
    }
})