/**
 * FarmManagement - DataTable Module
 * Client-side table search, sort, filter and pagination
 * * Usage:
 * FarmApp.DataTable.init('#myTable', { searchInput: '#searchInput' });
 */

(function (FarmApp) {
    'use strict';

    const DataTable = {
        // Store instances
        _instances: new Map(),

        // Default options
        _defaultOptions: {
            searchInput: null,           // Selector for search input
            searchDelay: 300,            // Debounce delay for search
            searchMinLength: 1,          // Minimum characters to trigger search
            searchableColumns: [],       // Array of column indexes to search (empty = all)
            highlightMatches: true,      // Highlight search matches
            noResultsMessage: 'Không tìm thấy kết quả phù hợp',
            sortable: true,              // Enable column sorting
            sortIcons: {
                asc: 'bi-sort-up',
                desc: 'bi-sort-down',
                default: 'bi-sort'
            },
            pagination: false,           // Enable pagination
            pageSize: 10,                // Rows per page
            pageSizeOptions: [10, 25, 50, 100],
            onSearch: null,              // Callback: function(searchTerm, visibleRows)
            onSort: null,                // Callback: function(column, direction)
            onPageChange: null,          // Callback: function(page, pageSize)
            preserveState: false,        // Preserve state in URL/localStorage
            storageKey: null             // Key for localStorage
        },

        /**
         * Initialize DataTable on an element
         * @param {string|HTMLElement} table - Table selector or element
         * @param {Object} options - Configuration options
         */
        init: function (table, options = {}) {
            const tableEl = typeof table === 'string' ? document.querySelector(table) : table;

            if (!tableEl) {
                console.warn('DataTable: Table element not found');
                return null;
            }

            const opts = { ...this._defaultOptions, ...options };
            const instance = new DataTableInstance(tableEl, opts);

            this._instances.set(tableEl, instance);
            return instance;
        },

        /**
         * Get instance by table element
         */
        getInstance: function (table) {
            const tableEl = typeof table === 'string' ? document.querySelector(table) : table;
            return this._instances.get(tableEl);
        },

        /**
         * Destroy instance
         */
        destroy: function (table) {
            const instance = this.getInstance(table);
            if (instance) {
                instance.destroy();
                const tableEl = typeof table === 'string' ? document.querySelector(table) : table;
                this._instances.delete(tableEl);
            }
        },

        /**
         * Quick search initialization for simple use cases
         */
        quickSearch: function (tableSelector, inputSelector) {
            return this.init(tableSelector, {
                searchInput: inputSelector,
                sortable: false,
                pagination: false
            });
        }
    };

    /**
     * DataTable Instance Class
     */
    class DataTableInstance {
        constructor(table, options) {
            this.table = table;
            this.options = options;
            this.tbody = table.querySelector('tbody');
            this.thead = table.querySelector('thead');
            this.rows = [];
            this.filteredRows = [];
            this.currentPage = 1;
            this.sortColumn = null;
            this.sortDirection = 'asc';
            this.searchTerm = '';

            this._init();
        }

        _init() {
            // Store original rows
            this._storeRows();

            // Initialize search
            if (this.options.searchInput) {
                this._initSearch();
            }

            // Initialize sorting
            if (this.options.sortable) {
                this._initSort();
            }

            // Initialize pagination
            if (this.options.pagination) {
                this._initPagination();
            }

            // Restore state if enabled
            if (this.options.preserveState) {
                this._restoreState();
            }

            // Add table class
            this.table.classList.add('datatable-initialized');
        }

        _storeRows() {
            this.rows = Array.from(this.tbody.querySelectorAll('tr')).map((row, index) => ({
                element: row,
                originalIndex: index,
                cells: Array.from(row.querySelectorAll('td')).map(cell => ({
                    element: cell,
                    text: cell.textContent.trim().toLowerCase(),
                    html: cell.innerHTML
                })),
                visible: true
            }));
            this.filteredRows = [...this.rows];
        }

        _initSearch() {
            const input = typeof this.options.searchInput === 'string'
                ? document.querySelector(this.options.searchInput)
                : this.options.searchInput;

            if (!input) return;

            // Create debounced search function
            const debouncedSearch = FarmApp.Utils.debounce((value) => {
                this.search(value);
            }, this.options.searchDelay);

            // Listen to input events
            input.addEventListener('input', (e) => {
                debouncedSearch(e.target.value);
            });

            // Listen to clear button if exists
            const clearBtn = input.parentElement?.querySelector('[data-clear-search]');
            if (clearBtn) {
                clearBtn.addEventListener('click', () => {
                    input.value = '';
                    this.search('');
                });
            }

            this.searchInput = input;
        }

        _initSort() {
            if (!this.thead) return;

            const headers = this.thead.querySelectorAll('th[data-sortable="true"], th:not([data-sortable="false"])');

            headers.forEach((th, index) => {
                // Skip if explicitly disabled
                if (th.dataset.sortable === 'false') return;

                th.style.cursor = 'pointer';
                th.classList.add('sortable');

                // Add sort icon
                const icon = document.createElement('i');
                icon.className = `bi ${this.options.sortIcons.default} ms-1 sort-icon`;
                th.appendChild(icon);

                // Click handler
                th.addEventListener('click', () => {
                    this.sort(index);
                });
            });
        }

        _initPagination() {
            // Create pagination container
            const paginationContainer = document.createElement('div');
            paginationContainer.className = 'datatable-pagination d-flex justify-content-between align-items-center p-3 border-top';

            paginationContainer.innerHTML = `
                <div class="datatable-info">
                    <span class="datatable-info-text"></span>
                </div>
                <div class="d-flex align-items-center gap-3">
                    <div class="datatable-page-size">
                        <select class="form-select form-select-sm" style="width: auto;">
                            ${this.options.pageSizeOptions.map(size =>
                `<option value="${size}" ${size === this.options.pageSize ? 'selected' : ''}>${size}</option>`
            ).join('')}
                        </select>
                    </div>
                    <nav class="datatable-pages">
                        <ul class="pagination pagination-sm mb-0"></ul>
                    </nav>
                </div>
            `;

            this.table.parentElement.appendChild(paginationContainer);
            this.paginationContainer = paginationContainer;

            // Page size change
            const pageSelect = paginationContainer.querySelector('select');
            pageSelect.addEventListener('change', (e) => {
                this.options.pageSize = parseInt(e.target.value);
                this.currentPage = 1;
                this._renderPagination();
                this._renderRows();
            });

            this._renderPagination();
        }

        /**
         * Search table rows
         * @param {string} term - Search term
         */
        search(term) {
            this.searchTerm = term.toLowerCase().trim();
            this.currentPage = 1;

            if (this.searchTerm.length < this.options.searchMinLength) {
                // Show all rows
                this.filteredRows = [...this.rows];
                this.filteredRows.forEach(row => row.visible = true);
            } else {
                // Filter rows
                this.filteredRows = this.rows.filter(row => {
                    const searchableCells = this.options.searchableColumns.length > 0
                        ? row.cells.filter((_, i) => this.options.searchableColumns.includes(i))
                        : row.cells;

                    const matches = searchableCells.some(cell =>
                        cell.text.includes(this.searchTerm)
                    );
                    row.visible = matches;
                    return matches;
                });
            }

            this._renderRows();

            // Callback
            if (typeof this.options.onSearch === 'function') {
                this.options.onSearch(this.searchTerm, this.filteredRows.length);
            }

            // Update URL if preserveState
            if (this.options.preserveState) {
                this._saveState();
            }
        }

        /**
         * Sort table by column
         * @param {number} columnIndex - Column index to sort
         * @param {string} direction - 'asc' or 'desc' (optional, toggles if same column)
         */
        sort(columnIndex, direction = null) {
            // Determine sort direction
            if (this.sortColumn === columnIndex && !direction) {
                this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
            } else {
                this.sortDirection = direction || 'asc';
            }
            this.sortColumn = columnIndex;

            // Sort filtered rows
            this.filteredRows.sort((a, b) => {
                const aValue = a.cells[columnIndex]?.text || '';
                const bValue = b.cells[columnIndex]?.text || '';

                // Try numeric sort
                const aNum = parseFloat(aValue.replace(/[^\d.-]/g, ''));
                const bNum = parseFloat(bValue.replace(/[^\d.-]/g, ''));

                let comparison;
                if (!isNaN(aNum) && !isNaN(bNum)) {
                    comparison = aNum - bNum;
                } else {
                    comparison = aValue.localeCompare(bValue, 'vi');
                }

                return this.sortDirection === 'asc' ? comparison : -comparison;
            });

            // Update sort icons
            this._updateSortIcons();

            // Re-render
            this._renderRows();

            // Callback
            if (typeof this.options.onSort === 'function') {
                this.options.onSort(columnIndex, this.sortDirection);
            }
        }

        /**
         * Go to specific page
         * @param {number} page - Page number
         */
        goToPage(page) {
            const totalPages = Math.ceil(this.filteredRows.length / this.options.pageSize);
            this.currentPage = Math.max(1, Math.min(page, totalPages));
            this._renderRows();
            this._renderPagination();

            if (typeof this.options.onPageChange === 'function') {
                this.options.onPageChange(this.currentPage, this.options.pageSize);
            }
        }

        /**
         * Refresh table data
         */
        refresh() {
            this._storeRows();
            this.search(this.searchTerm);
        }

        /**
         * Get visible rows
         */
        getVisibleRows() {
            return this.filteredRows.filter(row => row.visible);
        }

        /**
         * Get selected rows (if selection enabled)
         */
        getSelectedRows() {
            return this.filteredRows.filter(row =>
                row.element.classList.contains('selected') ||
                row.element.querySelector('input[type="checkbox"]:checked')
            );
        }

        _renderRows() {
            // Hide all rows first
            this.rows.forEach(row => {
                row.element.style.display = 'none';
            });

            const visibleRows = this.filteredRows.filter(r => r.visible);

            // Apply pagination
            let rowsToShow = visibleRows;
            if (this.options.pagination) {
                const start = (this.currentPage - 1) * this.options.pageSize;
                const end = start + this.options.pageSize;
                rowsToShow = visibleRows.slice(start, end);
            }

            // Show filtered rows
            rowsToShow.forEach(row => {
                row.element.style.display = '';

                // Highlight matches if enabled
                if (this.options.highlightMatches && this.searchTerm) {
                    this._highlightRow(row);
                } else {
                    this._clearHighlight(row);
                }
            });

            // Show no results message if empty
            this._toggleNoResults(rowsToShow.length === 0);

            // Update pagination info
            if (this.options.pagination) {
                this._updatePaginationInfo();
            }
        }

        _highlightRow(row) {
            row.cells.forEach((cell, index) => {
                // Skip columns not in searchable list
                if (this.options.searchableColumns.length > 0 &&
                    !this.options.searchableColumns.includes(index)) {
                    return;
                }

                if (cell.text.includes(this.searchTerm)) {
                    const regex = new RegExp(`(${this.searchTerm.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')})`, 'gi');
                    cell.element.innerHTML = cell.html.replace(regex, '<mark class="highlight">$1</mark>');
                }
            });
        }

        _clearHighlight(row) {
            row.cells.forEach(cell => {
                cell.element.innerHTML = cell.html;
            });
        }

        _updateSortIcons() {
            const headers = this.thead.querySelectorAll('th');
            headers.forEach((th, index) => {
                const icon = th.querySelector('.sort-icon');
                if (!icon) return;

                if (index === this.sortColumn) {
                    icon.className = `bi ${this.sortDirection === 'asc'
                        ? this.options.sortIcons.asc
                        : this.options.sortIcons.desc} ms-1 sort-icon`;
                } else {
                    icon.className = `bi ${this.options.sortIcons.default} ms-1 sort-icon`;
                }
            });
        }

        _toggleNoResults(show) {
            let noResultsRow = this.tbody.querySelector('.datatable-no-results');

            if (show) {
                if (!noResultsRow) {
                    const colCount = this.thead?.querySelectorAll('th').length || 1;
                    noResultsRow = document.createElement('tr');
                    noResultsRow.className = 'datatable-no-results';
                    noResultsRow.innerHTML = `
                        <td colspan="${colCount}" class="text-center text-muted py-4">
                            <i class="bi bi-search me-2"></i>
                            ${this.options.noResultsMessage}
                        </td>
                    `;
                    this.tbody.appendChild(noResultsRow);
                }
            } else if (noResultsRow) {
                noResultsRow.remove();
            }
        }

        _renderPagination() {
            if (!this.paginationContainer) return;

            const totalRows = this.filteredRows.filter(r => r.visible).length;
            const totalPages = Math.ceil(totalRows / this.options.pageSize);
            const pagesContainer = this.paginationContainer.querySelector('.pagination');

            let html = '';

            // Previous button
            html += `
                <li class="page-item ${this.currentPage === 1 ? 'disabled' : ''}">
                    <a class="page-link" href="#" data-page="${this.currentPage - 1}">
                        <i class="bi bi-chevron-left"></i>
                    </a>
                </li>
            `;

            // Page numbers
            const maxVisiblePages = 5;
            let startPage = Math.max(1, this.currentPage - Math.floor(maxVisiblePages / 2));
            let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

            if (endPage - startPage < maxVisiblePages - 1) {
                startPage = Math.max(1, endPage - maxVisiblePages + 1);
            }

            for (let i = startPage; i <= endPage; i++) {
                html += `
                    <li class="page-item ${i === this.currentPage ? 'active' : ''}">
                        <a class="page-link" href="#" data-page="${i}">${i}</a>
                    </li>
                `;
            }

            // Next button
            html += `
                <li class="page-item ${this.currentPage === totalPages ? 'disabled' : ''}">
                    <a class="page-link" href="#" data-page="${this.currentPage + 1}">
                        <i class="bi bi-chevron-right"></i>
                    </a>
                </li>
            `;

            pagesContainer.innerHTML = html;

            // Add click handlers
            pagesContainer.querySelectorAll('.page-link').forEach(link => {
                link.addEventListener('click', (e) => {
                    e.preventDefault();
                    const page = parseInt(link.dataset.page);
                    if (!isNaN(page)) {
                        this.goToPage(page);
                    }
                });
            });
        }

        _updatePaginationInfo() {
            if (!this.paginationContainer) return;

            const totalRows = this.filteredRows.filter(r => r.visible).length;
            const start = totalRows === 0 ? 0 : (this.currentPage - 1) * this.options.pageSize + 1;
            const end = Math.min(this.currentPage * this.options.pageSize, totalRows);

            const infoText = this.paginationContainer.querySelector('.datatable-info-text');
            if (infoText) {
                infoText.textContent = `Hiển thị ${start} - ${end} trong ${totalRows} bản ghi`;
            }
        }

        _saveState() {
            const state = {
                search: this.searchTerm,
                page: this.currentPage,
                sortColumn: this.sortColumn,
                sortDirection: this.sortDirection
            };

            if (this.options.storageKey) {
                localStorage.setItem(this.options.storageKey, JSON.stringify(state));
            } else {
                // Update URL params
                const params = new URLSearchParams(window.location.search);
                if (this.searchTerm) {
                    params.set('search', this.searchTerm);
                } else {
                    params.delete('search');
                }
                window.history.replaceState({}, '', `${window.location.pathname}?${params}`);
            }
        }

        _restoreState() {
            let state = null;

            if (this.options.storageKey) {
                const saved = localStorage.getItem(this.options.storageKey);
                if (saved) {
                    state = JSON.parse(saved);
                }
            } else {
                const params = new URLSearchParams(window.location.search);
                state = {
                    search: params.get('search') || ''
                };
            }

            if (state) {
                if (state.search) {
                    this.searchTerm = state.search;
                    if (this.searchInput) {
                        this.searchInput.value = state.search;
                    }
                    this.search(state.search);
                }
                if (state.sortColumn !== null && state.sortColumn !== undefined) {
                    this.sort(state.sortColumn, state.sortDirection);
                }
                if (state.page) {
                    this.goToPage(state.page);
                }
            }
        }

        destroy() {
            // Remove event listeners
            this.table.classList.remove('datatable-initialized');

            // Remove pagination
            if (this.paginationContainer) {
                this.paginationContainer.remove();
            }

            // Remove sort icons
            if (this.thead) {
                this.thead.querySelectorAll('.sort-icon').forEach(icon => icon.remove());
            }

            // Clear highlights
            this.rows.forEach(row => this._clearHighlight(row));

            // Show all rows
            this.rows.forEach(row => {
                row.element.style.display = '';
            });
        }
    }

    // Add to FarmApp namespace
    FarmApp.DataTable = DataTable;

})(window.FarmApp || (window.FarmApp = {}));