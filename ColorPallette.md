# Color Palette & Design System — PersonalPortfolio

## Theme: Dark Tech / Premium Developer Portfolio

A dark, sophisticated color system built for a developer portfolio targeting recruiters and tech professionals. The palette draws from deep-space darkness with electric cyan as the primary accent — evoking terminal screens, code editors, and modern developer tools.

---

## Color Tokens

### Backgrounds

| Token | Hex | Usage |
|-------|-----|-------|
| `--bg-base` | `#0a0a0f` | Page background, deepest layer |
| `--bg-elevated` | `#12121a` | Cards, modals, elevated surfaces |
| `--bg-surface` | `#1a1a2e` | Secondary surfaces, admin panel sections |
| `--bg-hover` | `#252540` | Hover states, active items |

**Rationale:** A near-black base with subtle blue undertone (`#0a0a0f`) avoids the flatness of pure `#000000` while maintaining maximum contrast for text. Elevated surfaces step up slightly in lightness to create depth hierarchy without relying on shadows alone.

### Borders

| Token | Value | Usage |
|-------|-------|-------|
| `--border-subtle` | `rgba(255, 255, 255, 0.06)` | Card borders, table dividers, section separators |
| `--border-active` | `rgba(0, 212, 255, 0.3)` | Focus rings, hover borders, active states |

**Rationale:** Ultra-subtle white borders at 6% opacity create soft delineation without visual noise. The active border uses the accent cyan at 30% for a glow-like effect on interaction.

### Text

| Token | Hex | WCAG Ratio on `#0a0a0f` | Usage |
|-------|-----|--------------------------|-------|
| `--text-primary` | `#e8e8f0` | ~15.8:1 (AAA) | Headings, primary content, labels |
| `--text-secondary` | `#8888a0` | ~5.2:1 (AA) | Body text, descriptions, paragraphs |
| `--text-muted` | `#555570` | ~2.8:1 (large only) | Captions, metadata, timestamps |

**Rationale:** The primary text is a cool off-white with slight blue tint to harmonize with the dark base. Secondary text passes WCAG AA for normal text. Muted text is reserved for non-essential metadata and only used at large sizes.

### Accent Colors

| Token | Hex | Usage |
|-------|-----|-------|
| `--accent-cyan` | `#00d4ff` | Primary CTA buttons, links, active states, highlights |
| `--accent-cyan-glow` | `rgba(0, 212, 255, 0.15)` | Glow backgrounds, hover overlays |
| `--accent-cyan-glow-strong` | `rgba(0, 212, 255, 0.3)` | Strong glow, focus rings, border accents |
| `--accent-violet` | `#7c3aed` | Secondary accent, skill category badges, secondary CTAs |
| `--accent-violet-glow` | `rgba(124, 58, 237, 0.15)` | Violet glow backgrounds |

**Rationale:** Electric cyan (`#00d4ff`) is the hero accent — it reads as "tech" without being the overused AI-purple gradient. It has excellent contrast against the dark base (~11.5:1). Violet (`#7c3aed`) serves as a secondary accent for categorical differentiation (skill badges, secondary actions) without competing with the primary cyan.

### Semantic Colors

| Token | Hex | Usage |
|-------|-----|-------|
| `--success` | `#10b981` | Success alerts, create confirmations, green badges |
| `--danger` | `#ef4444` | Error alerts, delete buttons, validation errors |
| `--warning` | `#f59e0b` | Admin panel header, warning alerts, amber badges |

**Rationale:** Standard semantic colors chosen for high visibility against dark backgrounds. All pass WCAG AA contrast requirements.

---

## Typography

### Font Families

| Token | Font Stack | Usage |
|-------|-----------|-------|
| `--font-body` | `Inter`, `-apple-system`, `BlinkMacSystemFont`, `Segoe UI`, `sans-serif` | All body text, headings, UI elements |
| `--font-mono` | `JetBrains Mono`, `Fira Code`, `monospace` | Code snippets, eyebrow labels, metadata, technical details |

**Rationale:** Inter is a highly legible, neutral sans-serif designed for screens. It has excellent weight range (300-800) and looks premium at display sizes. JetBrains Mono provides the "developer" aesthetic for labels and code-related elements.

### Type Scale

