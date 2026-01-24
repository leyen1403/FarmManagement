/**
 * FarmManagement - Global Application JavaScript
 * Version: 1.0.0
 * * Global Namespace Pattern - Tất cả chức năng được đóng gói trong FarmApp
 */

const FarmApp = (function () {
    'use strict';

    // ============================================
    // Private Variables
    // ============================================
    const _config = {
        locale: 'vi-VN',
        currency: 'VND',
        dateFormat: 'dd/MM/yyyy',
        alertAutoDismissDelay: 5000,
        debounceDelay: 300,
        animationDuration: 300
    };

    let _isInitialized = false;

    // ============================================
    // Utility Functions
    // ============================================
    const Utils = {
        /**
         * Debounce function - Giới hạn tần suất gọi hàm
         */
        debounce: function (func, wait = _config.debounceDelay) {
            let timeout;
            return function executedFunction(...args) {
                const later = () => {
                    clearTimeout(timeout);
                    func(...args);
                };
                clearTimeout(timeout);
                timeout = setTimeout(later, wait);
            };
        },

        /**
         * Throttle function - Giới hạn số lần gọi hàm trong khoảng thời gian
         */
        throttle: function (func, limit = 100) {
            let inThrottle;
            return function (...args) {
                if (!inThrottle) {
                    func.apply(this, args);
                    inThrottle = true;
                    setTimeout(() => inThrottle = false, limit);
                }
            };
        },

        /**
         * Deep clone object
         */
        deepClone: function (obj) {
            return JSON.parse(JSON.stringify(obj));
        },

        /**
         * Check if element is in viewport
         */
        isInViewport: function (element) {
            const rect = element.getBoundingClientRect();
            return (
                rect.top >= 0 &&
                rect.left >= 0 &&
                rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
                rect.right <= (window.innerWidth || document.documentElement.clientWidth)
            );
        },

        /**
         * Generate unique ID
         */
        generateId: function (prefix = 'farm') {
            return `${prefix}_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`;
        },

        /**
         * Escape HTML để tránh XSS
         */
        escapeHtml: function (text) {
            const div = document.createElement('div');
            div.textContent = text;
            return div.innerHTML;
        },

        /**
         * Parse query string to object
         */
        parseQueryString: function (queryString = window.location.search) {
            const params = new URLSearchParams(queryString);
            const result = {};
            for (const [key, value] of params) {
                result[key] = value;
            }
            return result;
        },

        /**
         * Build query string from object
         */
        buildQueryString: function (params) {
            return new URLSearchParams(params).toString();
        }
    };

    // ============================================
    // Format Functions
    // ============================================
    const Format = {
        /**
         * Format số thành tiền tệ VND
         */
        currency: function (amount, options = {}) {
            const defaultOptions = {
                locale: _config.locale,
                currency: _config.currency,
                minimumFractionDigits: 0,
                maximumFractionDigits: 0
            };
            const opts = { ...defaultOptions, ...options };

            try {
                return new Intl.NumberFormat(opts.locale, {
                    style: 'currency',
                    currency: opts.currency,
                    minimumFractionDigits: opts.minimumFractionDigits,
                    maximumFractionDigits: opts.maximumFractionDigits
                }).format(amount);
            } catch (e) {
                return amount.toLocaleString('vi-VN') + ' ₫';
            }
        },

        /**
         * Format số với dấu phân cách hàng nghìn
         */
        number: function (value, decimals = 0) {
            if (isNaN(value)) return '0';
            return new Intl.NumberFormat(_config.locale, {
                minimumFractionDigits: decimals,
                maximumFractionDigits: decimals
            }).format(value);
        },

        /**
         * Parse chuỗi tiền tệ thành số
         */
        parseCurrency: function (value) {
            if (typeof value === 'number') return value;
            if (!value) return 0;
            // Xóa tất cả ký tự không phải số và dấu chấm/phẩy
            const cleaned = value.toString().replace(/[^\d,.-]/g, '');
            // Xử lý dấu phẩy theo định dạng VN (dấu phẩy là phân cách hàng nghìn)
            const normalized = cleaned.replace(/\./g, '').replace(',', '.');
            return parseFloat(normalized) || 0;
        },

        /**
         * Format ngày tháng
         */
        date: function (date, format = 'short') {
            if (!date) return '';
            const d = new Date(date);
            if (isNaN(d.getTime())) return '';

            const options = {
                short: { day: '2-digit', month: '2-digit', year: 'numeric' },
                long: { day: '2-digit', month: 'long', year: 'numeric' },
                full: { weekday: 'long', day: '2-digit', month: 'long', year: 'numeric' },
                time: { hour: '2-digit', minute: '2-digit' },
                datetime: { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' }
            };

            return d.toLocaleDateString(_config.locale, options[format] || options.short);
        },

        /**
         * Format relative time (e.g., "2 giờ trước")
         */
        relativeTime: function (date) {
            const now = new Date();
            const d = new Date(date);
            const diffMs = now - d;
            const diffSecs = Math.floor(diffMs / 1000);
            const diffMins = Math.floor(diffSecs / 60);
            const diffHours = Math.floor(diffMins / 60);
            const diffDays = Math.floor(diffHours / 24);

            if (diffSecs < 60) return 'Vừa xong';
            if (diffMins < 60) return `${diffMins} phút trước`;
            if (diffHours < 24) return `${diffHours} giờ trước`;
            if (diffDays < 7) return `${diffDays} ngày trước`;
            return this.date(date, 'short');
        },

        /**
         * Format số điện thoại VN
         */
        phone: function (phone) {
            if (!phone) return '';
            const cleaned = phone.replace(/\D/g, '');
            if (cleaned.length === 10) {
                return cleaned.replace(/(\d{4})(\d{3})(\d{3})/, '$1 $2 $3');
            }
            return phone;
        },

        /**
         * Format file size
         */
        fileSize: function (bytes) {
            if (bytes === 0) return '0 Bytes';
            const k = 1024;
            const sizes = ['Bytes', 'KB', 'MB', 'GB'];
            const i = Math.floor(Math.log(bytes) / Math.log(k));
            return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
        }
    };

    // ============================================
    // UI Components
    // ============================================
    const UI = {
        /**
         * Initialize Bootstrap tooltips
         */
        initTooltips: function (container = document) {
            const tooltipTriggerList = container.querySelectorAll('[data-bs-toggle="tooltip"]');
            tooltipTriggerList.forEach(el => {
                new bootstrap.Tooltip(el);
            });
        },

        /**
         * Initialize Bootstrap popovers
         */
        initPopovers: function (container = document) {
            const popoverTriggerList = container.querySelectorAll('[data-bs-toggle="popover"]');
            popoverTriggerList.forEach(el => {
                new bootstrap.Popover(el);
            });
        },

        /**
         * Auto dismiss alerts
         */
        initAlertAutoDismiss: function () {
            const alerts = document.querySelectorAll('.alert-dismissible.auto-dismiss, .alert-success, .alert-info');
            alerts.forEach(alert => {
                if (alert.closest('.alert-messages-container')) {
                    setTimeout(() => {
                        const bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
                        if (bsAlert) {
                            bsAlert.close();
                        }
                    }, _config.alertAutoDismissDelay);
                }
            });
        },

        /**
         * Show toast notification
         */
        toast: function (message, type = 'info', options = {}) {
            const defaultOptions = {
                title: '',
                delay: 5000,
                autohide: true,
                position: 'top-end'
            };
            const opts = { ...defaultOptions, ...options };

            // Create toast container if not exists
            let container = document.querySelector('.toast-container');
            if (!container) {
                container = document.createElement('div');
                container.className = `toast-container position-fixed p-3 ${opts.position === 'top-end' ? 'top-0 end-0' : opts.position}`;
                container.style.zIndex = '9999';
                document.body.appendChild(container);
            }

            const icons = {
                success: 'bi-check-circle-fill text-success',
                error: 'bi-exclamation-triangle-fill text-danger',
                warning: 'bi-exclamation-circle-fill text-warning',
                info: 'bi-info-circle-fill text-info'
            };

            const toastId = Utils.generateId('toast');
            const toastHtml = `
                <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="toast-header">
                        <i class="bi ${icons[type] || icons.info} me-2"></i>
                        <strong class="me-auto">${opts.title || (type === 'success' ? 'Thành công' : type === 'error' ? 'Lỗi' : 'Thông báo')}</strong>
                        <small>Vừa xong</small>
                        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                    <div class="toast-body">
                        ${Utils.escapeHtml(message)}
                    </div>
                </div>
            `;

            container.insertAdjacentHTML('beforeend', toastHtml);
            const toastEl = document.getElementById(toastId);
            const toast = new bootstrap.Toast(toastEl, {
                delay: opts.delay,
                autohide: opts.autohide
            });
            toast.show();

            toastEl.addEventListener('hidden.bs.toast', () => {
                toastEl.remove();
            });

            return toast;
        },

        /**
         * Show loading overlay
         */
        showLoading: function (message = 'Đang xử lý...') {
            let overlay = document.querySelector('.loading-overlay');
            if (!overlay) {
                overlay = document.createElement('div');
                overlay.className = 'loading-overlay';
                overlay.innerHTML = `
                    <div class="loading-content">
                        <div class="loading-spinner"></div>
                        <div class="loading-text">${Utils.escapeHtml(message)}</div>
                    </div>
                `;
                document.body.appendChild(overlay);
            } else {
                overlay.querySelector('.loading-text').textContent = message;
            }

            // Force reflow
            overlay.offsetHeight;
            overlay.classList.add('show');
        },

        /**
         * Hide loading overlay
         */
        hideLoading: function () {
            const overlay = document.querySelector('.loading-overlay');
            if (overlay) {
                overlay.classList.remove('show');
            }
        },

        /**
         * Confirm dialog
         */
        confirm: function (message, options = {}) {
            return new Promise((resolve) => {
                const defaultOptions = {
                    title: 'Xác nhận',
                    confirmText: 'Đồng ý',
                    cancelText: 'Hủy',
                    confirmClass: 'btn-primary',
                    cancelClass: 'btn-secondary'
                };
                const opts = { ...defaultOptions, ...options };

                const modalId = Utils.generateId('confirm');
                const modalHtml = `
                    <div class="modal fade" id="${modalId}" tabindex="-1">
                        <div class="modal-dialog modal-dialog-centered">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">${Utils.escapeHtml(opts.title)}</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                                </div>
                                <div class="modal-body">
                                    <p class="mb-0">${Utils.escapeHtml(message)}</p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn ${opts.cancelClass}" data-bs-dismiss="modal">${opts.cancelText}</button>
                                    <button type="button" class="btn ${opts.confirmClass}" data-confirm="true">${opts.confirmText}</button>
                                </div>
                            </div>
                        </div>
                    </div>
                `;

                document.body.insertAdjacentHTML('beforeend', modalHtml);
                const modalEl = document.getElementById(modalId);
                const modal = new bootstrap.Modal(modalEl);

                modalEl.querySelector('[data-confirm="true"]').addEventListener('click', () => {
                    modal.hide();
                    resolve(true);
                });

                modalEl.addEventListener('hidden.bs.modal', () => {
                    modalEl.remove();
                    resolve(false);
                });

                modal.show();
            });
        },

        /**
         * Highlight search term in text
         */
        highlightText: function (text, searchTerm) {
            if (!searchTerm || !text) return text;
            const regex = new RegExp(`(${searchTerm.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')})`, 'gi');
            return text.replace(regex, '<mark class="highlight">$1</mark>');
        },

        /**
         * Scroll to element
         */
        scrollTo: function (element, options = {}) {
            const defaultOptions = {
                behavior: 'smooth',
                block: 'start',
                offset: 0
            };
            const opts = { ...defaultOptions, ...options };

            if (typeof element === 'string') {
                element = document.querySelector(element);
            }

            if (element) {
                const y = element.getBoundingClientRect().top + window.pageYOffset + opts.offset;
                window.scrollTo({ top: y, behavior: opts.behavior });
            }
        }
    };

    // ============================================
    // Sidebar Functions
    // ============================================
    const Sidebar = {
        toggle: function () {
            const sidebar = document.querySelector('.sidebar');
            const overlay = document.querySelector('.sidebar-overlay');

            if (sidebar) {
                sidebar.classList.toggle('show');
                if (overlay) {
                    overlay.classList.toggle('show');
                }
            }
        },

        close: function () {
            const sidebar = document.querySelector('.sidebar');
            const overlay = document.querySelector('.sidebar-overlay');

            if (sidebar) {
                sidebar.classList.remove('show');
                if (overlay) {
                    overlay.classList.remove('show');
                }
            }
        }
    };

    // ============================================
    // Form Helpers
    // ============================================
    const Form = {
        /**
         * Serialize form data to object
         */
        serialize: function (form) {
            const formData = new FormData(form);
            const data = {};
            for (const [key, value] of formData) {
                if (data[key]) {
                    if (!Array.isArray(data[key])) {
                        data[key] = [data[key]];
                    }
                    data[key].push(value);
                } else {
                    data[key] = value;
                }
            }
            return data;
        },

        /**
         * Reset form and clear validation
         */
        reset: function (form) {
            if (typeof form === 'string') {
                form = document.querySelector(form);
            }
            if (form) {
                form.reset();
                form.querySelectorAll('.is-invalid, .is-valid').forEach(el => {
                    el.classList.remove('is-invalid', 'is-valid');
                });
                form.querySelectorAll('.invalid-feedback, .valid-feedback').forEach(el => {
                    el.textContent = '';
                });
            }
        },

        /**
         * Set form values from object
         */
        populate: function (form, data) {
            if (typeof form === 'string') {
                form = document.querySelector(form);
            }
            if (!form || !data) return;

            Object.entries(data).forEach(([key, value]) => {
                const field = form.querySelector(`[name="${key}"]`);
                if (field) {
                    if (field.type === 'checkbox') {
                        field.checked = Boolean(value);
                    } else if (field.type === 'radio') {
                        const radio = form.querySelector(`[name="${key}"][value="${value}"]`);
                        if (radio) radio.checked = true;
                    } else {
                        field.value = value;
                    }
                }
            });
        },

        /**
         * Validate single field
         */
        validateField: function (field) {
            const value = field.value.trim();
            const rules = {
                required: field.hasAttribute('required'),
                minLength: field.getAttribute('minlength'),
                maxLength: field.getAttribute('maxlength'),
                pattern: field.getAttribute('pattern'),
                type: field.type
            };

            let isValid = true;
            let message = '';

            if (rules.required && !value) {
                isValid = false;
                message = 'Trường này là bắt buộc';
            } else if (value) {
                if (rules.minLength && value.length < parseInt(rules.minLength)) {
                    isValid = false;
                    message = `Tối thiểu ${rules.minLength} ký tự`;
                }
                if (rules.maxLength && value.length > parseInt(rules.maxLength)) {
                    isValid = false;
                    message = `Tối đa ${rules.maxLength} ký tự`;
                }
                if (rules.pattern && !new RegExp(rules.pattern).test(value)) {
                    isValid = false;
                    message = 'Định dạng không hợp lệ';
                }
                if (rules.type === 'email' && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
                    isValid = false;
                    message = 'Email không hợp lệ';
                }
            }

            // Update UI
            field.classList.toggle('is-invalid', !isValid);
            field.classList.toggle('is-valid', isValid && value);

            const feedback = field.parentElement.querySelector('.invalid-feedback');
            if (feedback) {
                feedback.textContent = message;
            }

            return isValid;
        }
    };

    // ============================================
    // AJAX Helpers
    // ============================================
    const Ajax = {
        /**
         * GET request
         */
        get: async function (url, options = {}) {
            return this.request(url, { ...options, method: 'GET' });
        },

        /**
         * POST request
         */
        post: async function (url, data, options = {}) {
            return this.request(url, {
                ...options,
                method: 'POST',
                body: JSON.stringify(data),
                headers: {
                    'Content-Type': 'application/json',
                    ...options.headers
                }
            });
        },

        /**
         * Base request function
         */
        request: async function (url, options = {}) {
            const defaultOptions = {
                method: 'GET',
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                },
                credentials: 'same-origin'
            };

            // Add anti-forgery token if exists
            const token = document.querySelector('input[name="__RequestVerificationToken"]');
            if (token) {
                defaultOptions.headers['RequestVerificationToken'] = token.value;
            }

            const opts = { ...defaultOptions, ...options };
            opts.headers = { ...defaultOptions.headers, ...options.headers };

            try {
                const response = await fetch(url, opts);

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                const contentType = response.headers.get('content-type');
                if (contentType && contentType.includes('application/json')) {
                    return await response.json();
                }
                return await response.text();
            } catch (error) {
                console.error('Ajax request failed:', error);
                throw error;
            }
        }
    };

    // ============================================
    // Initialization
    // ============================================
    function init() {
        if (_isInitialized) return;

        // Initialize UI components
        UI.initTooltips();
        UI.initPopovers();
        UI.initAlertAutoDismiss();

        // Initialize sidebar toggle
        window.toggleSidebar = Sidebar.toggle;

        // Close sidebar on overlay click
        const overlay = document.querySelector('.sidebar-overlay');
        if (overlay) {
            overlay.addEventListener('click', Sidebar.close);
        }

        // Close sidebar on ESC key
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape') {
                Sidebar.close();
            }
        });

        // Auto-format currency inputs
        document.querySelectorAll('input[data-format="currency"]').forEach(input => {
            input.addEventListener('blur', function () {
                const value = Format.parseCurrency(this.value);
                if (value > 0) {
                    this.value = Format.number(value);
                }
            });
            input.addEventListener('focus', function () {
                const value = Format.parseCurrency(this.value);
                if (value > 0) {
                    this.value = value;
                }
            });
        });

        // Form validation on submit
        document.querySelectorAll('form[data-validate="true"]').forEach(form => {
            form.addEventListener('submit', function (e) {
                let isValid = true;
                this.querySelectorAll('input, select, textarea').forEach(field => {
                    if (!Form.validateField(field)) {
                        isValid = false;
                    }
                });
                if (!isValid) {
                    e.preventDefault();
                    e.stopPropagation();
                }
            });
        });

        // Real-time validation
        document.querySelectorAll('form[data-validate="true"] input, form[data-validate="true"] select, form[data-validate="true"] textarea').forEach(field => {
            field.addEventListener('blur', function () {
                Form.validateField(this);
            });
        });

        _isInitialized = true;
        console.log('FarmApp initialized successfully');
    }

    // Auto-init on DOM ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

    // ============================================
    // Public API
    // ============================================
    return {
        // Configuration
        config: _config,

        // Utilities
        Utils: Utils,

        // Formatting
        Format: Format,

        // UI Components
        UI: UI,

        // Sidebar
        Sidebar: Sidebar,

        // Form Helpers
        Form: Form,

        // AJAX
        Ajax: Ajax,

        // Manual initialization
        init: init,

        // Version
        version: '1.0.0'
    };
})();

// Export for module systems
if (typeof module !== 'undefined' && module.exports) {
    module.exports = FarmApp;
}