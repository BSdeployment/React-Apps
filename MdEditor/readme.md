# 📝 MDX Desktop Editor (React + Tauri)

A modern, fast, and lightweight desktop editor for writing and managing MDX content — built with **React.js** on the frontend and powered by **Tauri (Rust)** for a secure and efficient native desktop experience.

---

## 🚀 Overview

This project is a desktop application designed for developers, writers, and content creators who work with **Markdown (MDX)** and want a smooth, responsive, and distraction-free editing environment.

Unlike traditional Electron-based apps, this editor leverages **Tauri**, resulting in:

* ⚡ Smaller bundle size
* 🔒 Improved security model
* 🚀 Better performance

---

## 🧩 Tech Stack

* **Frontend:** React.js
* **Editor Engine:** MDX Editor
* **Desktop Runtime:** Tauri (Rust)
* **Language Support:** Markdown + JSX (MDX)

---

## ✨ Features

* 📝 Rich MDX editing experience
* ⚡ Fast and responsive UI
* 💾 Local desktop application (no browser dependency)
* 🔄 Unsaved changes warning (before unload protection)
* 🖥️ Native desktop window with custom title
* 🔒 Secure Rust backend via Tauri

---

## 🖼️ Use Cases

* Writing technical documentation
* Creating blog posts with MDX
* Managing developer notes
* Offline-first content editing

---

## ⚙️ Why Tauri?

Tauri provides a modern alternative to Electron by using the system's native webview and a Rust backend.

Benefits include:

* Minimal memory footprint
* Native performance
* Strong security boundaries

---

## 📦 Installation & Run

```bash
npm install
npm run tauri dev
```

---

## 🏁 Build (Production)

```bash
npm run tauri build
```

This will generate a native executable (.exe) ready for distribution.

---

## 🎯 Future Improvements

* Autosave functionality
* File system integration (open/save files)
* Theme support (dark/light)
* Plugin system

---

<img src="https://github.com/BSdeployment/React-Apps/blob/main/MdEditor/img.png?raw=true" width="500"/>

## 💡 Inspiration

Built as a learning and exploration project combining modern web technologies with native desktop capabilities.

---

## 📄 License

MIT License
