document.addEventListener("DOMContentLoaded", function() {
    //  lấy ra danh sách các thẻ img
    var list = document.querySelector('.slider__list');
    // lấy ra tất cả các tehr img
    var items = document.querySelectorAll('.slider__list-item');
    //  lấy ra nút quay lại
    var prev = document.getElementById('prev');
    // lấy ra nút next
    var next = document.getElementById('next');
    //  lấy ra các dấu chấm
    var dots = document.querySelectorAll('.dots li');
    var active = 0;

    // hàm này sẽ load ảnh đầu tiên nên
    function reload() {
        let checkLeft = items[active].offsetLeft;
        list.style.left = -checkLeft + 'px';
        updateDots();
    }

    //  thay đổi bị trí của nút chấm nhỏ
    function updateDots() {
        dots.forEach((dot, index) => {
            if (index === active) {
                dot.classList.add('active');
            } else {
                dot.classList.remove('active');
            }
        });
    }

    next.addEventListener('click', function() {
        active = (active + 1) % items.length; // Chuyển sang ảnh tiếp theo, nếu hết số ảnh trong Album thì chuyển sang ảnh đầu tiên
        reload();
    });

    prev.addEventListener('click', function() {
        active = (active - 1 + items.length) % items.length; // Quay lại ảnh trước đó, nếu hết số ảnh trong Album thì quay lại ảnh cuối cùng
        reload();
    });

    function autoSlide() {
        active = (active + 1) % items.length;
        reload();
    }

    window.addEventListener('load', function() {
        reload(); // Tải ảnh đầu tiên khi trang tải
        setInterval(autoSlide, 3000); // Tự động chuyển ảnh sau mỗi 3 giây
    });


    //  click đc cả mấy cái dot
    dots.forEach((dot, index) => {
        dot.addEventListener('click', function() {
            active = index;
            reload();
        });
    });
});


