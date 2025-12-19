import { fileURLToPath, URL } from "node:url"
import path from "path"
import vueI18n from "@intlify/unplugin-vue-i18n/vite"
import { defineConfig } from "vite"
import vue from "@vitejs/plugin-vue"
import vueDevTools from "vite-plugin-vue-devtools"

import tailwindcss from "@tailwindcss/vite"

// https://vite.dev/config/
export default defineConfig({
    plugins: [
        vue(),
        vueI18n({
            runtimeOnly: false,
            include: path.resolve(__dirname, "./src/locales/**"),
        }),
        vueDevTools(),
        tailwindcss(),
    ],
    resolve: {
        alias: {
            "@": fileURLToPath(new URL("./src", import.meta.url)),
        },
    },
})
