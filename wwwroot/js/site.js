/* ============================================
   PERSONAL PORTFOLIO - Site JavaScript
   Scroll Reveal, Navbar, Stagger Animations,
   Theme Toggle
   ============================================ */

(function () {
  'use strict';

  /* -----------------------------------------
     THEME TOGGLE
     ----------------------------------------- */
  function initThemeToggle() {
    var toggle = document.getElementById('themeToggle');
    if (!toggle) return;

    var html = document.documentElement;

    function getStoredTheme() {
      return localStorage.getItem('theme');
    }

    function getPreferredTheme() {
      var stored = getStoredTheme();
      if (stored === 'light' || stored === 'dark') return stored;
      if (window.matchMedia && window.matchMedia('(prefers-color-scheme: light)').matches) {
        return 'light';
      }
      return 'dark';
    }

    function applyTheme(theme) {
      if (theme === 'light') {
        html.setAttribute('data-theme', 'light');
      } else {
        html.removeAttribute('data-theme');
      }
      localStorage.setItem('theme', theme);
      updateToggleIcon(theme);
    }

    function updateToggleIcon(theme) {
      if (theme === 'light') {
        toggle.setAttribute('aria-label', 'Switch to dark theme');
        toggle.title = 'Switch to dark theme';
      } else {
        toggle.setAttribute('aria-label', 'Switch to light theme');
        toggle.title = 'Switch to light theme';
      }
    }

    // Initialize icon based on current state
    var currentTheme = html.getAttribute('data-theme') === 'light' ? 'light' : 'dark';
    updateToggleIcon(currentTheme);

    // Toggle on click
    toggle.addEventListener('click', function () {
      var current = html.getAttribute('data-theme') === 'light' ? 'light' : 'dark';
      var next = current === 'light' ? 'dark' : 'light';
      applyTheme(next);
    });

    // Listen for OS theme changes
    if (window.matchMedia) {
      window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (e) {
        // Only auto-switch if user hasn't manually set a preference
        if (!getStoredTheme()) {
          applyTheme(e.matches ? 'dark' : 'light');
        }
      });
    }
  }

  /* -----------------------------------------
     INTERSECTION OBSERVER: Scroll Reveal
     ----------------------------------------- */
  function initScrollReveal() {
    var reveals = document.querySelectorAll('.reveal');
    if (!reveals.length) return;

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          entry.target.classList.add('active');
        }
      });
    }, {
      threshold: 0.15,
      rootMargin: '0px 0px -40px 0px'
    });

    reveals.forEach(function (el) {
      observer.observe(el);
    });
  }

  /* -----------------------------------------
     STAGGER ANIMATION for .reveal-stagger
     ----------------------------------------- */
  function initStaggerReveal() {
    var staggers = document.querySelectorAll('.reveal-stagger');
    if (!staggers.length) return;

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          entry.target.classList.add('active');
        }
      });
    }, {
      threshold: 0.1,
      rootMargin: '0px 0px -30px 0px'
    });

    staggers.forEach(function (el) {
      observer.observe(el);
    });
  }

  /* -----------------------------------------
     NAVBAR: Scroll background change
     ----------------------------------------- */
  function initNavbarScroll() {
    var navbar = document.querySelector('.navbar');
    if (!navbar) return;

    var SCROLL_THRESHOLD = 50;

    function onScroll() {
      if (window.scrollY > SCROLL_THRESHOLD) {
        navbar.classList.add('scrolled');
      } else {
        navbar.classList.remove('scrolled');
      }
    }

    window.addEventListener('scroll', onScroll, { passive: true });
    onScroll(); // init on load
  }

  /* -----------------------------------------
     SMOOTH SCROLL for anchor links
     ----------------------------------------- */
  function initSmoothScroll() {
    document.addEventListener('click', function (e) {
      var link = e.target.closest('a[href^="#"]');
      if (!link) return;

      var targetId = link.getAttribute('href');
      if (!targetId || targetId === '#') return;

      var target = document.querySelector(targetId);
      if (!target) return;

      e.preventDefault();
      target.scrollIntoView({ behavior: 'smooth', block: 'start' });

      // Update URL without jump
      if (history.pushState) {
        history.pushState(null, null, targetId);
      }
    });
  }

  /* -----------------------------------------
     ACTIVE NAV LINK highlighting
     ----------------------------------------- */
  function initActiveNavLink() {
    var currentPath = window.location.pathname.toLowerCase();
    var navLinks = document.querySelectorAll('.navbar .nav-link[href]');

    navLinks.forEach(function (link) {
      var href = link.getAttribute('href');
      if (!href) return;

      // Handle asp-generated links
      var linkPath = link.getAttribute('href').toLowerCase();
      if (currentPath === linkPath || (linkPath !== '/' && currentPath.startsWith(linkPath))) {
        link.classList.add('active');
      }
    });
  }

  /* -----------------------------------------
     INIT
     ----------------------------------------- */
  function init() {
    initThemeToggle();
    initScrollReveal();
    initStaggerReveal();
    initNavbarScroll();
    initSmoothScroll();
    initActiveNavLink();
  }

  // Run when DOM is ready
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init);
  } else {
    init();
  }

})();