| Element | Size | Weight | Letter Spacing | Line Height |
|---------|------|--------|----------------|-------------|
| Hero Display | `3.5rem` (56px) | 800 | `-0.03em` | 1.05 |
| Section H2 | `2rem` (32px) | 700 | `-0.02em` | 1.2 |
| Card Title H5 | `1.15rem` (18.4px) | 600 | `-0.01em` | 1.3 |
| Body Text | `1rem` (16px) | 400 | `0` | 1.6 |
| Small / Caption | `0.875rem` (14px) | 400 | `0` | 1.5 |
| Eyebrow Label | `0.7rem` (11.2px) | 400 | `0.1em` | 1.4 |

---

## Spacing & Layout

### Section Spacing
- **Vertical padding:** `py-5` to `py-6` (Bootstrap) — generous breathing room between sections
- **Hero section:** `min-height: 100dvh` — full viewport, no scrolling required
- **Card gaps:** `gap: 1.5rem` (24px) in grid layouts

### Border Radius
- **Cards:** `16px` — soft, approachable
- **Buttons:** `12px` — slightly softer than cards
- **Inputs:** `10px` — clean, modern
- **Badges:** `8px` — compact, consistent
- **Skill icons:** `14px` — matches card rhythm

---

## Effects & Animations

### Glassmorphism
Cards use a subtle glass effect:
```css
background: rgba(18, 18, 26, 0.8);
backdrop-filter: blur(20px);
border: 1px solid rgba(255, 255, 255, 0.06);
```
Solid fallback for `prefers-reduced-transparency`.

### Animation Timing
All transitions use `cubic-bezier(0.16, 1, 0.3, 1)` (ease-out-expo) for a natural, premium feel.

| Duration | Usage |
|----------|-------|
| `150ms` | Fast interactions (hover, focus) |
| `300ms` | Standard transitions (card lift, border glow) |
| `500ms` | Slow transitions (section reveals, page elements) |

### Key Animations

| Name | Effect | Trigger |
|------|--------|---------|
| `fadeInUp` | Elements fade in from 30px below | Scroll into viewport (IntersectionObserver) |
| `gradientShift` | Hero name gradient cycles through cyan/violet/blue | Continuous loop, 6s |
| `floatParticle` | Background dots drift vertically | Continuous loop, varied durations |
| `orbPulse` | Gradient orb behind hero text pulses opacity | Continuous loop, 4s |
| `pulseGlow` | Primary buttons emit subtle glow pulse | Continuous loop, 2s |

### Scroll Reveal
- Elements with class `.reveal` animate in when 15% visible
- `.reveal-stagger > *` children get incremental delays (60ms each)
- Respects `prefers-reduced-motion: reduce` — all animations disabled

---

## Component Styling

### Navbar
- **Background:** `rgba(10, 10, 15, 0.85)` with `backdrop-filter: blur(20px)`
- **Height:** ~64px
- **Border:** 1px bottom gradient line (cyan to transparent)
- **Links:** `--text-secondary` on default, `--accent-cyan` on hover

### Cards (Projects, Certificates)
- **Background:** `--bg-elevated` with glass effect
- **Border:** `--border-subtle`
- **Hover:** Translate up 8px, border changes to `--border-active`, subtle box-shadow glow
- **Image:** Gradient overlay from transparent to `--bg-elevated`

### Skill Cards
- **Layout:** Horizontal flex (icon + info)
- **Icon:** 56px square, cyan gradient background, white icon
- **Proficiency dots:** 12px circles, `--bg-hover` inactive, `--accent-cyan` active (with glow)

### Buttons
- **Primary:** `--accent-cyan` background, dark text, glow shadow on hover
- **Outline:** Transparent with `--accent-cyan` border and text, fills on hover
- **Danger:** `--danger` background for delete actions
- **Scale:** `0.98` on `:active` for tactile feedback

### Forms
- **Background:** `--bg-elevated`
- **Input background:** `--bg-surface`
- **Border:** `--border-subtle`, changes to `--accent-cyan` on focus
- **Focus ring:** `0 0 0 3px var(--accent-cyan-glow)`
- **Labels:** `--text-secondary`

### Tables (Admin Panel)
- **Header:** `--bg-surface` background
- **Rows:** Alternating `--bg-elevated` / `--bg-base`
- **Hover:** `--bg-hover`
- **Borders:** `--border-subtle`

---

## Dark Mode Consistency

The entire site supports both dark (default) and light themes via a `data-theme` attribute on `<html>`. The dark theme is the default (no attribute = dark). Users can toggle between themes with a button in the navbar, and their preference is persisted in `localStorage`.

