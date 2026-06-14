document.addEventListener('DOMContentLoaded', function () {

    // ── Alert auto-dismiss with progress bar ──
    var alerts = document.querySelectorAll('.alert-dismissible');
    alerts.forEach(function (alert) {
        var progress = document.createElement('div');
        progress.className = 'alert-progress';
        alert.style.position = 'relative';
        alert.style.overflow = 'hidden';
        alert.appendChild(progress);

        setTimeout(function () {
            var bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });

    // ── Back to Top ──
    var backToTop = document.getElementById('backToTop');
    if (backToTop) {
        window.addEventListener('scroll', function () {
            if (window.scrollY > 300) {
                backToTop.classList.add('visible');
            } else {
                backToTop.classList.remove('visible');
            }
        });

        backToTop.addEventListener('click', function () {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
    }

    // ── Delete Modal: Enter key support ──
    var deleteModals = document.querySelectorAll('.modal[id$="deleteModal"], .modal[id="deleteModal"]');
    deleteModals.forEach(function (modal) {
        modal.addEventListener('keydown', function (e) {
            if (e.key === 'Enter') {
                var confirmBtn = modal.querySelector('.btn-danger[type="submit"]');
                if (confirmBtn) {
                    confirmBtn.click();
                }
            }
        });
    });

    // ── Responsive table-to-card toggle ──
    function toggleMobileTableLayout() {
        var tables = document.querySelectorAll('.table-responsive-mobile');
        tables.forEach(function (table) {
            var headerCells = table.querySelectorAll('thead th');
            var labels = [];
            headerCells.forEach(function (th) {
                labels.push(th.textContent.trim());
            });

            var rows = table.querySelectorAll('tbody tr');
            rows.forEach(function (row) {
                var cells = row.querySelectorAll('td');
                cells.forEach(function (td, index) {
                    if (labels[index]) {
                        td.setAttribute('data-label', labels[index]);
                    }
                });
            });
        });
    }

    toggleMobileTableLayout();

    // ── Active nav link based on current URL ──
    var currentPath = window.location.pathname.toLowerCase();
    var navLinks = document.querySelectorAll('.navbar-nav .nav-link');
    navLinks.forEach(function (link) {
        var href = link.getAttribute('href');
        if (href && href.toLowerCase() !== '#') {
            if (currentPath === href.toLowerCase() ||
                (href.toLowerCase() !== '/' && currentPath.startsWith(href.toLowerCase()))) {
                link.classList.add('active');
            }
        }
    });

    // ── Form validation shake effect ──
    var forms = document.querySelectorAll('form');
    forms.forEach(function (form) {
        form.addEventListener('submit', function () {
            var invalidInputs = form.querySelectorAll('.is-invalid');
            invalidInputs.forEach(function (input) {
                input.classList.add('validation-shake');
                setTimeout(function () {
                    input.classList.remove('validation-shake');
                }, 300);
            });
        });
    });

    // ── Form input focus label enhancement ──
    var formInputs = document.querySelectorAll('.form-control, .form-select');
    formInputs.forEach(function (input) {
        input.addEventListener('focus', function () {
            var label = this.closest('.mb-3, .col-md-6, .col-md-4, .col-md-8, .g-3, .g-2')?.querySelector('.form-label');
            if (label) {
                label.style.color = '#4F46E5';
            }
        });
        input.addEventListener('blur', function () {
            var label = this.closest('.mb-3, .col-md-6, .col-md-4, .col-md-8, .g-3, .g-2')?.querySelector('.form-label');
            if (label) {
                label.style.color = '#374151';
            }
        });
    });
});
