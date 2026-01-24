/**
 * FarmManagement - Form Helper Module
 * Xử lý tính toán số lượng/tổng tiền và validate form
 * * Usage:
 * FarmApp.FormHelper.initCalculator('#quantity', '#price', '#total');
 * FarmApp.FormHelper.initLivestockGenderValidation('#form', '#typeSelect', '#genderSelect');
 */

(function (FarmApp) {
    'use strict';

    const FormHelper = {
        // ============================================
        // Price Calculator
        // ============================================

        /**
         * Initialize price/total calculator
         * @param {Object} options - Configuration
         */
        initCalculator: function (options = {}) {
            const defaults = {
                quantityInput: null,    // Selector or element for quantity input
                priceInput: null,       // Selector or element for unit price input
                totalInput: null,       // Selector or element for total display
                weightInput: null,      // Optional: weight input
                autoFormat: true,       // Auto format currency
                decimalPlaces: 0,       // Decimal places for total
                onCalculate: null       // Callback: function(quantity, price, total, weight)
            };

            const opts = { ...defaults, ...options };
            const calculator = new PriceCalculator(opts);
            return calculator;
        },

        /**
         * Quick calculator setup
         */
        quickCalculator: function (quantitySelector, priceSelector, totalSelector) {
            return this.initCalculator({
                quantityInput: quantitySelector,
                priceInput: priceSelector,
                totalInput: totalSelector
            });
        },

        // ============================================
        // Livestock Gender Validation
        // ============================================

        /**
         * Livestock types and their allowed genders
         * Can be extended/modified based on business rules
         */
        _livestockGenderRules: {
            // Default rules - can be overridden
            // Format: typeId/typeName: [allowedGenders]
            // 'all' means all genders allowed
            defaults: {
                'gà': ['đực', 'mái', 'không xác định'],
                'vịt': ['đực', 'mái', 'không xác định'],
                'heo': ['đực', 'nái', 'heo thịt'],
                'bò': ['đực', 'cái', 'bê'],
                'dê': ['đực', 'cái'],
                'cừu': ['đực', 'cái'],
                'thỏ': ['đực', 'cái'],
                'cá': ['không xác định'],
                'tôm': ['không xác định'],
                'ong': ['ong chúa', 'ong thợ', 'ong đực']
            }
        },

        /**
         * Initialize livestock gender validation
         * @param {Object} options - Configuration
         */
        initLivestockGenderValidation: function (options = {}) {
            const defaults = {
                form: null,             // Form selector or element
                typeSelect: null,       // Livestock type select
                genderSelect: null,     // Gender select
                rules: null,            // Custom rules (optional)
                onTypeChange: null,     // Callback when type changes
                onValidationError: null,// Callback on validation error
                errorMessage: 'Giới tính không phù hợp với loại vật nuôi đã chọn'
            };

            const opts = { ...defaults, ...options };
            const validator = new LivestockGenderValidator(opts);
            return validator;
        },

        /**
         * Set custom gender rules
         * @param {Object} rules - { typeName: [allowedGenders] }
         */
        setGenderRules: function (rules) {
            this._livestockGenderRules = { ...this._livestockGenderRules, ...rules };
        },

        // ============================================
        // Form Calculations
        // ============================================

        /**
         * Calculate total from multiple line items
         * @param {string} containerSelector - Container with line items
         * @param {Object} options - Configuration
         */
        initLineItemsCalculator: function (containerSelector, options = {}) {
            const defaults = {
                itemSelector: '.line-item',
                quantitySelector: '.quantity',
                priceSelector: '.price',
                itemTotalSelector: '.item-total',
                grandTotalSelector: '.grand-total',
                onItemChange: null,
                onTotalChange: null
            };

            const opts = { ...defaults, ...options };
            const container = document.querySelector(containerSelector);

            if (!container) {
                console.warn('FormHelper: Container not found');
                return null;
            }

            const calculate = () => {
                let grandTotal = 0;
                const items = container.querySelectorAll(opts.itemSelector);

                items.forEach((item, index) => {
                    const quantityEl = item.querySelector(opts.quantitySelector);
                    const priceEl = item.querySelector(opts.priceSelector);
                    const itemTotalEl = item.querySelector(opts.itemTotalSelector);

                    const quantity = parseFloat(quantityEl?.value) || 0;
                    const price = FarmApp.Format.parseCurrency(priceEl?.value) || 0;
                    const itemTotal = quantity * price;

                    if (itemTotalEl) {
                        itemTotalEl.value = FarmApp.Format.number(itemTotal);
                    }

                    grandTotal += itemTotal;

                    if (typeof opts.onItemChange === 'function') {
                        opts.onItemChange(index, { quantity, price, itemTotal });
                    }
                });

                const grandTotalEl = document.querySelector(opts.grandTotalSelector);
                if (grandTotalEl) {
                    grandTotalEl.value = FarmApp.Format.number(grandTotal);
                    grandTotalEl.textContent = FarmApp.Format.currency(grandTotal);
                }

                if (typeof opts.onTotalChange === 'function') {
                    opts.onTotalChange(grandTotal);
                }

                return grandTotal;
            };

            // Attach event listeners
            container.querySelectorAll(`${opts.quantitySelector}, ${opts.priceSelector}`).forEach(input => {
                input.addEventListener('input', calculate);
                input.addEventListener('change', calculate);
            });

            // Initial calculation
            calculate();

            // Return controller
            return {
                calculate,
                getTotal: () => {
                    const el = document.querySelector(opts.grandTotalSelector);
                    return el ? FarmApp.Format.parseCurrency(el.value || el.textContent) : 0;
                }
            };
        },

        // ============================================
        // Form Validation Helpers
        // ============================================

        /**
         * Validate numeric range
         */
        validateRange: function (value, min, max, options = {}) {
            const num = parseFloat(value);
            const result = { valid: true, message: '' };

            if (isNaN(num)) {
                result.valid = false;
                result.message = options.nanMessage || 'Vui lòng nhập số hợp lệ';
            } else if (min !== null && num < min) {
                result.valid = false;
                result.message = options.minMessage || `Giá trị tối thiểu là ${min}`;
            } else if (max !== null && num > max) {
                result.valid = false;
                result.message = options.maxMessage || `Giá trị tối đa là ${max}`;
            }

            return result;
        },

        /**
         * Validate date range
         */
        validateDateRange: function (startDate, endDate, options = {}) {
            const start = new Date(startDate);
            const end = new Date(endDate);
            const result = { valid: true, message: '' };

            if (isNaN(start.getTime())) {
                result.valid = false;
                result.message = options.invalidStartMessage || 'Ngày bắt đầu không hợp lệ';
            } else if (isNaN(end.getTime())) {
                result.valid = false;
                result.message = options.invalidEndMessage || 'Ngày kết thúc không hợp lệ';
            } else if (start > end) {
                result.valid = false;
                result.message = options.rangeMessage || 'Ngày bắt đầu phải trước ngày kết thúc';
            }

            return result;
        },

        /**
         * Validate Vietnamese phone number
         */
        validatePhone: function (phone) {
            const cleaned = phone.replace(/\D/g, '');
            const regex = /^(0|84)(3|5|7|8|9)[0-9]{8}$/;
            return {
                valid: regex.test(cleaned),
                message: regex.test(cleaned) ? '' : 'Số điện thoại không hợp lệ'
            };
        },

        /**
         * Auto-format input as user types
         */
        autoFormatInput: function (inputSelector, formatType) {
            const input = typeof inputSelector === 'string'
                ? document.querySelector(inputSelector)
                : inputSelector;

            if (!input) return;

            const formatters = {
                currency: (value) => {
                    const num = FarmApp.Format.parseCurrency(value);
                    return num > 0 ? FarmApp.Format.number(num) : '';
                },
                phone: (value) => {
                    const cleaned = value.replace(/\D/g, '');
                    if (cleaned.length <= 4) return cleaned;
                    if (cleaned.length <= 7) return `${cleaned.slice(0, 4)} ${cleaned.slice(4)}`;
                    return `${cleaned.slice(0, 4)} ${cleaned.slice(4, 7)} ${cleaned.slice(7, 10)}`;
                },
                number: (value) => {
                    const num = parseFloat(value.replace(/[^\d.-]/g, ''));
                    return isNaN(num) ? '' : FarmApp.Format.number(num);
                }
            };

            const formatter = formatters[formatType];
            if (!formatter) return;

            input.addEventListener('blur', function () {
                this.value = formatter(this.value);
            });
        }
    };

    /**
     * Price Calculator Class
     */
    class PriceCalculator {
        constructor(options) {
            this.options = options;
            this.quantityInput = this._getElement(options.quantityInput);
            this.priceInput = this._getElement(options.priceInput);
            this.totalInput = this._getElement(options.totalInput);
            this.weightInput = options.weightInput ? this._getElement(options.weightInput) : null;

            this._init();
        }

        _getElement(selector) {
            return typeof selector === 'string' ? document.querySelector(selector) : selector;
        }

        _init() {
            if (!this.quantityInput || !this.priceInput) {
                console.warn('FormHelper.Calculator: Required inputs not found');
                return;
            }

            // Attach event listeners
            [this.quantityInput, this.priceInput, this.weightInput].filter(Boolean).forEach(input => {
                input.addEventListener('input', () => this.calculate());
                input.addEventListener('change', () => this.calculate());
            });

            // Initial calculation
            this.calculate();
        }

        calculate() {
            const quantity = parseFloat(this.quantityInput.value) || 0;
            const price = FarmApp.Format.parseCurrency(this.priceInput.value) || 0;
            const weight = this.weightInput ? (parseFloat(this.weightInput.value) || 0) : 1;

            // Calculate total (quantity * price * weight if weight exists)
            const total = quantity * price * (this.weightInput ? weight : 1);

            // Update total display
            if (this.totalInput) {
                if (this.totalInput.tagName === 'INPUT') {
                    this.totalInput.value = this.options.autoFormat
                        ? FarmApp.Format.number(total, this.options.decimalPlaces)
                        : total;
                } else {
                    this.totalInput.textContent = this.options.autoFormat
                        ? FarmApp.Format.currency(total)
                        : total;
                }
            }

            // Callback
            if (typeof this.options.onCalculate === 'function') {
                this.options.onCalculate({
                    quantity,
                    price,
                    weight,
                    total
                });
            }

            return total;
        }

        getValues() {
            return {
                quantity: parseFloat(this.quantityInput.value) || 0,
                price: FarmApp.Format.parseCurrency(this.priceInput.value) || 0,
                weight: this.weightInput ? (parseFloat(this.weightInput.value) || 0) : null,
                total: this.calculate()
            };
        }

        setValues(values) {
            if (values.quantity !== undefined) this.quantityInput.value = values.quantity;
            if (values.price !== undefined) this.priceInput.value = FarmApp.Format.number(values.price);
            if (values.weight !== undefined && this.weightInput) this.weightInput.value = values.weight;
            this.calculate();
        }

        reset() {
            this.quantityInput.value = '';
            this.priceInput.value = '';
            if (this.weightInput) this.weightInput.value = '';
            if (this.totalInput) {
                this.totalInput.value = '';
                this.totalInput.textContent = '';
            }
        }
    }

    /**
     * Livestock Gender Validator Class
     */
    class LivestockGenderValidator {
        constructor(options) {
            this.options = options;
            this.form = this._getElement(options.form);
            this.typeSelect = this._getElement(options.typeSelect);
            this.genderSelect = this._getElement(options.genderSelect);
            this.rules = options.rules || FormHelper._livestockGenderRules.defaults;

            this._init();
        }

        _getElement(selector) {
            return typeof selector === 'string' ? document.querySelector(selector) : selector;
        }

        _init() {
            if (!this.typeSelect || !this.genderSelect) {
                console.warn('FormHelper.LivestockGenderValidator: Required selects not found');
                return;
            }

            // Listen to type changes
            this.typeSelect.addEventListener('change', () => this._onTypeChange());

            // Validate on form submit
            if (this.form) {
                this.form.addEventListener('submit', (e) => {
                    if (!this.validate()) {
                        e.preventDefault();
                        e.stopPropagation();
                    }
                });
            }

            // Validate on gender change
            this.genderSelect.addEventListener('change', () => this.validate());

            // Initial update
            this._onTypeChange();
        }

        _onTypeChange() {
            const selectedType = this.typeSelect.options[this.typeSelect.selectedIndex];
            const typeName = (selectedType?.text || '').toLowerCase();
            const typeId = selectedType?.value;

            // Find matching rules
            let allowedGenders = this._findAllowedGenders(typeName, typeId);

            // Update gender select options
            this._updateGenderOptions(allowedGenders);

            // Callback
            if (typeof this.options.onTypeChange === 'function') {
                this.options.onTypeChange(typeId, typeName, allowedGenders);
            }

            // Validate current selection
            this.validate();
        }

        _findAllowedGenders(typeName, typeId) {
            // Check by ID first
            if (this.rules[typeId]) {
                return this.rules[typeId];
            }

            // Check by name (partial match)
            for (const [key, genders] of Object.entries(this.rules)) {
                if (typeName.includes(key.toLowerCase()) || key.toLowerCase().includes(typeName)) {
                    return genders;
                }
            }

            // Default: allow all
            return null;
        }

        _updateGenderOptions(allowedGenders) {
            if (!allowedGenders) {
                // Show all options
                Array.from(this.genderSelect.options).forEach(option => {
                    option.hidden = false;
                    option.disabled = false;
                });
                return;
            }

            // Hide/disable non-matching options
            Array.from(this.genderSelect.options).forEach(option => {
                const genderText = option.text.toLowerCase();
                const isAllowed = allowedGenders.some(g =>
                    genderText.includes(g.toLowerCase()) || g.toLowerCase().includes(genderText)
                );

                option.hidden = !isAllowed && option.value !== '';
                option.disabled = !isAllowed && option.value !== '';
            });

            // If current selection is invalid, reset to first valid option
            const currentOption = this.genderSelect.options[this.genderSelect.selectedIndex];
            if (currentOption && currentOption.disabled) {
                const firstValidOption = Array.from(this.genderSelect.options)
                    .find(opt => !opt.disabled && opt.value !== '');
                if (firstValidOption) {
                    this.genderSelect.value = firstValidOption.value;
                }
            }
        }

        validate() {
            const selectedType = this.typeSelect.options[this.typeSelect.selectedIndex];
            const selectedGender = this.genderSelect.options[this.genderSelect.selectedIndex];

            if (!selectedType?.value || !selectedGender?.value) {
                // Not fully selected yet
                this._clearError();
                return true;
            }

            const typeName = selectedType.text.toLowerCase();
            const genderText = selectedGender.text.toLowerCase();
            const allowedGenders = this._findAllowedGenders(typeName, selectedType.value);

            if (!allowedGenders) {
                // No rules, allow all
                this._clearError();
                return true;
            }

            const isValid = allowedGenders.some(g =>
                genderText.includes(g.toLowerCase()) || g.toLowerCase().includes(genderText)
            );

            if (!isValid) {
                this._showError();

                if (typeof this.options.onValidationError === 'function') {
                    this.options.onValidationError(selectedType.value, selectedGender.value);
                }
            } else {
                this._clearError();
            }

            return isValid;
        }

        _showError() {
            this.genderSelect.classList.add('is-invalid');

            // Add or update error message
            let feedback = this.genderSelect.parentElement.querySelector('.invalid-feedback');
            if (!feedback) {
                feedback = document.createElement('div');
                feedback.className = 'invalid-feedback';
                this.genderSelect.parentElement.appendChild(feedback);
            }
            feedback.textContent = this.options.errorMessage;
        }

        _clearError() {
            this.genderSelect.classList.remove('is-invalid');
            const feedback = this.genderSelect.parentElement.querySelector('.invalid-feedback');
            if (feedback) {
                feedback.textContent = '';
            }
        }

        /**
         * Set custom rules
         */
        setRules(rules) {
            this.rules = { ...this.rules, ...rules };
            this._onTypeChange();
        }

        /**
         * Get current validation state
         */
        getState() {
            return {
                type: this.typeSelect.value,
                gender: this.genderSelect.value,
                isValid: this.validate()
            };
        }
    }

    // Add to FarmApp namespace
    FarmApp.FormHelper = FormHelper;

})(window.FarmApp || (window.FarmApp = {}));