## Light Mode

The light theme activates via `data-theme="light"` on the `<html>` element. All CSS custom properties are overridden to provide a clean, premium light experience.

### Light Mode Color Tokens

#### Backgrounds

| Token | Hex | Usage |
|-------|-----|-------|
| `--bg-base` | `#f8f9fc` | Page background — warm off-white |
| `--bg-elevated` | `#ffffff` | Cards, modals, elevated surfaces |
| `--bg-surface` | `#f1f3f9` | Secondary surfaces, table headers |
| `--bg-hover` | `#e8ecf4` | Hover states, active items |

#### Borders

| Token | Value | Usage |
|-------|-------|-------|
| `--border-subtle` | `rgba(0, 0, 0, 0.08)` | Card borders, dividers |
| `--border-active` | `rgba(8, 145, 178, 0.3)` | Focus rings, active borders |

#### Text

| Token | Hex | Usage |
|-------|-----|-------|
| `--text-primary` | `#1a1a2e` | Headings, primary content — dark navy, not pure black |
| `--text-secondary` | `#5a5a7a` | Body text, descriptions |
| `--text-muted` | `#9898b0` | Captions, metadata |

#### Accents

| Token | Hex | Usage |
|-------|-----|-------|
| `--accent-cyan` | `#0891b2` | Primary accent — deeper cyan for light bg contrast |
| `--accent-cyan-glow` | `rgba(8, 145, 178, 0.1)` | Subtle tint backgrounds |
| `--accent-cyan-glow-strong` | `rgba(8, 145, 178, 0.2)` | Focus rings, border accents |
| `--accent-violet` | `#6d28d9` | Secondary accent — deeper violet |
| `--accent-violet-glow` | `rgba(109, 40, 217, 0.08)` | Violet tint backgrounds |

#### Semantic Colors

| Token | Hex | Usage |
|-------|-----|-------|
| `--success` | `#059669` | Success — slightly darker for contrast |
| `--danger` | `#dc2626` | Error — slightly darker |
| `--warning` | `#d97706` | Warning — slightly darker |

### Light Mode Design Decisions

- **Backgrounds:** Cool off-white (`#f8f9fc`) instead of pure white to reduce eye strain
- **Cards:** White with subtle box-shadow instead of glow effects
- **Navbar:** Frosted white glass (`rgba(255, 255, 255, 0.85)`) with backdrop blur
- **Hero gradient name:** Uses deeper cyan/violet values for contrast on light bg
- **Buttons:** White text on primary (instead of dark text), deeper cyan tones
- **Hero orb:** Reduced opacity (0.35) to avoid overwhelming the light background
- **Particles:** Reduced opacity (0.15) for subtlety on light bg
- **Transitions:** `background-color 0.3s ease, color 0.3s ease` for smooth theme switching

---

## Accessibility

- All text passes WCAG AA contrast against its background
- **Dark mode:** Primary text (`#e8e8f0` on `#0a0a0f`): ~15.8:1 (AAA)
- **Dark mode:** Secondary text (`#8888a0` on `#0a0a0f`): ~5.2:1 (AA)
- **Light mode:** Primary text (`#1a1a2e` on `#f8f9fc`): ~15.5:1 (AAA)
- **Light mode:** Secondary text (`#5a5a7a` on `#f8f9fc`): ~5.8:1 (AA)
- Interactive elements have visible focus indicators (cyan glow ring)
- All animations respect `prefers-reduced-motion: reduce`
- Theme toggle button has proper `aria-label` and `title` attributes
- Theme toggle button has visible `:focus-visible` outline
- Form inputs have visible labels (not placeholder-as-label)
- Button text is always readable against its background
- Theme preference persists via `localStorage` across sessions
- Anti-flash script applies theme before body renders to prevent FOUC

---

## CSS Custom Properties Reference

All design tokens are defined as CSS custom properties on `:root` in `wwwroot/css/site.css`. The dark theme values are on `:root` (default). Light theme overrides are on `[data-theme="light"]`. The entire theme cascades from these variables.

### Theme Toggle Architecture

1. **Anti-flash script** in `<head>` reads `localStorage` and sets `data-theme` before render
2. **`site.css`** defines `[data-theme="light"]` overrides after `:root`
3. **`site.js`** handles click events, localStorage, and OS preference detection
4. **Toggle button** in navbar uses Font Awesome icons (sun/moon) with CSS show/hide
