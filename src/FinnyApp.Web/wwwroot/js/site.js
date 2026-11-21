// Finny front-end behaviour. No dependencies (jQuery is only used by the validation scripts).
(function () {
    'use strict';

    var $$ = function (selector, root) {
        return Array.prototype.slice.call((root || document).querySelectorAll(selector));
    };

    /* ---------- Mobile menu ---------- */
    var toggle = document.querySelector('[data-nav-toggle]');
    var menu = document.getElementById('header-menu');

    function setMenu(open) {
        menu.classList.toggle('is-open', open);
        toggle.setAttribute('aria-expanded', String(open));
    }

    if (toggle && menu) {
        toggle.addEventListener('click', function () {
            setMenu(!menu.classList.contains('is-open'));
        });
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && menu.classList.contains('is-open')) {
                setMenu(false);
                toggle.focus();
            }
        });
    }

    /* ---------- Show / hide password ---------- */
    $$('[data-toggle-password]').forEach(function (btn) {
        var input = btn.parentElement.querySelector('input');
        if (!input) return;
        btn.addEventListener('click', function () {
            var show = input.type === 'password';
            input.type = show ? 'text' : 'password';
            btn.textContent = show ? 'Hide' : 'Show';
            btn.setAttribute('aria-pressed', String(show));
            btn.setAttribute('aria-label', show ? 'Hide password' : 'Show password');
        });
    });

    /* ---------- Number stepper (+ / -) ---------- */
    $$('[data-stepper]').forEach(function (box) {
        var input = box.querySelector('input');
        if (!input) return;
        $$('[data-step]', box).forEach(function (btn) {
            btn.addEventListener('click', function () {
                var step = parseInt(btn.getAttribute('data-step'), 10) || 0;
                var current = parseInt(input.value, 10);
                if (isNaN(current)) current = 0;
                var next = current + step;
                if (next < 1) return; // goals need at least one task
                input.value = String(next);
                input.dispatchEvent(new Event('input', { bubbles: true }));
                input.dispatchEvent(new Event('change', { bubbles: true }));
            });
        });
    });

    /* ---------- Lock the submit button while a valid form is sent ---------- */
    document.addEventListener('submit', function (e) {
        var form = e.target;
        if (!(form instanceof HTMLFormElement)) return;
        // Wait a tick so jQuery validation has had its say; skip if it blocked the submit.
        setTimeout(function () {
            if (e.defaultPrevented) return;
            var btn = form.querySelector('button[type="submit"]');
            if (!btn) return;
            btn.classList.add('is-loading');
            btn.setAttribute('aria-busy', 'true');
        }, 0);
    });

    // Coming back with the browser's back button must not leave a button stuck.
    window.addEventListener('pageshow', function () {
        $$('.btn.is-loading').forEach(function (btn) {
            btn.classList.remove('is-loading');
            btn.removeAttribute('aria-busy');
        });
    });

    /* ---------- Home: interactive jar demo ---------- */
    var demo = document.querySelector('[data-demo]');
    if (demo) {
        var SVG_NS = 'http://www.w3.org/2000/svg';
        var tasks = [
            'Make your bed',
            'Feed the cat',
            'Pack your school bag',
            'Water the plants',
            'Set the table',
            'Read for 15 minutes'
        ];
        // Where each coin lands inside the jar (x, y, tilt).
        var spots = [
            [46, 194, -6], [90, 196, 4], [134, 194, 7],
            [68, 158, 5], [112, 159, -5],
            [90, 123, 3]
        ];
        var COIN = 40;

        var goal = parseInt(demo.getAttribute('data-goal'), 10) || tasks.length;
        var reward = parseInt(demo.getAttribute('data-reward'), 10) || 5;
        var done = 0;

        var coins = demo.querySelector('[data-demo-coins]');
        var balance = demo.querySelector('[data-demo-balance]');
        var chip = demo.querySelector('[data-demo-chip]');
        var taskEl = demo.querySelector('[data-demo-task]');
        var button = demo.querySelector('[data-demo-done]');
        var progress = demo.querySelector('[data-demo-progress]');
        var bar = progress.querySelector('span');
        var status = demo.querySelector('[data-demo-status]');

        var render = function () {
            balance.textContent = String(done * reward);
            bar.style.width = (done / goal * 100) + '%';
            progress.setAttribute('aria-valuenow', String(done));
            var finished = done >= goal;
            taskEl.textContent = finished ? 'All tasks done' : tasks[done % tasks.length];
            button.textContent = finished ? 'Start over' : 'Mark as done';
            status.textContent = finished
                ? 'Scooter unlocked! ' + done + ' of ' + goal + ' tasks done.'
                : 'Goal: Scooter. ' + done + ' of ' + goal + ' tasks done.';
        };

        var dropCoin = function (index) {
            var spot = spots[index % spots.length];
            var g = document.createElementNS(SVG_NS, 'g');
            g.setAttribute('transform', 'rotate(' + spot[2] + ' ' + (spot[0] + COIN / 2) + ' ' + (spot[1] + COIN / 2) + ')');
            var use = document.createElementNS(SVG_NS, 'use');
            use.setAttribute('href', '#coin');
            use.setAttribute('x', spot[0]);
            use.setAttribute('y', spot[1]);
            use.setAttribute('width', COIN);
            use.setAttribute('height', COIN);
            use.setAttribute('class', 'coin-drop');
            g.appendChild(use);
            coins.appendChild(g);
        };

        button.addEventListener('click', function () {
            if (done >= goal) {
                done = 0;
                coins.textContent = '';
                render();
                return;
            }
            dropCoin(done);
            done += 1;
            render();
            chip.classList.remove('bump');
            void chip.offsetWidth; // restart the animation
            chip.classList.add('bump');
        });

        render();
    }
})();
