(function (window, document) {
    'use strict';

    // CropCost report client helpers
    var CropCostReport = {
        // Initialize: attach handlers to detail links
        init: function (selector) {
            selector = selector || 'a.details-link';
            document.querySelectorAll(selector).forEach(function (link) {
                link.addEventListener('click', onDetailsClick);
            });
        }
    };

    // Build the details URL preserving group keys and adding paging params
    function buildDetailsUrl(params) {
        var parts = [];
        parts.push('handler=Details');
        if (params.timePeriodKey) parts.push('timePeriodKey=' + encodeURIComponent(params.timePeriodKey));
        if (params.costTypeId) parts.push('costTypeId=' + encodeURIComponent(params.costTypeId));
        if (params.cropId) parts.push('cropId=' + encodeURIComponent(params.cropId));
        if (params.locationId) parts.push('locationId=' + encodeURIComponent(params.locationId));
        if (params.cropTypeId) parts.push('cropTypeId=' + encodeURIComponent(params.cropTypeId));
        parts.push('page=' + encodeURIComponent(params.page || 1));
        parts.push('pageSize=' + encodeURIComponent(params.pageSize || 50));
        return window.location.pathname + '?' + parts.join('&');
    }

    // Show loading indicator in detail container
    function showLoading() {
        var container = document.getElementById('detail-container');
        if (!container) return;
        container.innerHTML = '<p>Loading...</p>';
    }

    // Show error message in detail container and log to console
    function showError(message, err) {
        var container = document.getElementById('detail-container');
        if (container) container.innerHTML = '<p>' + escapeHtml(message) + '</p>';
        if (err) console.error('CropCostReport error:', err);
    }

    // Fetch details JSON from server and render
    function fetchAndRender(params) {
        var url = buildDetailsUrl(params);
        var opts = { credentials: 'same-origin' };

        showLoading();

        return fetch(url, opts)
            .then(function (resp) {
                if (!resp.ok) throw new Error('Network response was not ok: ' + resp.status);
                return resp.json();
            })
            .then(function (json) {
                renderDetails(json, params);
                return json;
            })
            .catch(function (err) {
                showError('Error loading details. See console for details.', err);
                throw err;
            });
    }

    // Render detail table and paging controls into containerId (default: detail-container)
    function renderDetails(data, requestParams) {
        var container = document.getElementById('detail-container');
        if (!container) return;

        if (!data || !Array.isArray(data.rows) || data.rows.length === 0) {
            container.innerHTML = '<p>No detail rows.</p>';
            return;
        }

        var sb = [];
        sb.push('<table class="table"><thead><tr>');
        sb.push('<th>CostDate</th><th>Crop</th><th>CostType</th><th>Location</th><th>Quantity</th><th>Unit</th><th>UnitPrice</th><th>TotalAmount</th><th>Note</th>');
        sb.push('</tr></thead><tbody>');

        data.rows.forEach(function (r) {
            sb.push('<tr>');
            sb.push('<td>' + (r.costDate ? escapeHtml(formatDate(r.costDate)) : '') + '</td>');
            sb.push('<td>' + escapeHtml(r.cropName) + '</td>');
            sb.push('<td>' + escapeHtml(r.costTypeName) + '</td>');
            sb.push('<td>' + escapeHtml(r.locationName) + '</td>');
            sb.push('<td>' + (r.quantity != null ? escapeHtml(String(r.quantity)) : '') + '</td>');
            sb.push('<td>' + escapeHtml(r.unit) + '</td>');
            sb.push('<td>' + (r.unitPrice != null ? escapeHtml(String(r.unitPrice)) : '') + '</td>');
            sb.push('<td>' + (r.totalAmount != null ? escapeHtml(String(r.totalAmount)) : '') + '</td>');
            sb.push('<td>' + escapeHtml(r.note) + '</td>');
            sb.push('</tr>');
        });

        sb.push('</tbody></table>');

        // paging controls
        var page = data.page || 1;
        var pageSize = data.pageSize || requestParams.pageSize || 50;
        var hasMore = !!data.hasMore;

        sb.push('<div class="detail-paging" role="navigation" aria-label="Detail paging">');
        if (page > 1) {
            sb.push('<button id="detail-prev" data-page="' + (page - 1) + '" type="button">Previous</button>');
        }
        if (hasMore) {
            sb.push('<button id="detail-next" data-page="' + (page + 1) + '" type="button">Next</button>');
        }
        sb.push('<span> Page ' + page + '</span>');
        sb.push('</div>');

        container.innerHTML = sb.join('');

        // wire paging buttons
        var prev = document.getElementById('detail-prev');
        if (prev) prev.addEventListener('click', function () { onPageButtonClick(data, parseInt(this.getAttribute('data-page'), 10)); });
        var next = document.getElementById('detail-next');
        if (next) next.addEventListener('click', function () { onPageButtonClick(data, parseInt(this.getAttribute('data-page'), 10)); });
    }

    function onPageButtonClick(prevData, targetPage) {
        var params = {
            timePeriodKey: prevData.timePeriodKey,
            costTypeId: prevData.costTypeId,
            cropId: prevData.cropId,
            locationId: prevData.locationId,
            cropTypeId: prevData.cropTypeId,
            page: targetPage,
            pageSize: prevData.pageSize || 50
        };
        fetchAndRender(params).catch(function (err) {
            showError('Error loading details. See console for details.', err);
        });
    }

    function onDetailsClick(e) {
        var target = e.currentTarget;
        e.preventDefault();

        // parse query params from href
        var href = target.getAttribute('href');
        if (!href) return;

        var query = href.split('?')[1] || '';
        var qp = parseQuery(query);

        var params = {
            timePeriodKey: qp.timePeriodKey || qp.timePeriod || qp.tp || '',
            costTypeId: qp.costTypeId ? parseInt(qp.costTypeId, 10) : null,
            cropId: qp.cropId ? parseInt(qp.cropId, 10) : null,
            locationId: qp.locationId ? parseInt(qp.locationId, 10) : null,
            cropTypeId: qp.cropTypeId ? parseInt(qp.cropTypeId, 10) : null,
            page: qp.page ? parseInt(qp.page, 10) : 1,
            pageSize: qp.pageSize ? parseInt(qp.pageSize, 10) : 50
        };

        fetchAndRender(params).catch(function (err) {
            showError('Error loading details. See console for details.', err);
        });
    }

    // Utilities
    function parseQuery(qstr) {
        var obj = {};
        if (!qstr) return obj;
        qstr.split('&').forEach(function (part) {
            var kv = part.split('=');
            if (!kv[0]) return;
            obj[decodeURIComponent(kv[0])] = kv[1] ? decodeURIComponent(kv[1]) : '';
        });
        return obj;
    }

    function formatDate(d) {
        try {
            var dt = new Date(d);
            if (isNaN(dt.getTime())) return d;
            return dt.toISOString().split('T')[0];
        } catch (e) {
            return d;
        }
    }

    function escapeHtml(input) {
        if (input === null || input === undefined) return '';
        return String(input)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#039;');
    }

    // expose
    window.CropCostReport = CropCostReport;

    // auto-init on DOMContentLoaded
    document.addEventListener('DOMContentLoaded', function () {
        CropCostReport.init();
    });

})(window, document);
