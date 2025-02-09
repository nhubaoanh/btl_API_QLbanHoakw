var app = angular.module('AppBanHoa', []);

app.controller("IndexCtrl", function($scope, $http) {
    // Initialize scope variables
    $scope.list_menus = [];
    $scope.list_items = [];
    $scope.list_product = [];
    $scope.list_sp = [];
    $scope.list_order = [];
    $scope.list_item = [];
    $scope.list_nhanvien = [];
    $scope.newSanPham = {};
    $scope.newOrder = {};
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.totalItems = 0;
    $scope.pages = [];
    $scope.products = [];


// dữ liệu được đồng bộ hóa 2 chiều giữa controler và view

    // Load menus
    $scope.LoadMenus = function() {
        $http.get(current_url + '/api-nguoidung/danhmuc/get-All-danhmuc')
            .then(function(response) {
                $scope.list_menus = response.data;
                console.log("Danh sách menu:", $scope.list_menus);
            })
            .catch(function(error) {
                console.error("LoadMenus Error:", error);
            });
    };

    // Load items
    $scope.LoadItems = function() {
        $http.get(current_url + '/api-nguoidung/sanpham/getAll-sanpham')
            .then(function(response) {
                $scope.list_items = response.data.map(function(item) {
                    if (item.sanPham_img) {
                        item.sanPham_img = `data:image/jpg;base64,${item.sanPham_img}`;
                    }
                    return item;
                });
                console.log("Danh sách sản phẩm:", $scope.list_items);
            })
            .catch(function(error) {
                console.error("LoadItems Error:", error);
            });
    };



    // Load items
    $scope.LoadProduct = function() {
        $http.get(current_url + '/api-admin/sanpham/getAll-sanpham')
            .then(function(response) {
                $scope.list_product = response.data.map(function(item) {
                    if (item.sanPham_img) {
                        item.sanPham_img = `data:image/jpg;base64,${item.sanPham_img}`;
                    }
                    return item;
                });
                console.log("Danh sách sản phẩm:", $scope.list_product);
            })
            .catch(function(error) {
                console.error("LoadItems Error:", error);
            });
    };
    // Open modal
    $scope.openModal = function() {
        document.getElementById('modal-form').style.display = 'flex';
    };

    // Close modal
    $scope.closeModal = function() {
        document.getElementById('modal-form').style.display = 'none';
    };

    // Create product
    $scope.createSanPham = function() {
        if (!$scope.newSanPham.danhmuc_id || !$scope.newSanPham.sanPham_color || !$scope.newSanPham.sanPham_size || !$scope.newSanPham.sanPham_img) {
            console.error('Thông tin sản phẩm không đầy đủ');
            return;
        }

        var sanPhamData = {
            sanpham_id: $scope.newSanPham.sanPham_id,
            danhmuc_id: $scope.newSanPham.danhmuc_id,
            sanPham_name: $scope.newSanPham.sanPham_name,
            sanPham_color: $scope.newSanPham.sanPham_color,
            sanPham_size: $scope.newSanPham.sanPham_size,
            sanPham_img: $scope.newSanPham.sanPham_img,
            sanPham_price: $scope.newSanPham.sanPham_price
        };

        $http.post(current_url + '/api-admin/sanpham/create-sanpham', sanPhamData, {
            headers: { 'Content-Type': 'application/json' }
        }).then(function(response) {
            console.log('Thêm sản phẩm thành công:', response.data);
            $scope.list_item.push(response.data);
            $scope.closeModal();
            $scope.newSanPham = {};
        }).catch(function(error) {
            console.error('Lỗi khi thêm sản phẩm:', error);
        });
    }

    // Create product
    $scope.updateSanPham = function() {
        if (!$scope.newSanPham.danhmuc_id || !$scope.newSanPham.sanPham_color || !$scope.newSanPham.sanPham_size || !$scope.newSanPham.sanPham_img) {
            console.error('Thông tin sản phẩm không đầy đủ');
            return;
        }

        var sanPhamData = {
            sanpham_id: $scope.newSanPham.sanPham_id,
            danhmuc_id: $scope.newSanPham.danhmuc_id,
            sanPham_name: $scope.newSanPham.sanPham_name,
            sanPham_color: $scope.newSanPham.sanPham_color,
            sanPham_size: $scope.newSanPham.sanPham_size,
            sanPham_img: $scope.newSanPham.sanPham_img,
            sanPham_price: $scope.newSanPham.sanPham_price
        };

        $http.post(current_url + '/api-admin/sanpham/update-sanpham', sanPhamData, {
            headers: { 'Content-Type': 'application/json' }
        }).then(function(response) {
            console.log('Thêm sản phẩm thành công:', response.data);
            $scope.list_item.push(response.data);
            $scope.closeModal();
            $scope.newSanPham = {};
        }).catch(function(error) {
            console.error('Lỗi khi thêm sản phẩm:', error);
        });
    }


    // load nhan vien

    $scope.LoadNhanvien = function() {
        $http.get(current_url + '/api-admin/NhanVien/nhanvien-getAll')
            .then(function(response) {
                $scope.list_nhanvien = response.data.map(function(item) {
                    return item;
                });
                console.log("Danh sách nhân viên:", $scope.list_nhanvien);
            })
            .catch(function(error) {
                console.error("LoadItems Error:", error);
            });
    };

    // Create product
    $scope.createUser = function() {
       

        var sanPhamData = {
            nhanvien_id: $scope.newSanPham.nhanvien_id,
            users_id: $scope.newSanPham.users_id,
            nhanvien_email: $scope.newSanPham.nhanvien_email,
            nhanvien_password: $scope.newSanPham.nhanvien_password
        };

        $http.post(current_url + '/api-admin/NhanVien/create-nhanvien', sanPhamData, {
            headers: { 'Content-Type': 'application/json' }
        }).then(function(response) {
            console.log('Thêm nhân viên thành công:', response.data);
            $scope.list_nhanvien.push(response.data);
            $scope.closeModal();
            $scope.newSanPham = {};
        }).catch(function(error) {
            console.error('Lỗi khi thêm nhân viên:', error);
        });
    };

    // Update product
    $scope.updateUser = function() {
        var sanPhamData = {
            nhanvien_id: $scope.newSanPham.nhanvien_id,
            users_id: $scope.newSanPham.users_id,
            nhanvien_email: $scope.newSanPham.nhanvien_email,
            nhanvien_password: $scope.newSanPham.nhanvien_password
        };

        $http.post(current_url + '/api-admin/NhanVien/update-nhanvien', sanPhamData, {
            headers: { 'Content-Type': 'application/json' }
        }).then(function(response) {
            console.log('Thêm nhân viên thành công:', response.data);
            $scope.list_nhanvien.push(response.data);
            $scope.closeModal();
            $scope.newSanPham = {};
        }).catch(function(error) {
            console.error('Lỗi khi thêm nhân viên:', error);
        });
    };







    // Load orders
    $scope.LoadOrder = function() {
        $http.get(current_url + '/api-admin/Order/getAll-order')
            .then(function(response) {
                $scope.list_order = response.data;
                console.log("Danh sách đơn hàng:", $scope.list_order);
            })
            .catch(function(error) {
                console.error("LoadOrder Error:", error);
            });
    };

    // Create order
    $scope.CreateOrder = function() {
        if (!$scope.newOrder.order_id || !$scope.newOrder.ngaymua || !$scope.newOrder.khachhang_name || !$scope.newOrder.nhanvien_id || !$scope.newOrder.trangthai || !$scope.newOrder.chietkhau || !$scope.newOrder.sodienthoai || !$scope.newOrder.diachi) {
            console.error('Thông tin đơn hàng không đầy đủ');
            return;
        }

        var OrderData = {
            order_id: $scope.newOrder.order_id,
            ngaymua: $scope.newOrder.ngaymua,
            khachhang_name: $scope.newOrder.khachhang_name,
            nhanvien_id: $scope.newOrder.nhanvien_id,
            trangthai: $scope.newOrder.trangthai,
            chietkhau: $scope.newOrder.chietkhau,
            tongtien: $scope.newOrder.tongtien,
            sodienthoai: $scope.newOrder.sodienthoai,
            diachi: $scope.newOrder.diachi,
            sanpham_id: $scope.newOrder.sanpham_id,
            sanpham_name: $scope.newOrder.sanpham_name,
            soluong: $scope.newOrder.soluong,
            dongia: $scope.newOrder.dongia,
            status: $scope.newOrder.status
        };

        $http.post(current_url + '/api-admin/Order/create-orders', OrderData, {
            headers: { 'Content-Type': 'application/json' }
        }).then(function(response) {
            console.log('Thêm đơn hàng thành công:', response.data);
            $scope.list_item.push(response.data);
            $scope.closeModal();
            $scope.newOrder = {};
        }).catch(function(error) {
            console.error('Lỗi khi thêm đơn hàng:', error);
        });
    };

    // Update order
    $scope.updateOrder = function() {
        if (!$scope.newOrder.order_id || !$scope.newOrder.ngaymua || !$scope.newOrder.khachhang_name || !$scope.newOrder.nhanvien_id || !$scope.newOrder.trangthai || !$scope.newOrder.chietkhau || !$scope.newOrder.sodienthoai || !$scope.newOrder.diachi) {
            console.error('Thông tin đơn hàng không đầy đủ');
            return;
        }

        var OrderData = {
            order_id: $scope.newOrder.order_id,
            ngaymua: $scope.newOrder.ngaymua,
            khachhang_name: $scope.newOrder.khachhang_name,
            nhanvien_id: $scope.newOrder.nhanvien_id,
            trangthai: $scope.newOrder.trangthai,
            chietkhau: $scope.newOrder.chietkhau,
            tongtien: $scope.newOrder.tongtien,
            sodienthoai: $scope.newOrder.sodienthoai,
            diachi: $scope.newOrder.diachi
        };

        $http.post(current_url + '/api-nguoidung/order/update-order', OrderData, {
            headers: { 'Content-Type': 'application/json' }
        }).then(function(response) {
            console.log('Sửa đơn hàng thành công:', response.data);
            $scope.list_item.push(response.data);
            $scope.closeModal();
            $scope.newOrder = {};
        }).catch(function(error) {
            console.error('Lỗi khi sửa đơn hàng:', error);
        });
    };

    // Update order
    $scope.deleteOrder = function() {
      
        var OrderData = {
            order_id: $scope.newOrder.order_id,
            ngaymua: $scope.newOrder.ngaymua,
            khachhang_name: $scope.newOrder.khachhang_name,
            nhanvien_id: $scope.newOrder.nhanvien_id,
            trangthai: $scope.newOrder.trangthai,
            chietkhau: $scope.newOrder.chietkhau,
            tongtien: $scope.newOrder.tongtien,
            sodienthoai: $scope.newOrder.sodienthoai,
            diachi: $scope.newOrder.diachi
        };

        $http.post(current_url + '/api-nguoidung/order/delete-order', OrderData, {
            headers: { 'Content-Type': 'application/json' }
        }).then(function(response) {
            console.log('xóa đơn hàng thành công:', response.data);
            $scope.list_item.push(response.data);
            $scope.closeModal();
            $scope.newOrder = {};
        }).catch(function(error) {
            console.error('Lỗi khi sửa đơn hàng:', error);
        });
    };

    
    // Load products with pagination
    $scope.LoadProducts = function(pageIndex, pageSize) {
        console.log("Calling LoadProducts with pageIndex:", pageIndex, "and pageSize:", pageSize);
        $http.post(current_url + '/api-nguoidung/sanpham/search-sanpham', null, {
            params: { pageIndex: pageIndex, pageSize: pageSize }
        }).then(function(response) {
            console.log("LoadProducts Response:", response);
            if (response.data && response.data.products && response.data.total !== undefined) {
                $scope.products = response.data.products;
                $scope.totalItems = response.data.total;
                $scope.updatePages();
            } else {
                console.error("Unexpected response structure:", response);
            }
        }).catch(function(error) {
            console.error("LoadProducts Error:", error);
        });
    };

    // Calculate number of pages
    $scope.updatePages = function() {
        const totalPages = Math.ceil($scope.totalItems / $scope.pageSize);
        $scope.pages = Array.from({ length: totalPages }, (_, i) => i + 1);
        console.log("Updated pages:", $scope.pages);
    };

    // Go to previous page
    $scope.prevPage = function() {
        if ($scope.currentPage > 1) {
            $scope.currentPage--;
            $scope.LoadProducts($scope.currentPage, $scope.pageSize);
        }
    };

    // Go to next page
    $scope.nextPage = function() {
        const totalPages = Math.ceil($scope.totalItems / $scope.pageSize);
        if ($scope.currentPage < totalPages) {
            $scope.currentPage++;
            $scope.LoadProducts($scope.currentPage, $scope.pageSize);
        }
    };

    // Go to specific page
    $scope.goToPage = function(page) {
        $scope.currentPage = page;
        $scope.LoadProducts($scope.currentPage, $scope.pageSize);
    };



    // Add to cart

        $scope.addToCart = function(item) {
            // Add the item to the cart (localStorage)
            alert('Thêm sản phẩm vào giỏ hàng thành công');
            let cart = JSON.parse(localStorage.getItem('cart')) || [];
            cart.push(item);
            localStorage.setItem('cart', JSON.stringify(cart));
    
            // Navigate to the cart page
            $window.location.href = './datHang.html';
        };
    // Initial load
    $scope.LoadMenus();
    $scope.LoadItems();
    $scope.LoadOrder();
    $scope.LoadProduct();
    $scope.LoadNhanvien();
    // $scope.createUser();
    // $scope.updateUser();
    // $scope.LoadProducts($scope.currentPage, $scope.pageSize);
});