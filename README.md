# SortVisualizer

A web application for demonstrating how data sorting algorithms work.\
The application runs directly in the browser and is deployed via **GitHub Pages**.

🔗 [Try the application](https://dobriytauren.github.io/SortVisualizer/)

---

## Features

- **Seven sorting algorithms** — bubble, insertion, quick, shaker (cocktail), merge, heap and Shell sort
- **Two visualizations** — vertical bars or colored circles (hue-mapped). Bars adapt to the element count: rounded and glowing when sparse, crisp and flat when dense to avoid shimmer
- **Compute first, then animate** — the sort is recorded instantly in the background and replayed as a smooth animation, so the UI never blocks on the algorithm itself (a spinner is shown while a large run is being prepared)
- **Full playback controls** — play / pause, step forward and back, a draggable progress slider, and speed presets (×0.25 – ×8) that can be changed live during playback
- **Statistics** — sorting time vs. element count, charted with `Highcharts`
- **History** — every run is stored locally via `Blazored.LocalStorage` / `IndexedDB`, with filtering by algorithm and visualization type

---

## How it works

Animation is decoupled from computation. Running a sort happens in two phases:

1. **Record** — the algorithm runs with no delays while `SortService` captures each step as a compact frame (only the elements that changed). All seven algorithms stay untouched; a stable element `Id` plus state snapshots make this work uniformly for both swap- and move-based algorithms.
2. **Replay** — a `SortPlayer` plays the recorded frames over the live elements with the chosen speed, driving play / pause / step / seek.

---

## Technologies

- Blazor WebAssembly (.NET 9)
- Bootstrap 5
- Highcharts
- Blazored.LocalStorage / IndexedDB
