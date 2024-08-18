document.addEventListener('DOMContentLoaded', function () {
    const postsPerPage = 4;
    const posts = document.querySelectorAll(".blog_post");
    const totalPages = Math.ceil(posts.length / postsPerPage);
    const pagination = document.querySelector(".pagination");
    let currentPage = 1;
    console.log(posts.length);

    function displayPosts(page) {
        const start = (page - 1) * postsPerPage;
        const end = page * postsPerPage;

        posts.forEach((post, index) => {
            if (index >= start && index < end) {
                post.style.display = "block";
            } else {
                post.style.display = "none";
            }
        });

        document.querySelectorAll(".pagination .page-num").forEach(pageNum => {
            pageNum.classList.remove("active");
        });
        document.querySelector(`.pagination .page-num[data-page="${page}"]`).classList.add("active");
    }

    function updatePaginationButtons() {
        if (pagination) {
            pagination.innerHTML = ''; 

           
            for (let i = 1; i <= totalPages; i++) {
                const pageButton = document.createElement('a');
                pageButton.href = "#";
                pageButton.classList.add('page-num');
                pageButton.setAttribute('data-page', i);
                pageButton.textContent = i;

                pagination.appendChild(pageButton);
            }

            
            document.querySelectorAll(".pagination .page-num").forEach(pageNum => {
                pageNum.addEventListener("click", function (event) {
                    event.preventDefault();
                    currentPage = parseInt(this.getAttribute("data-page"));
                    displayPosts(currentPage);
                });
            });

            displayPosts(currentPage); 
        }
    }

    const prevButton = document.getElementById("prev");
    const nextButton = document.getElementById("next");

    if (prevButton) {
        prevButton.addEventListener("click", function (event) {
            event.preventDefault();
            if (currentPage > 1) {
                currentPage--;
                displayPosts(currentPage);
            }
        });
    }

    if (nextButton) {
        nextButton.addEventListener("click", function (event) {
            event.preventDefault();
            if (currentPage < totalPages) {
                currentPage++;
                displayPosts(currentPage);
            }
        });
    }

    updatePaginationButtons();
});
