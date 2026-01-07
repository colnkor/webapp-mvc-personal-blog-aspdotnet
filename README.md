# PersonalWebBlog

This simple blog web app allows to view all posts in index page. <br> 
`/admin` route allows to access an admin page. `/admin/login` authenticates user (the password is hardcoded into the code; lookup for `AccountController.cs`). <br>
`admin/new` allow to create new posts. <br>
`admin/edit/{id}` allows to edit post. <br>
`admin/delete/{id}` deletes specified post.

## TODO 

1. Pagination of posts
2. Connect database
